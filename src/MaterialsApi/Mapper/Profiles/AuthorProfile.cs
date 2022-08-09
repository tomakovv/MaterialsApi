using AutoMapper;
using MaterialsApi.Data.Entities;
using MaterialsApi.DTO.Author;

namespace MaterialsApi.Mapper.Profiles
{
    public class AuthorProfile : Profile
    {
        public AuthorProfile()
        {
            CreateMap<Author, AuthorDto>();
        }
    }
}