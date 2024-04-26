using Microsoft.AspNetCore.Mvc;

namespace Restaurent.Manager.Controllers
{
    public class BillController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
