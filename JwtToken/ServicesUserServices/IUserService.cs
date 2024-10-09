using JwtToken.Models;

namespace JwtToken.ServicesUserServices
{
    public interface IUserService
    {
        Task<string> RegisterAsync(CreateUserDto createUserDto);
        Task<bool> Login(TokenRequestModel model);

        Task<string> AddRoleAsync(CreateRoleDto createRoleDto);

        Task<string> GetAccessToken(AppUserClass appUser);
    }
}
