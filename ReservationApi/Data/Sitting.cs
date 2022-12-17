using Mapster;

namespace ReservationApi.Data
{
    public class Sitting
    {
        public int Id { get; set; }
        public int RestaurantId { get; set; }
        public Restaurant Restaurant { get; set; } = new Restaurant();
        public int? GroupSittingId { get; set; } = null;
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public string Title { get; set; } = string.Empty;
        public string EventType { get; set; } = string.Empty;
        public string Notes { get; set; } = string.Empty;
    }
}
