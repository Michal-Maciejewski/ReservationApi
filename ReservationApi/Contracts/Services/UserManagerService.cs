using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Query.Internal;
using ReservationApi.Contracts.Interfaces;
using ReservationApi.Data;
using System.Data;

namespace ReservationApi.Contracts.Services
{
    public class UserManagerService : IUserManagerService
    {
        private readonly ILogger<UserManagerService> _logger;
        public UserManager<IdentityApiUser> UserManager { get; }
        private readonly IUserStore<IdentityApiUser> _userStore;
        private readonly IUserEmailStore<IdentityApiUser> _emailStore;
        public RoleManager<IdentityRole> RoleManager { get; }
        public SignInManager<IdentityApiUser> SignInManager { get; }


        public UserManagerService(ILogger<UserManagerService> logger, UserManager<IdentityApiUser> userManager, IUserStore<IdentityApiUser> userStore, RoleManager<IdentityRole> roleManager, SignInManager<IdentityApiUser> signInManager)
        {
            _logger = logger;
            UserManager = userManager;
            _userStore = userStore;
            _emailStore = GetEmailStore();
            RoleManager = roleManager;
            SignInManager = signInManager;
        }

        private IUserEmailStore<IdentityApiUser> GetEmailStore()
        {
            if (!UserManager.SupportsUserEmail)
            {
                throw new NotSupportedException("The default UI requires a user store with email support.");
            }
            return (IUserEmailStore<IdentityApiUser>)_userStore;
        }

        public async Task<IdentityApiUser> CreateUser(IdentityApiUser identityApiUser, string password)
        {
            await _userStore.SetUserNameAsync(identityApiUser, identityApiUser.Email, CancellationToken.None);
            await _emailStore.SetEmailAsync(identityApiUser, identityApiUser.Email, CancellationToken.None);

            var result = await UserManager.CreateAsync(identityApiUser, password);

            if (result.Succeeded)
            {
                var code = await UserManager.GenerateEmailConfirmationTokenAsync(identityApiUser);

                await UserManager.ConfirmEmailAsync(identityApiUser, code);
            }
        
            return identityApiUser;
        }

        public async Task<IdentityApiUser> CreateUserAndAssignRole(IdentityApiUser identityApiUser, string password, string role)
        {
            identityApiUser = await CreateUser(identityApiUser, password);
            await AssignRole(identityApiUser, role);
            return identityApiUser;
        }

        public async Task<IdentityApiUser> CreateUserAndAssignRoles(IdentityApiUser identityApiUser, string password, List<string> roles)
        {
            identityApiUser = await CreateUser(identityApiUser, password);
            await AssignRoles(identityApiUser, roles);
            return identityApiUser;
        }

        public async Task CreateRole(string role)
        {
            if(!await RoleManager.RoleExistsAsync(role))
            {
                await RoleManager.CreateAsync(new IdentityRole(role));
            }
        }

        public async Task CreateRoles(List<string> roles)
        {
            foreach (var role in roles)
            {
                if (!await RoleManager.RoleExistsAsync(role))
                {
                    await RoleManager.CreateAsync(new IdentityRole(role));
                }
            }
        }

        public async Task<IdentityApiUser> AssignRole(IdentityApiUser user, string role)
        {
            var result = await UserManager.AddToRoleAsync(user, role);
            if (result.Succeeded)
            {
                return user;
            }
            else
            {
                throw new Exception();
            }
        }

        public async Task<IdentityApiUser> AssignRoles(IdentityApiUser user, List<string> roles)
        {
            var result = await UserManager.AddToRolesAsync(user, roles);
            if(result.Succeeded)
            {
                return user;
            }
            else
            {
                throw new Exception();
            }
        }
    }
}
