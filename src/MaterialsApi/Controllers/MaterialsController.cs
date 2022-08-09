using MaterialsApi.Data.Entities.Identity;
using MaterialsApi.DTO.Materials;
using MaterialsApi.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace MaterialsApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class MaterialsController : ControllerBase
    {
        private readonly IMeterialsService _meterialsService;

        public MaterialsController(IMeterialsService meterialsService)
        {
            _meterialsService = meterialsService;
        }

        [SwaggerOperation(Summary = "Get all materials")]
        [HttpGet]
        [Authorize(Roles = $"{UserRoles.Admin},{UserRoles.User}")]
        public async Task<IActionResult> GetAllAsync() => Ok(await _meterialsService.GetAllAsync());

        [SwaggerOperation(Summary = "Get material by id")]
        [HttpGet("{id}")]
        [Authorize(Roles = $"{UserRoles.Admin},{UserRoles.User}")]
        public async Task<IActionResult> GetByIdAsync(int id) => Ok(await _meterialsService.GetByIdAsync(id));

        [SwaggerOperation(Summary = "Add new material")]
        [HttpPost]
        [Authorize(Roles = UserRoles.Admin)]
        public async Task<IActionResult> CreateMaterialAsync(AddMaterialDto materialDto)
        {
            var addedMaterial = await _meterialsService.AddMaterialAsync(materialDto);
            return Created($"api/Materials/{addedMaterial.Id}", addedMaterial);
        }

        [SwaggerOperation(Summary = "Update specific material")]
        [HttpPut("{id}")]
        [Authorize(Roles = UserRoles.Admin)]
        public async Task<IActionResult> EditAsync(int id, AddMaterialDto editMaterial)
        {
            await _meterialsService.EditMaterialAsync(id, editMaterial);
            return NoContent();
        }

        [SwaggerOperation(Summary = "Delete specific material")]
        [HttpDelete("{id}")]
        [Authorize(Roles = UserRoles.Admin)]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            await _meterialsService.DeleteMaterialAsync(id);
            return NoContent();
        }
    }
}