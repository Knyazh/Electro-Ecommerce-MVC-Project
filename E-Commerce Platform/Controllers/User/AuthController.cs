using E_Commerce_Platform.DataBase;
using E_Commerce_Platform.Services.Abstracts;
using E_Commerce_Platform.Services.Concretes;
using E_Commerce_Platform.ViewModels.User;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace E_Commerce_Platform.Controllers.User
{
    public class AuthController : Controller
    {

        private readonly ECommerceDBContext _dbContext;

        private readonly IUserService _userService;

        private readonly IUserActivationService _userActivationService;
        public AuthController(ECommerceDBContext dbContext, IUserService userService, IUserActivationService userActivationService)
        {
            _dbContext = dbContext;
            _userService = userService;
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
        public IActionResult Register(RegisterViewModel model)
        {

            foreach (var key in ModelState.Keys)
            {
                var modelStateEntry = ModelState[key];
                foreach (var error in modelStateEntry.Errors)
                {
                    Console.WriteLine(error.ErrorMessage.ToString());
                }
            }

            if (!ModelState.IsValid) return View(model);

            if (_dbContext.Users.Any(u => u.Email == model.Email))
            {
                ModelState.AddModelError("email", "This email already exists in the system!");
                return RedirectToAction("ErrorPage", "Auth");
            }

            var user = new DataBase.Models.User
            {
                Name = model.Name,
                LastName = model.LastName,
                PIN = model.PIN,
                Email = model.Email,
                PhysicalImageName = model.PhysicalImageName,
                DateofBirth = model.DateOfBirth,
                Password = BCrypt.Net.BCrypt.HashPassword(model.Password),
            };

            _dbContext.Users.Add(user);

            //_userActivationService.CreateAndSendActivationtoken(user);

            _dbContext.SaveChanges();

            return RedirectToAction("Index", "Home");
        }


        //[HttpGet("verify-account/{token}", Name = "register-account-verification")]
        //public IActionResult VerifyAccount(Guid token)
        //{

        //    var activation = _dbContext.UserActivations
        //        .Include(ua => ua.User)
        //        .SingleOrDefault(ua =>
        //            !ua.User.IsConfirmed &&
        //            ua.Token == token &&
        //            ua.ExpireDate > DateTime.UtcNow);

        //    if (activation == null)
        //        return BadRequest("Token not found or already expire");

        //    activation.User.IsConfirmed = true;
        //    _dbContext.SaveChanges();

        //    return RedirectToAction("login", "auth");
        //}


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

            var claims = new List<Claim>
        {
            new Claim("id", user.Id.ToString()),
        };

            claims.AddRange(_userService.GetClaimsAccordingToRole(user));

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var claimsPricipal = new ClaimsPrincipal(claimsIdentity);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPricipal);

            //if (!user.IsConfirmed)
            //{
            //    ModelState.AddModelError(string.Empty, "Account is not confirmed");
            //    return View(model);
            //}

            return RedirectToAction("Index", "Home");
        }
        //    private async Task SignInUser(DataBase.Models.User user)
        //    {
        //        var identity = new ClaimsIdentity(new[]
        //        {
        //    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
        //},  CookieAuthenticationDefaults.AuthenticationScheme);

        //        var principal = new ClaimsPrincipal(identity);

        //        var authProperties = new AuthenticationProperties
        //        {
        //            IsPersistent = false 
        //        };

        //        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, authProperties);
        //    }

        #endregion

        #region Logout

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction("index", "home");
        }


        #endregion


        [HttpGet]
        public IActionResult ErrorPage()
        {
            return View();
        }
    }
}
