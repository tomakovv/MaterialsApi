using MaterialsApi.Data.Entities.Identity;
using MaterialsApi.DTO.Register;
using MaterialsApi.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.Annotations;
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
        private readonly IIdentityService _identityService;

        public RegistrationController(IIdentityService identityService)
        {
            _identityService = identityService;
        }

        [SwaggerOperation(Summary = "Allows user to register")]
        [HttpPost]
        [Route("Register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register(RegisterDto newUser)
        {
            await _identityService.RegisterUserAsync(newUser);
            return Ok("Successfully added new user");
        }

        [SwaggerOperation(Summary = "Allows admin to register a new admin")]
        [HttpPost]
        [Route("RegisterAdmin")]
        [Authorize(Roles = UserRoles.Admin)]
        public async Task<IActionResult> RegisterAdmin(RegisterDto newAdmin)
        {
            await _identityService.RegisterAdminAsync(newAdmin);
            return Ok("Successfully added new Admin");
        }

        [SwaggerOperation(Summary = "Allows User or Admin to sign in")]
        [HttpPost]
        [Route("Login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login(RegisterDto login) => Ok(await _identityService.LoginAsync(login));
    }
}