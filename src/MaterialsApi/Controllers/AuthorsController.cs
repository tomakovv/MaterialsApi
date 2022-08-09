using MaterialsApi.Data.Entities.Identity;
using MaterialsApi.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace MaterialsApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AuthorsController : ControllerBase
    {
        private readonly IAuthorsService _authorsService;

        public AuthorsController(IAuthorsService authorsService)
        {
            _authorsService = authorsService;
        }

        [SwaggerOperation(Summary = "Get all Authors")]
        [HttpGet]
        [Authorize(Roles = UserRoles.User)]
        public async Task<IActionResult> GetAllAsync() => Ok(await _authorsService.GetAllAuthorsAsync());

        [SwaggerOperation(Summary = "Get Author by id")]
        [HttpGet("{id}")]
        [Authorize(Roles = UserRoles.User)]
        public async Task<IActionResult> GetByIdAsync(int id) => Ok(await _authorsService.GetByIdAsync(id));

        [SwaggerOperation(Summary = "Get all material from specified author with avg rating above 5")]
        [HttpGet("{id}/topRated")]
        [Authorize(Roles = UserRoles.Admin)]
        public async Task<IActionResult> GetTopRatedMateerialsAsync(int id) => throw new NotImplementedException();

        [SwaggerOperation(Summary = "Get most productive author")]
        [Authorize(Roles = UserRoles.Admin)]
        [HttpGet("MostProductive")]
        public async Task<IActionResult> GetMostProductiveAuthorAsync() => Ok(await _authorsService.GetMostProductiveAuthorAsync());
    }
}