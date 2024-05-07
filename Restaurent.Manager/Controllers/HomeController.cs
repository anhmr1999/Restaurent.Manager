using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Restaurent.Manager.Models;
using Restaurent.Manager.Models.Datas;

namespace Restaurent.Manager.Controllers
{
    public class HomeController : Controller
    {
        AppDbContext context = new AppDbContext();

        public IActionResult Index()
        {
            var tables = context.Table.Include(x => x.Bills).ToList();
            return View(tables);
        }

        public IActionResult Chef()
        {
            return View();
        }

        public IActionResult Menu(int id)
        {
            if(id == 0)
                return RedirectToAction("Index");
            var table = context.Table.FirstOrDefault(x => x.Id == id);
            ViewData["bill"] = new Bill() { Table = table, Records = new List<BillRecord>() };
            var foods = context.Food.Where(x => x.Status).ToList();
            return View(foods);
        }

        public IActionResult Bill(int id)
        {
            ViewData["bill"] = context.Bill.Include(x => x.Records).ThenInclude(x => x.Food).Include(x => x.Table).FirstOrDefault(x => x.Id == id);
            var foods = context.Food.Where(x => x.Status).ToList();
            return View("Menu", foods);
        }

        public IActionResult AddModal(int id)
        {
            var food = context.Food.FirstOrDefault(x => x.Id == id);
            if (food == null)
                return NotFound();
            return PartialView(food);
        }

        public IActionResult PaymentModal()
        {
            return PartialView();
        }
    }
}
