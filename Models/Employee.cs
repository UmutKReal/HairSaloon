using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Linq;

namespace BarberSaloon.Models
{
    public class Employee
    {
        [Key]
        public int EmployeeId { get; set; }

        [Required(ErrorMessage = "İsim zorunludur.")]
        [MaxLength(30,ErrorMessage = "Maksimum 30 karakter yazılabilir.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Soyisim zorunludur.")]
        [MaxLength(30, ErrorMessage = "Maksimum 30 karakter yazılabilir.")]
        public  string Surname { get; set; }

        [Required(ErrorMessage = "Cinsiyet Seçiniz")]
        public string Gender { get; set; }

        //public ICollection<TotalService> TotalServices { get; set; }
    }
}