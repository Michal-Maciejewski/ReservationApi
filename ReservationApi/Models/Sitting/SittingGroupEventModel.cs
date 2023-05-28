using System.Text.Json.Serialization;

namespace ReservationApi.Models.Sitting
{
    public class SittingGroupEventModel : SittingBaseEventModel
    {
        [JsonPropertyName("groupId")]
        public new int GroupSittingId { get; set; }
    }
}
