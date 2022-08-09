using MaterialsApi.DTO.Reviews;

namespace MaterialsApi.Services.Interfaces
{
    public interface IReviewService
    {
        Task<ReviewDto> AddAsync(AddReviewDto reviewDto);
        Task DeleteAsync(int id);
        Task EditAsync(int id, AddReviewDto reviewDto);
        Task<IEnumerable<ReviewDto>> GetAllAsync();
        Task<ReviewDto> GetByIdAsync(int id);
    }
}