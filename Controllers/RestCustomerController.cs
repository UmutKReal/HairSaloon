using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
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

        [HttpGet("Profile/{name}")]
        public async Task<ActionResult<Customer>> GetLoggedInCustomer(string name)
        {
            // Burada name (User.Identity.Name) gelecek
            var customer = await _context.Customers
                .Where(c => c.Name == name)
                    .Select(c => new Customer
                    {
                        Name = c.Name,
                        Surname = c.Surname,
                        Email = c.Email,
                        PhoneNumber = c.PhoneNumber,
                        Password=c.Password
                    })
       .FirstOrDefaultAsync();

            if (customer == null)
                return NotFound("Müşteri bulunamadı.");

            return Ok(customer);
        }


    }
}
