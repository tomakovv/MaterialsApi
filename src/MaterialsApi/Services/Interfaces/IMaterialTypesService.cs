using MaterialsApi.DTO.Materials;
using MaterialsApi.DTO.MaterialType;

namespace MaterialsApi.Services.Interfaces
{
    public interface IMaterialTypesService
    {
        Task<IEnumerable<MaterialTypeDto>> GetAllAsync();
        Task<IEnumerable<MaterialDto>> GetAllMaterialsByTypeId(int id);
    }
}