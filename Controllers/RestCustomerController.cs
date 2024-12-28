using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BarberSaloon.Models;
using BarberSaloon.Data;

namespace BarberSaloon.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RestCustomerController : ControllerBase
    {
        private readonly BarberSaloonDBContext _context;

        public RestCustomerController(BarberSaloonDBContext context)
        {
            _context = context;
        }

        // GET: api/RestCustomer/Appointments/{name}
        [HttpGet("Appointments/{name}")]
        public async Task<ActionResult<IEnumerable<Appointment>>> GetCustomerAppointmentsByName(string name)
        {
            // Müşteriyi ve ilişkili randevuları çek
            var customer = await _context.Customers
                .Include(c => c.Appointment)
                .FirstOrDefaultAsync(c => c.Name == name);

            if (customer == null)
            {
                return NotFound(new { Message = "Müşteri bulunamadı." });
            }

            if (customer.Appointment == null)
            {
                return Ok(new List<Appointment>()); // Boş bir liste döndür
            }

            return Ok(new List<Appointment> { customer.Appointment });
        }
    }
}