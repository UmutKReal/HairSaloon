using BarberSaloon.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BarberSaloon.Models
{
    public class Appointment
    {
        [Key]
        public int AppointmentID { get; set; }

        // Foreign key for Customer
        public int CustomerID { get; set; }

        [ForeignKey("CustomerID")]
        public Customer Customer { get; set; }

        // Bire çok ilişki için Navigation Property
        public ICollection<AppointmentDateTime> AppointmentDateTimes { get; set; }
    }
}
