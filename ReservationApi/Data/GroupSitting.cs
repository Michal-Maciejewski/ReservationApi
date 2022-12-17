namespace ReservationApi.Data
{
    public class GroupSitting
    {
        public int Id { get; set; }
        public int RestaurantId { get; set; }
        public List<Sitting> Sittings { get; set; } = new List<Sitting>();
    }
}
