using Duende.IdentityServer.Models;

namespace ReservationApi.Data
{
    public class Reservation
    {
        public int Id { get; set; }
        public int SittingId { get; set; }
        public Sitting Sitting { get; set; }
        public int PatronId { get; set; }
        public Patron Patron { get; set; }
        public int NumberOfGuests { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public int ReservationStatusId { get; set; }
        public ReservationStatus ReservationStatus { get; set; }
        public int ReservationSourceId { get; set; }
        public ReservationSource ReservationSource { get; set; }
        public string? Notes { get; set; }

    }
}