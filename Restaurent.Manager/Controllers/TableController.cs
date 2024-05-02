using Microsoft.AspNetCore.Mvc;

namespace Restaurent.Manager.Controllers
{
    public class TableController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult AddModal()
        {
            return PartialView();
        }
    }
}
