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

        // PUT api/<MaterialsController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<MaterialsController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}