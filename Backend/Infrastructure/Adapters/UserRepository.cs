﻿using Core.Domain.Entity;
using Core.Ports;
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
    public async Task<User> FindByEmailUser(string mail)
    {
        var userMail = await _context.Users.FirstOrDefaultAsync(u => u.Email == mail);
        return userMail;
    }
    public async Task UpdateAsync(User user)
    {
        _context.Users.Update(user);
        await _context.SaveChangesAsync();
    }

    public async Task SavePasswordResetTokenAsync(User user, string token, DateTime expires)
    {
        user.ResetPassWordToken = token;
        user.ResetPasswordTokenExpiration = expires;
        await _context.SaveChangesAsync();
    }
    public async Task<bool> ValidatePasswordResetTokenAsync(User user, string token)
    {
        return user.ResetPassWordToken == token && user.ResetPasswordTokenExpiration > DateTime.UtcNow;
    }
    
    public async Task CleanUpExpiredPasswordResetTokensAsync()
    {
        var expiredTokens = _context.Users.Where(u => u.ResetPasswordTokenExpiration <= DateTime.UtcNow);
        foreach (var user in expiredTokens)
        {
            user.ResetPassWordToken = null;
            user.ResetPasswordTokenExpiration = null;
        }

        await _context.SaveChangesAsync();
    }


}