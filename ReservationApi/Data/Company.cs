namespace ReservationApi.Data
{
    public class Company
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public List<Restaurant> Restaurants { get; set; } = new List<Restaurant>();
        public List<Patron> Patrons { get; set; } = new List<Patron>();
        public List<Employee> Employees { get; set; } = new List<Employee>();
    }
}
