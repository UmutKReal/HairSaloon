using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BarberSaloon.Models
{
    public class AppointmentDateTimeEmployee
    {
        [Key]
        public int AppointmentDTID { get; set; }

        [Required]
        public DateTime AppointmentDate { get; set; }

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
