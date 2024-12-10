namespace BarberSaloon.Models
{
    public class Customer : IPerson
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public  int Id { get; set; }
        public string PhoneNumber { get; set; }
        int IPerson.Id { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public void GetAppointment()
        {
            Console.WriteLine("Appointment booked.");
        }
    }
}
