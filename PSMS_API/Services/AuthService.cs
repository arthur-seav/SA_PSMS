using Microsoft.EntityFrameworkCore;
using PSMS_API.Data;
using PSMS_API.Dtos;
using PSMS_API.Models;

namespace PSMS_API.Services;

public class AuthService
{
    private readonly PsmsDbContext _db;
    private readonly JwtService _jwt;

    public AuthService(PsmsDbContext db, JwtService jwt)   // service 依賴 DB 和發 token 的服務
    {
        _db = db;
        _jwt = jwt;
    }

    // 帳密對 → 回 token；不對 → 回 null
    public async Task<string?> LoginAsync(LoginDto dto)
    {
        var user = await _db.Users
            .Include(u => u.Role)
            .FirstOrDefaultAsync(u => u.Username == dto.Username);

        if (user == null || !BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash))
            return null;

        return _jwt.GenerateToken(user.UserId, user.Username, user.Role.RoleName);
    }
    
    public async Task<int?> RegisterAsync(CreateUserDto dto)
    {
        // ① 查 username 是否已存在：問 DB「有沒有任何一筆符合條件」
        var exists = await _db.Users.AnyAsync(u => u.Username == dto.Username);
        if (exists)
            return null;   // 已存在 → 回 null，讓 controller 決定回 409
    
        // ② 把明文密碼 hash 掉
        var hash = BCrypt.Net.BCrypt.HashPassword(dto.Password);
    
        // ③ 組出要存的 User entity（只填需要給的欄位）
        var user = new User
        {
            Username = dto.Username,
            PasswordHash = hash,        // ← 存 hash，不是 dto.Password
            FullName = dto.FullName,
            RoleId = dto.RoleId,
            Contact = dto.Contact
        };
    
        // ④ 加進 DbContext 並存檔
        _db.Users.Add(user);
        await _db.SaveChangesAsync();
    
        return user.UserId;   // SaveChanges 後 DB 產生的主鍵自動回填
    }
    
    public async Task<List<RoleDto>> GetAllRolesAsync()
        {
            return await _db.Roles
                .Select(r => new RoleDto
                {
                    RoleId = r.RoleId,
                    RoleName = r.RoleName
                })
                .ToListAsync();
        }
    
        public async Task<bool> UpdateUserRoleAsync(int userId, UpdateUserRoleDto dto)
        {
            var user = await _db.Users.FindAsync(userId);
    
            if (user == null)
                return false;
    
            user.RoleId = dto.RoleId;
            await _db.SaveChangesAsync();
    
            return true;
        }
        
        // A：任何登入者改「自己的」密碼 —— 要驗舊密碼
            public async Task<bool> ChangeOwnPasswordAsync(int userId, ChangePasswordDto dto)
            {
                var user = await _db.Users.FindAsync(userId);
                if (user == null)
                    return false;
        
                // 先驗舊密碼對不對（證明是本人）
                if (!BCrypt.Net.BCrypt.Verify(dto.OldPassword, user.PasswordHash))
                    return false;
        
                // 舊密碼對 → 把新密碼 hash 後存進去
                user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.NewPassword);
                await _db.SaveChangesAsync();
        
                return true;
            }
        
            // B：Admin 重設「別人的」密碼 —— 不驗舊密碼
            public async Task<bool> ResetPasswordAsync(int userId, ResetPasswordDto dto)
            {
                var user = await _db.Users.FindAsync(userId);
                if (user == null)
                    return false;
        
                // 直接 hash 新密碼存進去（Admin 不需要知道舊密碼）
                user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.NewPassword);
                await _db.SaveChangesAsync();
        
                return true;
            }
}