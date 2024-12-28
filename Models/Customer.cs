using System.ComponentModel.DataAnnotations;

namespace BarberSaloon.Models
{
    public class Customer
    {
        [Key]
        public int CustomerID { get; set; }

        [Required(ErrorMessage = "İsim zorunludur.")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "Soyisim zorunludur.")]
        public string? Surname { get; set; }

        [Required(ErrorMessage = "Email giriniz.")]
        [EmailAddress(ErrorMessage = "Geçerli bir email adresi giriniz.")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Telefon numarası zorunludur.")]
        [Phone(ErrorMessage = "Geçerli bir telefon numarası giriniz.")]
        public string? PhoneNumber { get; set; }

        [Required(ErrorMessage = "Cinsiyet seçiniz.")]
        public string? Gender { get; set; }

        [Required(ErrorMessage = "Şifre giriniz.")]
        public string? Password { get; set; }

        // Bire bir ilişki için Navigation Property
        public Appointment? Appointment { get; set; }
    }
}