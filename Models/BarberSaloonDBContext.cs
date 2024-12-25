using Microsoft.EntityFrameworkCore;
using BarberSaloon.Models;

namespace BarberSaloon.Data
{
    public class BarberSaloonDBContext : DbContext
    {
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<AppointmentDateTime> AppointmentDateTimes { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<AppointmentDateTimeEmployee> appointmentDateTimeEmployees { get; set; }

        public BarberSaloonDBContext(DbContextOptions<BarberSaloonDBContext> options)
            : base(options)
        {
        }

        // Fluent API kullanarak ilişkileri yapılandırma
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // 1. Customer ile Appointment arasında bire bir ilişki
            modelBuilder.Entity<Customer>()
                .HasOne(c => c.Appointment)
                .WithOne(a => a.Customer)
                .HasForeignKey<Appointment>(a => a.CustomerID)
                .OnDelete(DeleteBehavior.Cascade);

            // 2. Appointment ile AppointmentDateTime arasında bire çok ilişki
            modelBuilder.Entity<Appointment>()
                .HasMany(a => a.AppointmentDateTimes)
                .WithOne(adt => adt.Appointment)
                .HasForeignKey(adt => adt.AppointmentID)
                .OnDelete(DeleteBehavior.Cascade);

            // 3. AppointmentDateTime ile Service arasında bire çok ilişki
            modelBuilder.Entity<Service>()
                .HasMany(s => s.AppointmentDateTimes)
                .WithOne(adt => adt.Service)
                .HasForeignKey(adt => adt.ServiceID)
                .OnDelete(DeleteBehavior.Restrict); // Cascade yerine Restrict kullanıldı

            // 4. AppointmentDateTime ile Employee arasında bire çok ilişki
            // 4. AppointmentDateTime ile Employee arasında bire çok ilişki
            modelBuilder.Entity<AppointmentDateTimeEmployee>()
                .HasOne(adt => adt.Employee)
                .WithMany(e => e.AppointmentDateTimes)
                .HasForeignKey(adt => adt.EmployeeID)
                .OnDelete(DeleteBehavior.Restrict); // Cascade yerine Restrict kullanıldı

            // 5. Employee ile Service arasında bire çok ilişki
            modelBuilder.Entity<Employee>()
                .HasMany(e => e.Services)
                .WithOne(s => s.Employee)
                .HasForeignKey(s => s.EmployeeID)
                .OnDelete(DeleteBehavior.Restrict); // Cascade yerine Restrict kullanıldı


            // (Opsiyonel) Seed verilerini ekleme
            modelBuilder.Entity<Service>().HasData(
                new Service { ServiceID = 1, ServiceName = "Sakal Tıraşı", ServiceDuration = 30, ServicePrice = 20.00m, EmployeeID = 1 },
                new Service { ServiceID = 2, ServiceName = "Saç Kesimi", ServiceDuration = 60, ServicePrice = 50.00m, EmployeeID = 1 },
                new Service { ServiceID = 3, ServiceName = "Renk", ServiceDuration = 60, ServicePrice = 70.00m, EmployeeID = 2 }
            );

            // (Opsiyonel) Employee ve Customer seed verileri ekleme
            modelBuilder.Entity<Employee>().HasData(
                new Employee { EmployeeID = 1, Name = "Ahmet", Surname = "Yılmaz", Gender = "Erkek" },
                new Employee { EmployeeID = 2, Name = "Veysel", Surname = "Aras", Gender = "Erkek" },
                new Employee { EmployeeID = 3, Name = "Ayşe", Surname = "Demir", Gender = "Kadın" }
            );

            modelBuilder.Entity<Customer>().HasData(
                new Customer { CustomerID = 1, Name = "Mehmet", Surname = "Kara", Email = "a@gmail.com", PhoneNumber = "5551234567", Gender = "Erkek", Password = "1" }
            );

            // Diğer seed verilerini buraya ekleyebilirsiniz
        }
    }
}
