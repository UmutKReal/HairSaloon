namespace BarberSaloon.Models
{
    public abstract class StaffMember : IStaffMember
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public int Id { get; set; }
        public string PhoneNumber { get; set; }
        public string Expertise { get; set; }
    }

}
