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
        private readonly ILogger<AuthorsService> _logger;

        public AuthorsService(IAuthorRepository authorRepository, IMapper mapper, ILogger<AuthorsService> logger)
        {
            _authorRepository = authorRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<IEnumerable<AuthorDto>> GetAllAuthorsAsync()
        {
            var authors = await _authorRepository.GetAllWithMembersAsync();
            _logger.LogInformation($"{authors.Count()} items successfully fetched");
            return _mapper.Map<IEnumerable<AuthorDto>>(authors);
        }

        public async Task<AuthorDto> GetByIdAsync(int id)
        {
            var author = await _authorRepository.GetSingleByConditionAsync(a => a.Id == id);
            if (author == null)
                throw new NotFoundException("Author with provided Id does not exist");
            _logger.LogInformation($"author with id: {id} successfully fetched");
            return _mapper.Map<AuthorDto>(author);
        }

        public async Task<IEnumerable<MaterialDto>> GetTopRatedMaterialsAsync(int authorId)
        {
            var author = await _authorRepository.GetSingleByParameterWithMembersAsync(a => a.Id == authorId);
            if (author == null)
                throw new NotFoundException("Author with provided Id does not exist");
            var materials = author.CreatedMaterials.Where(m => m.Reviews != null && m.Reviews.Any() && m.Reviews.Average(r => r.NumericRating) > 5).ToList();
            _logger.LogInformation($"top rated materials from author of id:{authorId} successfully fetched");
            return _mapper.Map<IEnumerable<MaterialDto>>(materials);
        }

        public async Task<AuthorDto> GetMostProductiveAuthorAsync()
        {
            var authors = await _authorRepository.GetAllWithMembersAsync();
            var author = authors.MaxBy(m => m.NumberOfCreatedMaterials);
            if (author == null)
                throw new NotFoundException("There are no Authors");
            _logger.LogInformation($"Most productive author successfully fetched");
            return _mapper.Map<AuthorDto>(author);
        }
    }
}