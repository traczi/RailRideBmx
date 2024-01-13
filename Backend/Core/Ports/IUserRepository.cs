﻿using Core.Domain.Entity;

namespace Core.Ports;

public interface IUserRepository
{
    Task<User> CreateUser(User user);
    Task<User> LoginUser(User user);
    Task<User> FindByEmail(User user);
    Task<User> FindByEmailUser(string mail);
    Task UpdateAsync(User user);
    Task SavePasswordResetTokenAsync(User user, string token, DateTime expires);
    Task<bool> ValidatePasswordResetTokenAsync(User user, string token);
    Task CleanUpExpiredPasswordResetTokensAsync();
}