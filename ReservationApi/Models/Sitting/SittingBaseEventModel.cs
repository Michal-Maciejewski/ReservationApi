namespace ReservationApi.Models.Sitting
{
    public class SittingBaseEventModel
    {
        public int Id { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public string Title { get; set; } = string.Empty;
        public string EventType { get; set; } = string.Empty;
        public string Notes { get; set; } = string.Empty;
    }
}
