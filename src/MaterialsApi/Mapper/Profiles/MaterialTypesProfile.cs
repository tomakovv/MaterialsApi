using AutoMapper;
using MaterialsApi.Data.Entities;
using MaterialsApi.DTO.MaterialType;

namespace MaterialsApi.Mapper.Profiles
{
    public class MaterialTypesProfile : Profile
    {
        public MaterialTypesProfile()
        {
            CreateMap<MaterialType, MaterialTypeDto>();
        }
    }
}