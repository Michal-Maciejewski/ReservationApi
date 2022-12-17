using ReservationApi.Contracts.Interfaces;
using ReservationApi.Contracts.Services;
using System.Security.Principal;

namespace ReservationApi.Data.Seeding
{
    public class DbInitialiser
    {
        public IUserManagerService _userManagerService { get; set; }
        public ReservationApiDbContext _context { get; set; }
        public DbInitialiser(IUserManagerService userManagerService, ReservationApiDbContext context)
        {
            _userManagerService = userManagerService;
            _context = context;
        }
        public void Initialise()
        {
            _userManagerService.CreateRoles(new List<string> { "Manager", "Employee", "Patron" });
            var manager = new IdentityApiUser { Email = "Test@gmail.com", UserName = "Test@gmail.com", FirstName = "Test", LastName = "User" };
            _userManagerService.CreateUserAndAssignRoles(manager, "!Tt123", new List<string> { "Employee", "Manager" });
        }
    }
}
