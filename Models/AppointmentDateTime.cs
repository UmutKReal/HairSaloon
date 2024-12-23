using BarberSaloon.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BarberSaloon.Models
{
    public class AppointmentDateTime
    {
        [Key]
        public int AppointmentDTID { get; set; }

        [Required]
        public int Month { get; set; }

        [Required]
        public int Day { get; set; }

        [Required]
        public int Hour { get; set; }

        [Required]
        public int Minute { get; set; }

        // Foreign key to Appointment
        public int AppointmentID { get; set; }

        [ForeignKey("AppointmentID")]
        public Appointment Appointment { get; set; }

        // Foreign key to Service
        public int ServiceID { get; set; }

        [ForeignKey("ServiceID")]
        public Service Service { get; set; }

        // Foreign key to Employee (Bire bir ilişki)
        public int EmployeeID { get; set; }

        [ForeignKey("EmployeeID")]
        public Employee Employee { get; set; }
    }
}
