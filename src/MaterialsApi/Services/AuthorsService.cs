using AutoMapper;
using MaterialsApi.Data.DAL.Interfaces;
using MaterialsApi.DTO.Author;
using MaterialsApi.DTO.Materials;
using MaterialsApi.Exceptions;
using MaterialsApi.Services.Interfaces;

namespace MaterialsApi.Services
{
    public class AuthorsService : IAuthorsService
    {
        private readonly IAuthorRepository _authorRepository;
        private readonly IMapper _mapper;

        public AuthorsService(IAuthorRepository authorRepository, IMapper mapper)
        {
            _authorRepository = authorRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<AuthorDto>> GetAllAuthorsAsync()
        {
            var authors = await _authorRepository.GetAllWithMembersAsync();
            return _mapper.Map<IEnumerable<AuthorDto>>(authors);
        }

        public async Task<AuthorDto> GetByIdAsync(int id)
        {
            var author = await _authorRepository.GetSingleByConditionAsync(a => a.Id == id);
            if (author == null)
                throw new NotFoundException("Author with provided Id does not exist");
            return _mapper.Map<AuthorDto>(author);
        }

        public async Task<IEnumerable<AuthorDto>> GetTopRatedMaterialsAsync(int authorId)
        {
            throw new NotImplementedException();
            var author = await _authorRepository.GetSingleByParameterWithMembersAsync(a => a.Id == authorId);
            //author.CreatedMaterials.
        }

        public async Task<AuthorDto> GetMostProductiveAuthorAsync()
        {
            var authors = await _authorRepository.GetAllWithMembersAsync();
            var author = authors.MaxBy(m => m.NumberOfCreatedMaterials);
            return _mapper.Map<AuthorDto>(author);
        }
    }
}