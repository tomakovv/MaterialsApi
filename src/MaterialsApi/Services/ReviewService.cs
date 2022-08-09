using AutoMapper;
using MaterialsApi.Data.DAL.Interfaces;
using MaterialsApi.Data.Entities;
using MaterialsApi.DTO.Reviews;
using MaterialsApi.Exceptions;
using MaterialsApi.Services.Interfaces;

namespace MaterialsApi.Services
{
    public class ReviewService : IReviewService
    {
        private readonly IReviewsRepository _reviewsRepository;
        private readonly IMaterialsRepository _materialsRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<ReviewService> _logger;

        public ReviewService(IReviewsRepository authorRepository, IMapper mapper, IMaterialsRepository materialsRepository, ILogger<ReviewService> logger)
        {
            _reviewsRepository = authorRepository;
            _materialsRepository = materialsRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<IEnumerable<ReviewDto>> GetAllAsync()
        {
            var reviews = await _reviewsRepository.GetAllWithMembersAsync();
            _logger.LogInformation($"{reviews.Count()} items successfully fetched");
            return _mapper.Map<IEnumerable<ReviewDto>>(reviews);
        }

        public async Task<ReviewDto> GetByIdAsync(int id)
        {
            var review = await _reviewsRepository.GetByConditionWithMembersAsync(r => r.Id == id);
            if (review == null)
                throw new NotFoundException("Review with provided Id does not exist");
            _logger.LogInformation($"review with id: {id} successfully fetched");
            return _mapper.Map<ReviewDto>(review);
        }

        public async Task<ReviewDto> AddAsync(AddReviewDto reviewDto)
        {
            if (string.IsNullOrWhiteSpace(reviewDto.TextReview))
                throw new BadRequestException("Invalid TextReview");
            if (await _materialsRepository.GetSingleByConditionAsync(m => m.Id == reviewDto.MaterialId) == null)
                throw new BadRequestException("Invalid material Id");
            var newReview = _mapper.Map<Review>(reviewDto);
            var addedReview = await _reviewsRepository.CreateAsync(newReview);
            _logger.LogInformation($"new review added successfully ");
            return _mapper.Map<ReviewDto>(addedReview);
        }

        public async Task EditAsync(int id, AddReviewDto reviewDto)
        {
            var review = await _reviewsRepository.GetByConditionWithMembersAsync(r => r.Id == id);
            if (review == null)
                throw new NotFoundException("Review with provided Id does not exist");
            if (string.IsNullOrWhiteSpace(reviewDto.TextReview))
                throw new BadRequestException("Invalid TextReview");
            if (await _materialsRepository.GetSingleByConditionAsync(m => m.Id == reviewDto.MaterialId) == null)
                throw new BadRequestException("Invalid material Id");
            var updatedReview = _mapper.Map(reviewDto, review);
            await _reviewsRepository.UpdateAsync(updatedReview);
            _logger.LogInformation($"review updated successfully ");
        }

        public async Task DeleteAsync(int id)
        {
            var review = await _reviewsRepository.GetByConditionWithMembersAsync(r => r.Id == id);
            if (review == null)
                throw new NotFoundException("Review with provided Id does not exist");
            await _reviewsRepository.DeleteAsync(review);
            _logger.LogInformation($"review deleted successfully");
        }
    }
}