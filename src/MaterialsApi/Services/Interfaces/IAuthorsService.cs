using MaterialsApi.DTO.Author;

namespace MaterialsApi.Services.Interfaces
{
    public interface IAuthorsService
    {
        Task<IEnumerable<AuthorDto>> GetAllAuthorsAsync();

        Task<AuthorDto> GetByIdAsync(int id);

        Task<AuthorDto> GetMostProductiveAuthorAsync();

        Task<IEnumerable<AuthorDto>> GetTopRatedMaterialsAsync(int authorId);
    }
}