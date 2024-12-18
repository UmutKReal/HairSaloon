using Microsoft.AspNetCore.Mvc;

namespace BarberSaloon.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        { 
            return View();
        }

    }
}
