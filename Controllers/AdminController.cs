using BarberSaloon.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BarberSaloon.Controllers
{
    public class AdminController : Controller
    {
        private readonly HairSaloonDBContext _context;
        public AdminController(HairSaloonDBContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public IActionResult Index()
        {
            return View();
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
        public async Task<IActionResult> PersonelEkle_Duzenle([Bind("EmployeeId,Name,Surname,Gender")] Employee employee)
        {
            

                if (employee.EmployeeId == 0)
                {
                    _context.Add(employee);
                }
                else
                {
                    _context.Update(employee);
                }
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(PersonelIslemleri));
            
            //return View(employee);
                    
        }

        // POST: PersonelSil
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> PersonelSil(int id)
        {
            var employee = await _context.Employees.FirstOrDefaultAsync(e => e.EmployeeId == id);
            if (employee == null)
            {
                return NotFound();
            }
            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(PersonelIslemleri));
        }
    }
}