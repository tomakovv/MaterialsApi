using MaterialsApi.DTO.Register;

namespace MaterialsApi.Services.Interfaces
{
    public interface IIdentityService
    {
        Task<string> LoginAsync(RegisterDto login);
        Task RegisterAdminAsync(RegisterDto userDto);
        Task RegisterUserAsync(RegisterDto userDto);
    }
}