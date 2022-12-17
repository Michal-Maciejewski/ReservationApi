namespace ReservationApi.Data
{
    public class Patron
    {
        public int Id { get; set; }
        public string? IdentityApiUserId { get; set; } = string.Empty;
        public IdentityApiUser? User { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
    }
}
