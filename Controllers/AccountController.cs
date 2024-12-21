using BarberSaloon.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

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

            if (string.IsNullOrEmpty(model.Password))
            {
                ModelState.AddModelError("Password", "Şifre boş olamaz.");
                return View(model);
            }

            var customer = new Customer
            {
                Email = model.Email,
                Password = model.Password // Şifre hash'ini sakla
            };

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
                    return RedirectToAction("Failure", "Account");
                }
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UserLogin(string email, string password)
        {
            // 1. Email ve şifre kontrolü
            var user = await _context.Customers
                .Where(c => c.Email == email)
                .FirstOrDefaultAsync();

            if (user == null)
            {
                // Eğer kullanıcı bulunamazsa, hata mesajı ver
                ModelState.AddModelError(string.Empty, "Geçersiz e-posta adresi veya şifre.");
                return View();
            }

            // 2. Şifre doğrulaması
            if (password!=user.Password)
            {
                // Şifre yanlışsa, hata mesajı ver
                ModelState.AddModelError(string.Empty, "Geçersiz e-posta adresi veya şifre.");
                return View();
            }

            // 3. Giriş başarılıysa, kullanıcıyı yönlendirelim (örneğin anasayfaya)
            TempData["SuccessMessage"] = "Giriş başarılı!";
            return RedirectToAction("Index", "Customer"); // Başka bir sayfaya yönlendirme yapılabilir
        }

        public IActionResult Failure()
        {
            return View();
        }
    }
}