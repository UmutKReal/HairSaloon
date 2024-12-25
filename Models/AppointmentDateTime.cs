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
        public DateTime AppointmentDate {  get; set; }

        // Foreign key to Appointment
        public int AppointmentID { get; set; }

        [ForeignKey("AppointmentID")]
        public Appointment Appointment { get; set; }

        // Foreign key to Service
        public int ServiceID { get; set; }

        [ForeignKey("ServiceID")]
        public Service Service { get; set; }
    }
}
