using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PSMS_API.Dtos;
using PSMS_API.Services;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;


namespace PSMS_API.Controllers;

[ApiController]
[Route("users")]
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
    [Authorize]
    public async Task<IActionResult> ChangeMyPassword(ChangePasswordDto dto)
    {
        // confirm logged-in user's token by claims
        var myId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

        var ok = await _auth.ChangeOwnPasswordAsync(myId, dto);
        if (!ok)
            return BadRequest("Invalid Old Password.");

        return NoContent();
    }

    [HttpPut("{id}/password")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> ResetUserPassword(int id, ResetPasswordDto dto)
    {
        var ok = await _auth.ResetPasswordAsync(id, dto);
        if (!ok)
            return NotFound("User not found.");

        return NoContent();
    }
}