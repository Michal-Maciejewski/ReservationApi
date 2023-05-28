using System.Text.Json.Serialization;

namespace ReservationApi.Models.Sitting
{
    [JsonDerivedType(typeof(SittingEventModel))]
    [JsonDerivedType(typeof(SittingGroupEventModel))]
    public class SittingBaseEventModel
    {
        public int Id { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public string Title { get; set; } = string.Empty;
        public int SittingTypeId { get; set; }
        public string Notes { get; set; } = string.Empty;
        public bool Editable { get; set; } = false;
    }
}
