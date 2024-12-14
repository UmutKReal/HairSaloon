using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;
namespace BarberSaloon.Models
{
    public class Employee
    {
        [Key]
        public int EmployeeId { get; set; }

        [Required(ErrorMessage = "İsim zorunludur.")]
        [MaxLength(30)]
        public string Name { get; set; }

        [Required(ErrorMessage = "Soyisim zorunludur.")]
        [MaxLength(30)]
        public  string Surname { get; set; }

        [Required]
        [RegularExpression("Erkek|Kadın", ErrorMessage = "Cinsiyet sadece 'Erkek' veya 'Kadın' olabilir.")]
        public string Gender { get; set; }

        public ICollection<TotalService> TotalServices { get; set; }
    }
}