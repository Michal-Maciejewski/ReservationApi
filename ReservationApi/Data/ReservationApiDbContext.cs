using Duende.IdentityServer.EntityFramework.Options;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace ReservationApi.Data
{
    public class ReservationApiDbContext : IdentityDbContext<IdentityApiUser>
    {
        public DbSet<IdentityApiUser> IdentityApiUsers { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<Restaurant> Restaurants { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Patron> Patrons { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<GroupSitting> GroupSittings { get; set; }
        public DbSet<Sitting> Sittings { get; set; }
        public DbSet<SeatingArea> SeatingAreas { get; set; }
        public DbSet<Table> Tables { get; set; }
        public ReservationApiDbContext(DbContextOptions<ReservationApiDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<IdentityRole>().HasData(new IdentityRole { Name = "Employee", NormalizedName = "Employee".ToUpper() }, new IdentityRole { Name = "Manager", NormalizedName = "Manager".ToUpper() }, new IdentityRole { Name = "Patron", NormalizedName = "Patron".ToUpper() });
        }
    }
}
