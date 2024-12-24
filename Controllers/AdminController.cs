using BarberSaloon.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using BarberSaloon.Models;
using BarberSaloon.Data;

namespace BarberSaloon.Controllers
{
    public class AdminController : Controller
    {
        private readonly BarberSaloonDBContext _context;
        public AdminController(BarberSaloonDBContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Services.ToListAsync());
        }

        public IActionResult RandevuIslemleri()
        {
            return View();
        }

        public async Task<IActionResult> PersonelIslemleri()
        {
            return View(await _context.Employees.ToListAsync());
        }

        // GET: Personel/AddOrEdit
        [HttpGet]
        public IActionResult PersonelEkle_Duzenle(int id = 0)
        {
            if (id == 0)
            {
                return View(new Employee());
            }
            else
            {
                return View(_context.Employees.Find(id));
            }
        }

        // POST: Personel/AddOrEdit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> PersonelEkle_Duzenle([Bind("EmployeeID,Name,Surname,Gender")] Employee employee)
        {
            if (!ModelState.IsValid) return View(employee);
           
            if (employee.EmployeeID == 0)
            {
                await _context.AddAsync(employee);
            }
            else
            {
                _context.Update(employee);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(PersonelIslemleri));
        }

        // POST: PersonelSil
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> PersonelSil(int id)
        {
            var employee = await _context.Employees.FirstOrDefaultAsync(e => e.EmployeeID == id);
            if (employee == null)
            {
                return NotFound();
            }
            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(PersonelIslemleri));
        }

        //ADD AND EDİT
        [HttpGet]
        public IActionResult AddService(int id = 0)
        {
            if ( id == 0 )
            {
                return View(new Service());
            }
            else
            {
                 return View(_context.Services.Find(id));
            }
          
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddService(Service service)
        {
            if (!ModelState.IsValid)  return View(service); // Eğer validasyon hatası varsa aynı view'ı göster
            if (service.ServiceID == 0)
            {
                await _context.Services.AddAsync(service); // Servisi asenkron olarak ekle            
            }
            else
            {
                 _context.Services.Update(service);
            }
             await _context.SaveChangesAsync(); // Veritabanı değişikliklerini kaydet
             return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> DeleteService(int id)
        {
            var service = await _context.Services.FindAsync(id); // ID'ye göre servisi asenkron olarak bul
            if (service == null)
            {
                return NotFound();
            }
            _context.Services.Remove(service); // Servisi kaldır
            await _context.SaveChangesAsync(); // Değişiklikleri kaydet
            return RedirectToAction("Index");
        }
    }
}