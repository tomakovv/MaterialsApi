using AutoMapper;
using MaterialsApi.Data.DAL.Interfaces;
using MaterialsApi.DTO.Materials;
using MaterialsApi.DTO.MaterialType;
using MaterialsApi.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace MaterialsApi.Services
{
    public class MaterialTypesService : IMaterialTypesService
    {
        private readonly IMaterialTypesRepository _materialTypesRepository;
        private readonly IMaterialsRepository _materialsRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<MaterialTypesService> _logger;

        public MaterialTypesService(IMaterialTypesRepository materialTypesRepository, IMaterialsRepository materialsRepository, IMapper mapper, ILogger<MaterialTypesService> logger)
        {
            _materialTypesRepository = materialTypesRepository;
            _materialsRepository = materialsRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<IEnumerable<MaterialTypeDto>> GetAllAsync()
        {
            var types = await _materialTypesRepository.GetAll().ToListAsync();
            _logger.LogInformation($"{types.Count()} items successfully fetched");
            return _mapper.Map<IEnumerable<MaterialTypeDto>>(types);
        }

        public async Task<IEnumerable<MaterialDto>> GetAllMaterialsByTypeIdAsync(int id)
        {
            var materials = await _materialsRepository.GetAll().Where(x => x.TypeId == id).ToListAsync();
            _logger.LogInformation($"Materials with Typeid: {id} successfully fetched");
            return _mapper.Map<IEnumerable<MaterialDto>>(materials);
        }
    }
}