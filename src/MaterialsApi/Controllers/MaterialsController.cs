using MaterialsApi.DTO.Materials;
using MaterialsApi.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MaterialsApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MaterialsController : ControllerBase
    {
        private readonly IMeterialsService _meterialsService;

        public MaterialsController(IMeterialsService meterialsService)
        {
            _meterialsService = meterialsService;
        }

        [SwaggerOperation(Summary = "Get all materials")]
        [HttpGet]
        public async Task<IActionResult> GetAllAsync() => Ok(await _meterialsService.GetAllAsync());

        [SwaggerOperation(Summary = "Get material by id")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(int id) => Ok(await _meterialsService.GetByIdAsync(id));

        [SwaggerOperation(Summary = "Add new material")]
        [HttpPost]
        public async Task<IActionResult> CreateMaterialAsync(AddMaterialDto materialDto)
        {
            var addedMaterial = await _meterialsService.AddMaterialAsync(materialDto);
            return Created($"api/Materials/{addedMaterial.Id}", addedMaterial);
        }

        [SwaggerOperation(Summary = "Update specific material")]
        [HttpPut("{id}")]
        public async Task<IActionResult> EditAsync(int id, AddMaterialDto editMaterial)
        {
            await _meterialsService.EditMaterialAsync(id, editMaterial);
            return NoContent();
        }

        [SwaggerOperation(Summary = "Delete specific material")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            await _meterialsService.DeleteMaterialAsync(id);
            return NoContent();
        }
    }
}