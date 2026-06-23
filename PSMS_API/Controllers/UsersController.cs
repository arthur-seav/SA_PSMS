using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PSMS_API.Dtos;
using PSMS_API.Services;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;


namespace PSMS_API.Controllers;

// Only Admin can access this controller.
[ApiController]
[Route("users")]
// [Authorize(Roles = "Admin")]
public class UsersController : ControllerBase
{
    private readonly AuthService _auth;

    public UsersController(AuthService auth)
    {
        _auth = auth;
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> CreateUser(CreateUserDto dto)
    {
        var newId = await _auth.RegisterAsync(dto);

        if (newId == null)
            return Conflict("Duplicated Username.");   // 409

        return CreatedAtAction(null, new { id = newId }, new { userId = newId });  // 201
    }
    
    [HttpGet("/roles")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> GetRoles()
    {
        var roles = await _auth.GetAllRolesAsync();
        return Ok(roles);
    }

    [HttpPut("{id}/role")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> UpdateUserRole(int id, UpdateUserRoleDto dto)
    {
        var ok = await _auth.UpdateUserRoleAsync(id, dto);

        if (!ok)
            return NotFound("Could not found user.");

        return NoContent();
    }
    
    [HttpPut("me/password")]
    [Authorize]                          // ← 注意：不是 Admin only，任何登入者都能改自己的
    public async Task<IActionResult> ChangeMyPassword(ChangePasswordDto dto)
    {
        // 從 token 的 claim 讀出「我是誰」
        var myId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

        var ok = await _auth.ChangeOwnPasswordAsync(myId, dto);
        if (!ok)
            return BadRequest("舊密碼錯誤");   // 400

        return NoContent();                  // 204
    }

    [HttpPut("{id}/password")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> ResetUserPassword(int id, ResetPasswordDto dto)
    {
        var ok = await _auth.ResetPasswordAsync(id, dto);
        if (!ok)
            return NotFound("找不到這個使用者");  // 404

        return NoContent();                  // 204
    }
}