namespace BarberSaloon.Models
{
    public interface IPerson
    {
        string Name { get; set; }
        string Surname { get; set; }
        int Id { get; set; }
        string PhoneNumber { get; set; }
    }
}
