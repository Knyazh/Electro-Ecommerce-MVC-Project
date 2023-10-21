using Microsoft.AspNetCore.Mvc;

namespace E_Commerce_Platform.Controllers.User
{
    public class AdminController : Controller
    {
        [HttpGet]
        public IActionResult Dashboard()
        {
            return View();
        }
    }
}
