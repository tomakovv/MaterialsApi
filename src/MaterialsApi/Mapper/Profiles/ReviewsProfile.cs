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
                  .ForMember(r => r.Material, opt => opt.MapFrom(x => x.Material.Title));
        }
    }
}