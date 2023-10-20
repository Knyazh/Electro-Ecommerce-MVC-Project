using E_Commerce_Platform.DataBase;
using E_Commerce_Platform.Services.Abstracts;
using E_Commerce_Platform.Services.Concretes;
using E_Commerce_Platform.ViewModels.User;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace E_Commerce_Platform.Controllers
{
    public class AuthController : Controller
    {
        private readonly ECommerceDBContext _dbContext;

        private readonly IUserService _userService;

        private readonly IUserActivationService _userActivationService;
        public AuthController(ECommerceDBContext dbContext, IUserService userService, IUserActivationService userActivationService)
        {
            _dbContext = dbContext;
            this._userService = userService;
            _userActivationService = userActivationService;
        }




















        #region Register


        [HttpGet("register")]
        public IActionResult Register()
        {
            if (_userService.IsCurrentUserAuthenticated())
            {
                return RedirectToAction("index", "home");
            }

            return View();
        }


        [HttpPost("register")]
        public IActionResult Register(UserAddViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            if (_dbContext.Users.Any(u => u.Email == model.Email))
            {
                TempData["ErrorMessage"] = "This email address is already available in the system!!!";
                return RedirectToAction("ErrorPage", "Auth");
            }
            return RedirectToAction("Index", "Home");
        }


        #endregion






























        #region Login

        [HttpGet("login")]
        public IActionResult Login()
        {
            if (_userService.IsCurrentUserAuthenticated())
            {
                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(UserLoginViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var user = _dbContext.Users.SingleOrDefault(u => u.Email == model.Email);
            if (user == null)
            {
                ModelState.AddModelError("Password", "Email not found");
                return View(model);
            }

            if (!BCrypt.Net.BCrypt.Verify(model.Password, user.Password))
            {
                ModelState.AddModelError("Password", "Password is not valid");
                return View(model);
            }

            if (!user.IsComfirmed)
            {
                ModelState.AddModelError(string.Empty, "Account is not confirmed");
                return View(model);
            }


            var claim = new List<Claim>
            {
                new Claim("id", user.Id.ToString()),
            };


            claim.AddRange(_userService.GetClaimsAccordingToRole(user));


            var claimsIdentity = new ClaimsIdentity(claim, CookieAuthenticationDefaults.AuthenticationScheme);
            var claimsPricipal = new ClaimsPrincipal(claimsIdentity);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPricipal);

            return RedirectToAction("Index", "Home");
        }

        #endregion

        #region Logout

        #endregion


        [HttpGet]
        public IActionResult ErrorPage()
        {
            return View();
        }
    }
}
