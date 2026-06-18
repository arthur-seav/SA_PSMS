using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace PSMS_API.Services;

public class JwtService
{
    private readonly IConfiguration _config;

    public JwtService(IConfiguration config)   // DI 把設定檔注入進來
    {
        _config = config;
    }

    public string GenerateToken(int userId, string username, string role)
    {
        // ① claims：這個 token 要帶哪些資訊
        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, userId.ToString()),
            new Claim("username", username),
            new Claim(ClaimTypes.Role, role)        // ← 這個很關鍵，見下方說明
        };

        // ② 從 appsettings 拿 Key
        var key = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(_config["Jwt:Key"]!));

        // ③ key + 演算法 = 簽章憑證
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        // ④ 組 token
        var token = new JwtSecurityToken(
            issuer: _config["Jwt:Issuer"],
            audience: _config["Jwt:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(
                int.Parse(_config["Jwt:ExpiryMinutes"]!)),
            signingCredentials: creds
        );

        // ⑤ 序列化成字串
        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}