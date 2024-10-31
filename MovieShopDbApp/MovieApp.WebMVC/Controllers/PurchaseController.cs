using Microsoft.AspNetCore.Mvc;
using ApplicationCore.Entities;

namespace MovieApp.WebMVC.Controllers
{
    
    public class PurchaseController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Purchase purchase)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            // purchase logic
            return RedirectToAction("Index");
        }
    }
}
