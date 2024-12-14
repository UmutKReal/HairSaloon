using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Linq;

namespace BarberSaloon.Models
{
    public class Appointment
    {
        [Key]
        public int AppointmentId { get; set; }

        [Required(ErrorMessage = "Tarih girmek zorunludur")]
        public DateTime AppointmentDT { get; set; }

        public Customer Customer { get; set; } //Navigation Property
        public ICollection<TotalService> TotalServices { get; set; } //Navigation Property

    }
}
