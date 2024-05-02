using Microsoft.AspNetCore.Mvc;

namespace Restaurent.Manager.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Chef()
        {
            return View();
        }

        public IActionResult Menu()
        {
            return View();
        }

        public IActionResult Bill(int id)
        {
            return View("Menu");
        }

        public IActionResult AddModal(int id)
        {
            return PartialView();
        }
    }
}
