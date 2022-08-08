using MaterialsApi.DTO.Materials;

namespace MaterialsApi.Services.Interfaces
{
    public interface IMeterialsService
    {
        Task<MaterialDto> AddMaterialAsync(AddMaterialDto materialDto);

        Task DeleteMaterialAsync(int id);

        Task EditMaterialAsync(int id, AddMaterialDto materialDto);

        Task<IEnumerable<MaterialDto>> GetAllAsync();

        Task<MaterialDto> GetByIdAsync(int id);
    }
}