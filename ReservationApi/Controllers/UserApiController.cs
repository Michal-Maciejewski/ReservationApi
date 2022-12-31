using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using ReservationApi.Data;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Mapster;
using SignInResult = Microsoft.AspNetCore.Identity.SignInResult;
using Microsoft.EntityFrameworkCore;
using ReservationApi.Models.User;
using MapsterMapper;
using ReservationApi.Contracts.Interfaces;

namespace ReservationApi.Controllers
{
    [Route("user")]
    [ApiController]
    [ApiVersion("1.0")]
    public class UserApiController : BaseApiController
    {
        private readonly IConfiguration _config;
        private readonly IUserManagerService _userManagerService;
        public UserApiController(IConfiguration config, IUserManagerService userManagerService, IMapper mapper, ILogger<UserApiController> logger) : base(mapper, logger)
        {
            _config = config;
            _userManagerService = userManagerService;
            _mapper = mapper;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var users = await _userManagerService.UserManager.Users.ToListAsync();
            var model = _mapper.Map<List<ReturnUserModel>>(users);

            return Ok(model);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="registerMemberModel"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("register-member")]
        public async Task<IActionResult> RegisterMember(RegisterMemberModel registerMemberModel)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if(await _userManagerService.UserManager.Users.AnyAsync(a => a.NormalizedEmail == registerMemberModel.Email.ToUpper()))
            {
                return BadRequest("User email already exists");
            }

            var userToCreate = registerMemberModel.Adapt<IdentityApiUser>();

            await _userManagerService.CreateUserAndAssignRole(userToCreate, registerMemberModel.Password, "Member");

            return Ok();
        }

        /// <summary>
        /// Deletes 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Authorize(Roles = "Member")]
        [Route("deletemember/{id=id}")]
        public async Task<IActionResult> DeleteMember(string id)
        {
            var user = await _userManagerService.UserManager.FindByIdAsync(id);
            
            if(user == null)
            {
                return NotFound("User does not exist");
            }
            var correctUser = await _userManagerService.UserManager.GetUserAsync(User);
            if(user.Id != correctUser.Id)
            {
                return BadRequest("Not the correct user");
            }

            return Ok();
        }

        /// <summary>
        /// Sign into the web API
        /// </summary>
        /// <remarks>
        /// Sample request
        /// 
        ///     POST user/signin
        ///     {
        ///         "email": "test@gmail.com",
        ///         "password": "ThisIsAPassword123"
        ///     }
        /// </remarks>
        /// <param name="logInModel"></param>
        /// <returns>The user token and information</returns>
        /// <response code="200">Returns signed in user credentials</response>
        [HttpPost]
        [AllowAnonymous]
        [Route("signin")]
        public async Task<IActionResult> LogIn(LogInModel logInModel)
        {
            SignInResult result =
                await _userManagerService.SignInManager.PasswordSignInAsync(logInModel.Email, logInModel.Password, true, false);

            if (!result.Succeeded)
            {
                return BadRequest("Invalid credentials");
            }

            IdentityApiUser user = await _userManagerService.UserManager.FindByEmailAsync(logInModel.Email);
            List<Claim> claims = new()
            {
                // Originally, this used strings with incorrect names as claim types
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Sub, _config["Jwt:Subject"]!),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString(CultureInfo.InvariantCulture)),
            };

            // Add all roles the user has
            var roles = await _userManagerService.UserManager.GetRolesAsync(user);
            claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

            SymmetricSecurityKey key = new(Encoding.UTF8.GetBytes(_config["Jwt:Key"]!));
            SigningCredentials sign = new(key, SecurityAlgorithms.HmacSha256);
            JwtSecurityToken token = new(
                _config["Jwt:Issuer"],
                _config["Jwt:Audience"],
                claims,
                expires: DateTime.UtcNow.AddDays(1),
                signingCredentials: sign
            );
            var jwt = new JwtSecurityTokenHandler().WriteToken(token);
            var userRole = (jwt, user, roles).Adapt<ReturnUserModel>();

            return Ok(userRole);
        }
    }
}
