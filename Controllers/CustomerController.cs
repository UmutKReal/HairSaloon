using Microsoft.AspNetCore.Mvc;

namespace BarberSaloon.Controllers
{
    public class CustomerController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Randevu()
        {
            return View();
        }
        public IActionResult CustomerServis()
        {
            return View();
        }
    }
}
