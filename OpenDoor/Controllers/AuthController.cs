using Microsoft.AspNetCore.Mvc;
using OpenDoor.Services;
using System.Threading.Tasks;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IJwtService _jwtService;

    public AuthController(IJwtService jwtService)
    {
        _jwtService = jwtService;
    }

    // POST: api/Auth/login
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginModel loginModel)
    {
        // Здесь логика для проверки пользователя и генерации токена
        var token = await _jwtService.Authenticate(loginModel);
        if (token == null)
        {
            return Unauthorized(new { message = "Неверные логин или пароль" });
        }

        return Ok(new { token });
    }
}
