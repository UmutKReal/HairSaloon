using Microsoft.AspNetCore.Mvc;

namespace BarberSaloon.Models
{
    public class UserController : Controller
    {
        public IActionResult AddOrEdit(int id=0)
        {
            Customer customerModel = new Customer();
            return View(customerModel);
        }
    }
}
