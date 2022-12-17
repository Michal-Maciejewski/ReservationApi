namespace ReservationApi.Data
{
    public class SeatingArea
    {
        public int Id { get; set; }
        public int RestaurantId { get; set; }
        public Restaurant Restaurant { get; set; }
        public string Name { get; set; } = string.Empty;
        public List<Table> Tables { get; set; } = new List<Table>();
    }
}