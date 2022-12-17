namespace ReservationApi.Data
{
    public class Table
    {
        public int Id { get; set; }
        public int SeatingAreaId { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Seats { get; set; }
        public double X { get; set; }
        public double Y { get; set; }
    }
}