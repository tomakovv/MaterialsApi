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
    public class MaterialTypeController : ControllerBase
    {
        private readonly IMaterialTypesService _materialTypesService;

        public MaterialTypeController(IMaterialTypesService materialTypesService)
        {
            _materialTypesService = materialTypesService;
        }

        [SwaggerOperation(Summary = "Get all material types")]
        [HttpGet]
        [Authorize(Roles = UserRoles.User)]
        public async Task<IActionResult> GetAllAsync() => Ok(await _materialTypesService.GetAllAsync());

        [SwaggerOperation(Summary = "Get all materials from specified material type")]
        [HttpGet("{id}/materials")]
        [Authorize(Roles = UserRoles.Admin)]
        public async Task<IActionResult> GetAllAuthorMaterialsAsync(int id) => Ok(await _materialTypesService.GetAllMaterialsByTypeId(id));
    }
}