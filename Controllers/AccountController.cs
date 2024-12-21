using BarberSaloon.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace BarberSaloon.Controllers
{
    public class AccountController : Controller
    {
        private readonly HairSaloonDBContext _context;
        public AccountController(HairSaloonDBContext context)
        {
            _context = context;
        }
        // GET: /Account/Register
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        // POST: /Account/Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(Customer model)
        {
           
            // 2. Aynı email ile kayıtlı kullanıcı var mı kontrol et
            bool userExists = await _context.Customers.AnyAsync(c => c.Email == model.Email);
            if (userExists)
            {
                ModelState.AddModelError(string.Empty, "Bu e-posta adresi zaten kayıtlı.");
                return View(model);
            }

            // 3. Yeni müşteri ekle (burada şifre alanı gösterilmedi, isterseniz ekleyebilirsiniz)
            _context.Customers.Add(model);
            await _context.SaveChangesAsync();

            // 4. Kayıt başarılı, login sayfasına yönlendirelim
            return RedirectToAction("Login", "Account");
        }
        
        [HttpGet]
        public IActionResult AdminLogin()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AdminLogin(string username, string password)
        { 
               if (username == "admin" && password == "admin123")
                {
                    return RedirectToAction("Index", "Admin");
                }
                else
                {
                    ViewBag.ErrorMessage = "Admin kullanıcı adı veya şifre hatalı!";
                    return RedirectToAction("Failure");
                }
        }
  
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        
        

        public IActionResult Failure()
        {
            return View();
        }
    }
}