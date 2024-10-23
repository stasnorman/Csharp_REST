using OpenDoor.Models;

namespace OpenDoor.Services
{
    public interface IJwtService
    {
        string GenerateToken(User user);
        bool ValidateToken(string token);
        Task<string> Authenticate(LoginModel loginModel);

    }
}
