using E_Commerce_Platform.DataBase;
using E_Commerce_Platform.Services.Abstracts;
using E_Commerce_Platform.ViewModels.User;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce_Platform.Controllers
{
    public class AuthController : Controller
    {
        private readonly ECommerceDBContext _dbContext;

        private readonly IUserService userServide;

        private readonly IUserActivationService _userActivationService;
        public AuthController(ECommerceDBContext dbContext, IUserService userServide, IUserActivationService userActivationService)
        {
            _dbContext = dbContext;
            this.userServide = userServide;
            _userActivationService = userActivationService;
        }


        #region Register
        [HttpGet]   
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Register(UserAddViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            if(_dbContext.Users.Any(u => u.Email == model.Email))
            {
                TempData["ErrorMessage"] = "This email address is already available in the system!!!";
                return RedirectToAction("ErrorPage", "Auth");
            }
            return RedirectToAction("Index", "Home");
        }
        [HttpGet]
        #endregion

        #region Login
        public IActionResult Login()
        {
            return View();  
        }

        [HttpPost]
        public IActionResult Login(UserLoginViewModel Model)
        {
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
