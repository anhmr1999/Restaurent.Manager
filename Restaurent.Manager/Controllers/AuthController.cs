using Microsoft.AspNetCore.Mvc;
using Restaurent.Manager.Models;

namespace Restaurent.Manager.Controllers
{
    public class AuthController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(LoginModel model)
        {
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Logout()
        {
            return RedirectToAction("Index");
        }
    }
}
