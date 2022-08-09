using MaterialsApi.Data.Entities.Identity;
using MaterialsApi.DTO.Register;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MaterialsApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class RegistrationController : ControllerBase
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<User> _userManager;
        private readonly IConfiguration _configuration;

        public RegistrationController(UserManager<User> userManager, IConfiguration configuration, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _configuration = configuration;
            _roleManager = roleManager;
        }

        [HttpPost]
        [Route("Register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register(RegisterDto register)
        {
            var user = await _userManager.FindByNameAsync(register.Username);
            if (user != null)
                return BadRequest("User with provided username already exists");
            User newUser = new User()
            {
                UserName = register.Username,
            };
            var createdUser = await _userManager.CreateAsync(newUser, register.Password);
            if (!createdUser.Succeeded)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { result = "failed to register new user", Errors = createdUser.Errors.Select(x => x.Description) });
            }
            if (!await _roleManager.RoleExistsAsync(UserRoles.User))
                await (_roleManager).CreateAsync(new IdentityRole(UserRoles.User));
            await _userManager.AddToRoleAsync(newUser, UserRoles.User);
            return Ok(new { result = "Successfully added new user" });
        }

        [HttpPost]
        [Route("RegisterAdmin")]
        [Authorize(Roles = UserRoles.Admin)]
        public async Task<IActionResult> RegisterAdmin(RegisterDto register)
        {
            var user = await _userManager.FindByNameAsync(register.Username);
            if (user != null)
                return BadRequest("User with provided username already exists");
            User newUser = new User()
            {
                UserName = register.Username,
            };
            var createdUser = await _userManager.CreateAsync(newUser, register.Password);
            if (!createdUser.Succeeded)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { result = "failed to register new user", Errors = createdUser.Errors.Select(x => x.Description) });
            }
            if (!await _roleManager.RoleExistsAsync(UserRoles.Admin))
                await (_roleManager).CreateAsync(new IdentityRole(UserRoles.Admin));
            await _userManager.AddToRoleAsync(newUser, UserRoles.Admin);
            return Ok(new { result = "Successfully added new Admin" });
        }

        [HttpPost]
        [Route("Login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login(RegisterDto login)
        {
            var user = await _userManager.FindByNameAsync(login.Username);
            if (user != null || await _userManager.CheckPasswordAsync(user, login.Password))
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name,user.UserName)
                };

                var userRoles = await _userManager.GetRolesAsync(user);
                foreach (var role in userRoles)
                {
                    claims.Add(new Claim(ClaimTypes.Role, role));
                }
                var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));

                var token = new JwtSecurityToken(
                    expires: DateTime.Now.AddHours(1),
                    claims: claims,
                    signingCredentials: new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256)
                    );
                return Ok(new JwtSecurityTokenHandler().WriteToken(token));
            }
            return Unauthorized();
        }
    }
}