using Core.Domain.Entity;

namespace Core.Ports;

public interface IUserRepository
{
    Task<User> CreateUser(User user);
    Task<User> LoginUser(User user);
    Task<User> FindByEmail(User user);
}