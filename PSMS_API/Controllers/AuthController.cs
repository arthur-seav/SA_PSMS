using Microsoft.AspNetCore.Mvc;
using PSMS_API.Dtos;
using PSMS_API.Services;

namespace PSMS_API.Controllers;

[ApiController]
[Route("auth")]
public class AuthController : ControllerBase
{
    private readonly AuthService _auth;

    public AuthController(AuthService auth)
    {
        _auth = auth;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginDto dto)
    {
        var token = await _auth.LoginAsync(dto);
        if (token == null)
            return Unauthorized("Invalid Credentials");

        return Ok(new { token });
    }
    
    
    
}