using Core.Entities;

namespace DataAccess.Repositories;

public interface IUserRepository
{
    Task<User> CreateUser(User user);
    Task<User> LoginUser(User user);
    Task<User> FindByEmail(User user);
}