using Microsoft.AspNetCore.Identity;
using ReservationApi.Data;
using System.Data;

namespace ReservationApi.Contracts.Interfaces
{
    public interface IUserManagerService
    {
        UserManager<IdentityApiUser> UserManager { get; }
        SignInManager<IdentityApiUser> SignInManager { get; }
        RoleManager<IdentityRole> RoleManager { get; }
        Task<IdentityApiUser> CreateUser(IdentityApiUser identityApiUser, string password);
        Task<IdentityApiUser> CreateUserAndAssignRole(IdentityApiUser identityApiUser, string password, string role);
        Task<IdentityApiUser> CreateUserAndAssignRoles(IdentityApiUser identityApiUser, string password, List<string> roles);
        Task<IdentityApiUser> AssignRole(IdentityApiUser user, string role);
        Task<IdentityApiUser> AssignRoles(IdentityApiUser user, List<string> roles);
        Task CreateRole(string role);
        Task CreateRoles(List<string> roles);
    }
}