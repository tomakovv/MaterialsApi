using MaterialsApi.Data.Entities.Identity;
using MaterialsApi.DTO.Register;
using MaterialsApi.Exceptions;
using MaterialsApi.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MaterialsApi.Services
{
    public class IdentityService : IIdentityService
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<User> _userManager;
        private readonly IConfiguration _configuration;
        private readonly ILogger<IdentityService> _logger;

        public IdentityService(RoleManager<IdentityRole> roleManager, UserManager<User> userManager, IConfiguration configuration, ILogger<IdentityService> logger)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _configuration = configuration;
            _logger = logger;
        }

        public async Task RegisterUserAsync(RegisterDto userDto)
        {
            var user = await _userManager.FindByNameAsync(userDto.Username);
            if (user != null)
                throw new BadRequestException("User with provided username already exists");
            User newUser = new User()
            {
                UserName = userDto.Username,
            };
            var createdUser = await _userManager.CreateAsync(newUser, userDto.Password);
            if (!createdUser.Succeeded)
                throw new BadRequestException($"failed to register new User Error:{createdUser.Errors.Select(x => x.Description)}");
            if (!await _roleManager.RoleExistsAsync(UserRoles.User))
                await (_roleManager).CreateAsync(new IdentityRole(UserRoles.User));
            await _userManager.AddToRoleAsync(newUser, UserRoles.User);
            _logger.LogInformation("successfully created new user");
        }

        public async Task RegisterAdminAsync(RegisterDto userDto)
        {
            var user = await _userManager.FindByNameAsync(userDto.Username);
            if (user != null)
                throw new BadRequestException("User with provided username already exists");
            User newUser = new User()
            {
                UserName = userDto.Username,
            };
            var createdUser = await _userManager.CreateAsync(newUser, userDto.Password);
            if (!createdUser.Succeeded)
                throw new BadRequestException($"failed to register new Admin ");
            if (!await _roleManager.RoleExistsAsync(UserRoles.Admin))
                await (_roleManager).CreateAsync(new IdentityRole(UserRoles.Admin));
            await _userManager.AddToRoleAsync(newUser, UserRoles.Admin);
            _logger.LogInformation("successfully created new admin");
        }

        public async Task<string> LoginAsync(RegisterDto login)
        {
            var user = await _userManager.FindByNameAsync(login.Username);
            if (user != null && await _userManager.CheckPasswordAsync(user, login.Password))
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
                _logger.LogInformation("successfully log in");
                return new JwtSecurityTokenHandler().WriteToken(token);
            }
            throw new UnauthorizedAccessException("User does not exist or password is incorrect");
        }
    }
}