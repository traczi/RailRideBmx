using Core.Domain.Enums;

namespace Core.Domain.Entity;

public class User
{
    public Guid Id { get; set; }
    public string Firstname { get; set; }
    public string Lastname { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public UserRole? Role { get; set; }
    public ICollection<Like> Like { get; set; }
}