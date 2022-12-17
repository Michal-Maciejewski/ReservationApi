namespace ReservationApi.Data
{
    public class Restaurant
    {
        public int Id { get; set; }
        public int CompanyId { get; set; }
        public Company Company { get; set; } = new Company();
        public string Name { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public List<Sitting> Sittings { get; set; } = new List<Sitting>();
        public List<Reservation> Reservations { get; set; } = new List<Reservation>();
        public List<SeatingArea> SeatingAreas { get; set; } = new List<SeatingArea>();
    }
}
