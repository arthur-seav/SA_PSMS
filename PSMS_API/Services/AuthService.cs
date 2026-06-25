using Microsoft.EntityFrameworkCore;
using PSMS_API.Data;
using PSMS_API.Dtos;
using PSMS_API.Models;

namespace PSMS_API.Services;

public class AuthService
{
    private readonly PsmsDbContext _db;
    private readonly JwtService _jwt;

    public AuthService(PsmsDbContext db, JwtService jwt)   // service depend on DB and jwt service
    {
        _db = db;
        _jwt = jwt;
    }
    
    // Log in
    // if user logged-in return token, else return null
    public async Task<string?> LoginAsync(LoginDto dto)
    {
        var user = await _db.Users
            .Include(u => u.Role)
            .FirstOrDefaultAsync(u => u.Username == dto.Username);

        if (user == null || !BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash))
            return null;

        return _jwt.GenerateToken(user.UserId, user.Username, user.Role.RoleName);
    }
    
    // Register Account
    public async Task<int?> RegisterAsync(CreateUserDto dto)
    {
        // verify exist acc
        var exists = await _db.Users.AnyAsync(u => u.Username == dto.Username);
        if (exists)
            return null;   // controller reply 409
    
        // hash password
        var hash = BCrypt.Net.BCrypt.HashPassword(dto.Password);
    
        var user = new User
        {
            Username = dto.Username,
            PasswordHash = hash,
            FullName = dto.FullName,
            RoleId = dto.RoleId,
            Contact = dto.Contact
        };
        
        _db.Users.Add(user);
        await _db.SaveChangesAsync();
    
        return user.UserId;
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
        
        // change own password, need verify old password
            public async Task<bool> ChangeOwnPasswordAsync(int userId, ChangePasswordDto dto)
            {
                var user = await _db.Users.FindAsync(userId);
                if (user == null)
                    return false;
        
                // verify old password
                if (!BCrypt.Net.BCrypt.Verify(dto.OldPassword, user.PasswordHash))
                    return false;
        
                // hash new password then override into db
                user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.NewPassword);
                await _db.SaveChangesAsync();
        
                return true;
            }
        
            // Admin reset others password without verify old password
            public async Task<bool> ResetPasswordAsync(int userId, ResetPasswordDto dto)
            {
                var user = await _db.Users.FindAsync(userId);
                if (user == null)
                    return false;
        
                // hash new password then override into db
                user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.NewPassword);
                await _db.SaveChangesAsync();
        
                return true;
            }
}