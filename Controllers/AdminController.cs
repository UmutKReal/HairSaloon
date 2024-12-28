using BarberSaloon.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
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

		public IActionResult Randevular()
		{
			var appointments = _context.appointmentDateTimeEmployees
				.Include(a => a.Employee) // İlişkili tabloyu dahil eder
				.Include(a=> a.Service)
				.ToList();
			return View(appointments);
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
		public async Task<IActionResult> PersonelEkle_Duzenle([Bind("EmployeeID,Name,Surname,Gender,Skills")] Employee employee)
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

        [HttpGet]
        public IActionResult AddService(int id = 0)
        {
            if (id == 0)
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
            ModelState.Remove("Employee");
            ModelState.Remove("AppointmentDateTimes");

            if (!ModelState.IsValid)
            {
                return View(service); // Eğer validasyon hatası varsa aynı view'ı göster
            }

            if (service.ServiceID == 0)
            {
                // Initially, set EmployeeID to a default value or leave it as is
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

        [HttpGet]
        public async Task<IActionResult> UpdateService(int id)
        {
            if (id == 0)
            {
                return NotFound(); // Service ID must be provided
            }

            var service = await _context.Services.FindAsync(id);
            if (service == null)
            {
                return NotFound(); // Service not found
            }

            return View(service); // Return the view with the service to be updated
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateService(Service service)
        {
            ModelState.Remove("Employee");
            ModelState.Remove("AppointmentDateTimes");

            if (!ModelState.IsValid)
            {
                return View(service); // Return the same view with validation errors
            }

            var serviceToUpdate = await _context.Services.FindAsync(service.ServiceID);
            if (serviceToUpdate == null)
            {
                return NotFound(); // Service not found
            }

            // Update the service properties
            serviceToUpdate.ServiceName = service.ServiceName;
            serviceToUpdate.ServiceDuration = service.ServiceDuration;
            serviceToUpdate.ServicePrice = service.ServicePrice;

            // Optionally update the EmployeeID if it is provided
            if (service.EmployeeID.HasValue)
            {
                serviceToUpdate.EmployeeID = service.EmployeeID.Value;
            }

            _context.Services.Update(serviceToUpdate);
            await _context.SaveChangesAsync(); // Save the changes to the database

            return RedirectToAction(nameof(Index)); // Redirect to the index page after successful update
        }
    }
}