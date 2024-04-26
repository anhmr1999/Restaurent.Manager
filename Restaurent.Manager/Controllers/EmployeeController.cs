using Microsoft.AspNetCore.Mvc;

namespace Restaurent.Manager.Controllers
{
    public class EmployeeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
