namespace PSMS_API.Dtos;

public class UserListDto
{
    public int UserId { get; set; }
    public string Username { get; set; } = "";
    public string? FullName { get; set; } = "";
    public string RoleName { get; set; } = "";
    public string? Contact {get; set;} = "";
    public bool IsActive { get; set; } = false;

}