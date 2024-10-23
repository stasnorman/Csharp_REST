using Microsoft.EntityFrameworkCore;
using OpenDoor.Models;

namespace OpenDoor.Services
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext _context;

        public UserService(ApplicationDbContext context)
        {
            _context = context;
        }

        // Метод для проверки пользователя с использованием статических данных
        public async Task<User> ValidateUserAsync(string email, string password)
        {
            // Пример статических данных (логин: admin, пароль: admin)
            if (email == "admin" && password == "admin")
            {
                // Возвращаем объект пользователя с нужными данными
                return new User
                {
                    Id = 1,
                    Email = "admin@admin.ru",
                    Role = "Admin"
                };
            }

            // Если данные не совпадают, возвращаем null
            return null;
        }
    }
}
