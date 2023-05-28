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

            var sittingType1 = new SittingType { Name = "Breakfast" };
            var sittingType2 = new SittingType { Name = "Lunch" };
            var sittingType3 = new SittingType { Name = "Dinner" };
            _context.SittingTypes.AddRange(sittingType1, sittingType2, sittingType3);

            var sittings = new List<Sitting>()
            {
                new Sitting { Id = 1, Start = DateTime.Now.Date.AddHours(7), End = DateTime.Now.Date.AddHours(9), SittingType = sittingType1, Notes = "Hehe", Title = "Title1" },
                new Sitting { Id = 2, GroupSittingId = 1, Start = DateTime.Now.Date.AddDays(1).AddHours(7), End = DateTime.Now.Date.AddDays(1).AddHours(9), SittingType = sittingType1, Notes = "Hehe", Title = "Title1" },
                new Sitting { Id = 3, GroupSittingId = 1, Start = DateTime.Now.Date.AddDays(2).AddHours(7), End = DateTime.Now.Date.AddDays(2).AddHours(9), SittingType = sittingType1, Notes = "Hehe", Title = "Title1" },
                new Sitting { Id = 4, GroupSittingId = 2, Start = DateTime.Now.Date.AddDays(3).AddHours(7), End = DateTime.Now.Date.AddDays(3).AddHours(9), SittingType = sittingType1, Notes = "Hehe", Title = "Title1" },
                new Sitting { Id = 5, GroupSittingId = 2, Start = DateTime.Now.Date.AddDays(4).AddHours(7), End = DateTime.Now.Date.AddDays(4).AddHours(9), SittingType = sittingType1, Notes = "Hehe", Title = "Title1" },
                new Sitting { Id = 6, GroupSittingId = 3, Start = DateTime.Now.Date.AddDays(-1).AddHours(7), End = DateTime.Now.Date.AddDays(-1).AddHours(9), SittingType = sittingType1, Notes = "Hehe", Title = "Title1" },
                new Sitting { Id = 7, GroupSittingId = 3, Start = DateTime.Now.Date.AddDays(-2).AddHours(7), End = DateTime.Now.Date.AddDays(-2).AddHours(9), SittingType = sittingType1, Notes = "Hehe", Title = "Title1" },
                new Sitting { Id = 8, GroupSittingId = 3, Start = DateTime.Now.Date.AddDays(-3).AddHours(7), End = DateTime.Now.Date.AddDays(-3).AddHours(9), SittingType = sittingType1, Notes = "Hehe", Title = "Title1" },
                new Sitting { Id = 9, GroupSittingId = 3, Start = DateTime.Now.Date.AddDays(-4).AddHours(7), End = DateTime.Now.Date.AddDays(-4).AddHours(9), SittingType = sittingType1, Notes = "Hehe", Title = "Title1" },
                new Sitting { Id = 10, GroupSittingId = 3, Start = DateTime.Now.Date.AddDays(-5).AddHours(7), End = DateTime.Now.Date.AddDays(-5).AddHours(9), SittingType = sittingType1, Notes = "Hehe", Title = "Title1" },                new Sitting { Id = 11, GroupSittingId = 3, Start = DateTime.Now.Date.AddDays(-6).AddHours(7), End = DateTime.Now.Date.AddDays(-6).AddHours(9), SittingType = sittingType1, Notes = "Hehe", Title = "Title1" },
                new Sitting { Id = 12, GroupSittingId = 3, Start = DateTime.Now.Date.AddDays(-7).AddHours(7), End = DateTime.Now.Date.AddDays(-7).AddHours(9), SittingType = sittingType1, Notes = "Hehe", Title = "Title1" },
                new Sitting { Id = 13, GroupSittingId = 3, Start = DateTime.Now.Date.AddDays(-8).AddHours(7), End = DateTime.Now.Date.AddDays(-8).AddHours(9), SittingType = sittingType1, Notes = "Hehe", Title = "Title1" },
                new Sitting { Id = 14, GroupSittingId = 3, Start = DateTime.Now.Date.AddHours(7), End = DateTime.Now.Date.AddHours(9), SittingType = sittingType1, Notes = "Hehe", Title = "Title1" }
            };
            _context.Sittings.AddRange(sittings);
            _context.SaveChanges();
        }
    }
}
