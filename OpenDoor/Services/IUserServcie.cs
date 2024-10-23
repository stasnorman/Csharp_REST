using OpenDoor.Models;

public interface IUserService
{
    Task<User> ValidateUserAsync(string email, string password);
}
