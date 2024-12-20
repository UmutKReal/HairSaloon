using BarberSaloon.Models;
using Microsoft.AspNetCore.Mvc;

namespace BarberSaloon.Controllers
{
    public class AdminController : Controller
    {
        private readonly HairSaloonDBContext _context;
        public IActionResult Index()
        { 
            return View();
        }
        public IActionResult RandevuIslemleri()
        {
            return View();
        }

        [HttpGet]
        public IActionResult PersonelIslemleri()
        {
          ViewBag.EmployeeList = _context.Employees.ToList();
            return View();
        }

        // POST: PersonelEkle
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult PersonelIslemleri(Employee employee)
        {
            if (ModelState.IsValid)
            {
                _context.Add(employee);
                _context.SaveChanges();
                return RedirectToAction(nameof(PersonelIslemleri));
            }
            return View("PersonelIslemleri", employee);
        }

        // GET: PersonelEkle for update
        [HttpGet]
        public IActionResult PersonelGuncelle(int id)
        {
            var employee = _context.Employees.FirstOrDefault(e => e.EmployeeId == id);
            if (employee == null)
            {
                return NotFound();
            }
            return View(employee);
        }

        // POST: PersonelGuncelle
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult PersonelGuncelle(Employee employee)
        {
            if (ModelState.IsValid)
            {
                _context.Update(employee);
                _context.SaveChanges();
                return RedirectToAction(nameof(PersonelIslemleri));
            }
            return View(employee);
        }

        // GET: PersonelIslemleri/Delete
        [HttpGet]
        public IActionResult PersonelSil(int id)
        {
            var employee = _context.Employees.FirstOrDefault(e => e.EmployeeId == id);
            if (employee == null)
            {
                return NotFound();
            }
            _context.Employees.Remove(employee);
            _context.SaveChanges();
            return RedirectToAction(nameof(PersonelIslemleri));
        }

    }
}
