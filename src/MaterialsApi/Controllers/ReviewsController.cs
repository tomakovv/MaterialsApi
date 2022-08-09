using MaterialsApi.DTO.Reviews;
using MaterialsApi.Services;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace MaterialsApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewsController : ControllerBase
    {
        private readonly IReviewService _reviewService;

        public ReviewsController(IReviewService reviewService)
        {
            _reviewService = reviewService;
        }

        [SwaggerOperation(Summary = "Get all reviews")]
        [HttpGet]
        public async Task<IActionResult> GetAllAsync() => Ok(await _reviewService.GetAllAsync());

        [SwaggerOperation(Summary = "Get review by id")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(int id) => Ok(await _reviewService.GetByIdAsync(id));

        [SwaggerOperation(Summary = "Add new review")]
        [HttpPost]
        public async Task<IActionResult> CreateAsync(AddReviewDto reviewDto)
        {
            var addedReview = await _reviewService.AddAsync(reviewDto);
            return Created($"api/Reviews/{addedReview.Id}", addedReview);
        }

        [SwaggerOperation(Summary = "Update specific review")]
        [HttpPut("{id}")]
        public async Task<IActionResult> EditAsync(int id, AddReviewDto editReview)
        {
            await _reviewService.EditAsync(id, editReview);
            return NoContent();
        }

        [SwaggerOperation(Summary = "Delete specific review")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            await _reviewService.DeleteAsync(id);
            return NoContent();
        }
    }
}