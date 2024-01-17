namespace Application.IServices;

public interface IJwtService
{
    public string GenerateToken(Guid userId, string email, string role);
    public bool VerifyToken(string token);
}