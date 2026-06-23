namespace PSMS_API.Dtos;

// Admin reset password for staff, without verify staff's old password.
public class ResetPasswordDto
{
    public string NewPassword { get; set; } = "";
}

// Staff(logged-in) change their own password with verify old password.
public class ChangePasswordDto
{
    public string OldPassword { get; set; } = "";
    public string NewPassword { get; set; } = "";
}