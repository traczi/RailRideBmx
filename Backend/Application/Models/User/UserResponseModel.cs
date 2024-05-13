using Core.Domain.Enums;

namespace Application.Models.User;

public class UserResponseModel : BaseResponseModel
{
    public string Firstname { get; set; }
    public string Lastname { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public UserRole? Role { get; set; }
    
}