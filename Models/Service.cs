using BarberSaloon.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BarberSaloon.Models
{
    public class Service
    {
        [Key]
        public int ServiceID { get; set; }

        [Required]
        public string? ServiceName { get; set; }

        [Required]
        public int ServiceDuration { get; set; } // Süreyi dakika cinsinden belirtin

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal ServicePrice { get; set; }

        // Bire çok ilişki için Navigation Property
        public ICollection<AppointmentDateTime> AppointmentDateTimes { get; set; }

        // Employee ile bire çok ilişki için Foreign Key
        public int EmployeeID { get; set; }

        [ForeignKey("EmployeeID")]
        public Employee Employee { get; set; }
    }
}
