using Microsoft.AspNetCore.Mvc;

namespace BarberSaloon.Models
{
    public class UserController : Controller
    {
        [HttpGet]
        public ActionResult Login(int id=0)
        {
            return View();
        }

    }
}
