using AutoMapper;
using MaterialsApi.Data.Entities;
using MaterialsApi.DTO.Reviews;

namespace MaterialsApi.Mapper.Profiles
{
    public class ReviewsProfile : Profile
    {
        public ReviewsProfile()
        {
            CreateMap<Review, ReviewDto>()
                  .ForMember(r => r.MaterialId, opt => opt.MapFrom(x => x.MaterialId));
            CreateMap<Review, AddReviewDto>();
            CreateMap<AddReviewDto, Review>();
        }
    }
}