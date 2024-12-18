using BarberSaloon.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BarberSaloon.Controllers
{
    public class AccountController : Controller
    {
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string username, string password, string roleType)
        {
            // Örnek kontrol: Admin login
            if (roleType == "admin")
            {
                if (username == "admin" && password == "admin123")
                {
                    return RedirectToAction("Index", "Admin");
                }
                else
                {
                    ViewBag.Error = "Admin kullanıcı adı veya şifre hatalı!";
                    return View();
                }
            }
            else
            {
                // Kullanıcı login
                if (username == "user" && password == "user123")
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ViewBag.Error = "Kullanıcı adı veya şifre hatalı!";
                    return View();
                }
            }
        }
    }
}