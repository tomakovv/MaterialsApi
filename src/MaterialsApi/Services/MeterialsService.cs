using AutoMapper;
using MaterialsApi.Data.DAL.Interfaces;
using MaterialsApi.Data.Entities;
using MaterialsApi.DTO.Materials;
using MaterialsApi.Exceptions;
using MaterialsApi.Services.Interfaces;

namespace MaterialsApi.Services
{
    public class MeterialsService : IMeterialsService
    {
        private readonly IMaterialsRepository _materialsRepository;
        private readonly IMaterialTypesRepository _materialTypesRepository;
        private readonly IAuthorRepository _authorRepository;
        private readonly IMapper _mapper;

        public MeterialsService(IMaterialsRepository materialsRepository, IMapper mapper, IMaterialTypesRepository materialTypesRepository, IAuthorRepository authorRepository)
        {
            _materialsRepository = materialsRepository;
            _materialTypesRepository = materialTypesRepository;
            _authorRepository = authorRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<MaterialDto>> GetAllAsync()
        {
            var materials = await _materialsRepository.GetAllWithMembersAsync();
            return _mapper.Map<IEnumerable<MaterialDto>>(materials);
        }

        public async Task<MaterialDto> GetByIdAsync(int id)
        {
            var material = await _materialsRepository.GetByConditionWithMembersAsync(m => m.Id == id);
            if (material == null)
                throw new NotFoundException("Material with provided Id does not exist");
            return _mapper.Map<MaterialDto>(material);
        }

        public async Task<MaterialDto> AddMaterialAsync(AddMaterialDto materialDto)
        {
            if (string.IsNullOrWhiteSpace(materialDto.Description) || string.IsNullOrWhiteSpace(materialDto.Location) || string.IsNullOrWhiteSpace(materialDto.Title))
                throw new BadRequestException("one of parameters was invalid");
            if (await _materialTypesRepository.GetSingleByConditionAsync(m => m.Id == materialDto.TypeId) == null)
                throw new BadRequestException("Invalid material type Id");
            if (await _authorRepository.GetSingleByConditionAsync(m => m.Id == materialDto.AuthorId) == null)
                throw new BadRequestException("Invalid author Id");

            var newMaterial = _mapper.Map<Material>(materialDto);
            var addedMaterial = await _materialsRepository.CreateAsync(newMaterial);
            return _mapper.Map<MaterialDto>(addedMaterial);
        }
    }
}