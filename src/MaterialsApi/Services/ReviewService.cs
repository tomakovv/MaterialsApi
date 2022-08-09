using AutoMapper;
using MaterialsApi.Data.DAL.Interfaces;
using MaterialsApi.Data.Entities;
using MaterialsApi.DTO.Reviews;
using MaterialsApi.Exceptions;

namespace MaterialsApi.Services
{
    public class ReviewService : IReviewService
    {
        private readonly IReviewsRepository _reviewsRepository;
        private readonly IMaterialsRepository _materialsRepository;
        private readonly IMapper _mapper;

        public ReviewService(IReviewsRepository authorRepository, IMapper mapper, IMaterialsRepository materialsRepository)
        {
            _reviewsRepository = authorRepository;
            _mapper = mapper;
            _materialsRepository = materialsRepository;
        }

        public async Task<IEnumerable<ReviewDto>> GetAllAsync()
        {
            var materials = await _reviewsRepository.GetAllWithMembersAsync();
            return _mapper.Map<IEnumerable<ReviewDto>>(materials);
        }

        public async Task<ReviewDto> GetByIdAsync(int id)
        {
            var review = await _reviewsRepository.GetByConditionWithMembersAsync(r => r.Id == id);
            if (review == null)
                throw new NotFoundException("Review with provided Id does not exist");
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
        }

        public async Task DeleteAsync(int id)
        {
            var review = await _reviewsRepository.GetByConditionWithMembersAsync(r => r.Id == id);
            if (review == null)
                throw new NotFoundException("Review with provided Id does not exist");
            await _reviewsRepository.DeleteAsync(review);
        }
    }
}