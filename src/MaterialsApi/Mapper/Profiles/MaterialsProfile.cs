using AutoMapper;
using MaterialsApi.Data.Entities;
using MaterialsApi.DTO.Materials;

namespace MaterialsApi.Mapper.Profiles
{
    public class MaterialsProfile : Profile
    {
        public MaterialsProfile()
        {
            CreateMap<Material, MaterialDto>()
                .ForMember(m => m.AuthorId, opt => opt.MapFrom(x => x.AuthorId))
                .ForMember(m => m.TypeId, opt => opt.MapFrom(x => x.TypeId))
                .ForMember(m => m.TextReviews, opt => opt.MapFrom(x => x.Reviews.Select(s => s.TextReview)))
                .ForMember(m => m.NumericRating, opt => opt.MapFrom(x => x.Reviews.Select(s => s.NumericRating)))
                .ForMember(m => m.PublishDate, opt => opt.MapFrom(x => x.PublishDate.ToShortDateString()));
            CreateMap<AddMaterialDto, Material>();
        }
    }
}