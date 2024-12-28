using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BarberSaloon.Models
{
    public class Employee
    {
        [Key]
        public int EmployeeID { get; set; }

        [Required(ErrorMessage = "İsim zorunludur.")]
        [MaxLength(30, ErrorMessage = "Maksimum 30 karakter yazılabilir.")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "Soyisim zorunludur.")]
        [MaxLength(30, ErrorMessage = "Maksimum 30 karakter yazılabilir.")]
        public string? Surname { get; set; }

        [Required(ErrorMessage = "Cinsiyet Seçiniz")]
        public string? Gender { get; set; }

        [Required(ErrorMessage = "Uzmanlık Seçiniz")]
        public string? Skills { get; set; }

        // Bire bir ilişki için Navigation Property
        public ICollection<AppointmentDateTimeEmployee>? AppointmentDateTimes { get; set; }

        // Bire çok ilişki için Navigation Property
        public ICollection<Service>? Services { get; set; }

    }
}