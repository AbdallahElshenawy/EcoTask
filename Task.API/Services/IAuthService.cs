using Task1.API.Models;

namespace Task1.API.Services
{
    public interface IAuthService
    {
        Task<AuthModel> RegisterAsync(RegisterModel model);
        Task<AuthModel> GetTokenAsync(TokenRequestModel model);
       Task<string> AddRoleAsync(AddRoleModel model);
    }
}
