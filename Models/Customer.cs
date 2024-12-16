using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace BarberSaloon.Models
{
    public class Customer
    {
        [Key]
        public int CustomerId { get; set; } 

        [Required(ErrorMessage = "İsim zorunludur.")]
        [MaxLength(30)]
        public string? Name { get; set; }

        [Required(ErrorMessage ="Email Gereklidir")]
        [MaxLength(50)]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Soyisim zorunludur.")]
        [MaxLength(30)]
        public string? Surname { get; set; }

        [Required]
        [RegularExpression(@"^\+?[1-9]\d{1,14}$", ErrorMessage = "Telefon numarası yanlıştır")]
        public string? PhoneNumber { get; set; }

        [Required]
        [RegularExpression("Erkek|Kadın", ErrorMessage = "Cinsiyet sadece 'Erkek' veya 'Kadın' olabilir.")]
        public string? Gender { get; set; }

        public ICollection<Appointment>  Appointments { get; set; }     //Navi
    }
}
