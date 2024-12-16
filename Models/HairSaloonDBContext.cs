using Microsoft.EntityFrameworkCore;


namespace BarberSaloon.Models
{
    public class HairSaloonDBContext : DbContext
    {
        public HairSaloonDBContext(DbContextOptions<HairSaloonDBContext> options) : base(options)
        { }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<TotalService> TotalServices { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<Service> Services { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb; Database=HairSaloon;Trusted_Connection=True;");
        }
    }
}
