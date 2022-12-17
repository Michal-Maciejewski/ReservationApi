namespace ReservationApi.Data
{
    public class Employee
    {
        public int Id { get; set; }
        public string? IdentityApiUserId { get; set; } = string.Empty;
        public IdentityApiUser? User { get; set; }
    }
}
