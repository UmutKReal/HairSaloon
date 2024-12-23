using BarberSaloon.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Linq;

namespace BarberSaloon.Models
{
    public class TotalService
    {
        [Key]
        public int TotalServiceId { get; set; }

        public int TotalServicePrice { get; set; }

        public Employee? Employee { get; set; }
        public Service? Service { get; set; }
        public Appointment? Appointment { get; set; }

    }
}
