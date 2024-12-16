using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;
//deneme
namespace BarberSaloon.Models
{
    public class Service
    {
        [Key]
        public int ServiceId { get; set; }

        [Required(ErrorMessage = "Servis türü girmek zorunlududr")]
        public string ServiceName { get; set; }

        public int ServiceDuration  { get; set; }
        public int ServicePrice { get; set; }

        public ICollection<TotalService> TotalServices { get; set; }
    }
}
