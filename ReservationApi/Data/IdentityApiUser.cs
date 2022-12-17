using Mapster;
using Microsoft.AspNetCore.Identity;

namespace ReservationApi.Data
{
    //[AdaptTo("[name]Dto"), GenerateMapper]
    public class IdentityApiUser : IdentityUser
    {
        public bool Active { get; set; } = true;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
    }
}
