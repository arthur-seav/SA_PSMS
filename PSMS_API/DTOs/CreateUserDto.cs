namespace PSMS_API.Dtos;

public class CreateUserDto
{
    public string Username { get; set; } = "";
    public string Password { get; set; } = "";   // 明文，進 service 後才 hash
    public string FullName { get; set; } = "";
    public int RoleId { get; set; }
    public string? Contact { get; set; }          // 可選，所以是 string?
}