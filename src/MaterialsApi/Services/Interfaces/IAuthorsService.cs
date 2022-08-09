using MaterialsApi.DTO.Author;
using MaterialsApi.DTO.Materials;

namespace MaterialsApi.Services.Interfaces
{
    public interface IAuthorsService
    {
        Task<IEnumerable<AuthorDto>> GetAllAuthorsAsync();

        Task<AuthorDto> GetByIdAsync(int id);

        Task<AuthorDto> GetMostProductiveAuthorAsync();

        Task<IEnumerable<MaterialDto>> GetTopRatedMaterialsAsync(int authorId);
    }
}