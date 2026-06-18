using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PSMS_API.Data;       // PsmsDbContext 在這
using PSMS_API.Dtos;
using PSMS_API.Services;   // JwtService 在這

namespace PSMS_API.Controllers;

[ApiController]
[Route("auth")]
public class AuthController : ControllerBase
{
    private readonly PsmsDbContext _db;
    private readonly JwtService _jwt;

    public AuthController(PsmsDbContext db, JwtService jwt)   // DI 注入 DB 和發 token 的服務
    {
        _db = db;
        _jwt = jwt;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginDto dto)
    {
        // ① 用 username 查 user，並把關聯的 Role 一起載入
        var user = await _db.Users
            .Include(u => u.Role)
            .FirstOrDefaultAsync(u => u.Username == dto.Username);

        // ②③ 查不到 user，或密碼不對 → 一律回同一個 401
        if (user == null ||
            !BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash))
        {
            return Unauthorized("帳號或密碼錯誤");
        }

        // ④ 通過 → 發 token
        var token = _jwt.GenerateToken(user.UserId, user.Username, user.Role.RoleName);
        return Ok(new { token });
    }
}