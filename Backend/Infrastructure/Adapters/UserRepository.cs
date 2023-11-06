using Core.Entities;
using DataAccess.Repositories;
using Infrastructure.DbContext;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Adapters;

public class UserRepository : IUserRepository
{

    private readonly ApplicationDbContext _context;

    public UserRepository(ApplicationDbContext context)
    {
        _context = context;
    }
    
    public async Task<User> CreateUser(User user)
    {
        _context.Users.Add(user);
        await _context.SaveChangesAsync();
        return user;
    }

    public async Task<User> LoginUser(User user)
    {
        var a = await _context.Users.FirstOrDefaultAsync(u => u.Email == user.Email && u.Password == user.Password);
        return a;
    }

    public async Task<User> FindByEmail(User user)
    {
        var a = await _context.Users.FirstOrDefaultAsync(u => u.Email == user.Email);
        return a;
    }
}