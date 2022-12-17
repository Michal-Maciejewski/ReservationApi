namespace ReservationApi.Models.Sitting
{
    public class CreateSittingModel
    {
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public string Title { get; set; } = string.Empty;
        public string EventType { get; set; } = string.Empty;
        public string Notes { get; set; } = string.Empty;
    }
}
