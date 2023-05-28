namespace ReservationApi.Models.Sitting
{
    public class UpdateGroupSittingModel
    {
        public int Id { get; set; }
        public int GroupId { get; set; }
        public DateTime Start { get; set; }
        public DateTime StartNew { get; set; }
        public DateTime End { get; set; }
        public DateTime EndNew { get; set; }
    }
}
