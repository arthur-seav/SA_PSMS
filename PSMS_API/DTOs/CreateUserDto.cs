namespace PSMS_API.Dtos;

public class CreateUserDto
{
    public string Username { get; set; } = "";
    public string Password { get; set; } = "";
    public string FullName { get; set; } = "";
    public int RoleId { get; set; }
    public string? Contact { get; set; }
}