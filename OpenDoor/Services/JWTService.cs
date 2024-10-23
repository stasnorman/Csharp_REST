using Microsoft.IdentityModel.Tokens;
using OpenDoor.Models;
using OpenDoor.Services;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

public class JwtService : IJwtService
{
    private readonly IConfiguration _configuration;
    private readonly IUserService _userService; // Сервис для работы с пользователями

    public JwtService(IConfiguration configuration, IUserService userService)
    {
        _configuration = configuration;
        _userService = userService;  // Инициализируем сервис работы с пользователями
    }

    public async Task<string> Authenticate(LoginModel loginModel)
    {
        // Проверяем пользователя по email и паролю
        var user = await _userService.ValidateUserAsync(loginModel.Email, loginModel.Password);

        if (user == null)
        {
            // Если пользователь не найден, возвращаем null
            return null;
        }

        // Если пользователь найден, генерируем для него JWT-токен
        return GenerateToken(user);
    }

    public string GenerateToken(User user)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Role),
                new Claim(JwtRegisteredClaimNames.Aud, _configuration["Jwt:Audience"]),
                new Claim(JwtRegisteredClaimNames.Iss, _configuration["Jwt:Issuer"])
            }),
            Expires = DateTime.UtcNow.AddHours(1),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }

    public bool ValidateToken(string token)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]);

        tokenHandler.ValidateToken(token, new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(key),
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidIssuer = _configuration["Jwt:Issuer"], // Издатель должен совпадать с тем, что указано в конфигурации
            ValidAudience = _configuration["Jwt:Audience"], // Аудитория должна совпадать с тем, что указано в конфигурации
            ValidateLifetime = true
        }, out SecurityToken validatedToken);

        return validatedToken != null;
    }
}
