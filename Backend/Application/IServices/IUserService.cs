using Application.Models.User;
using Core.Domain.Entity;
using OneOf;

namespace Application;

public interface IUserService
{
    Task<User> CreateUserAsync(UserResponseModel userResponseModel);
    Task<OneOf<string>> LoginUserAsync(UserLoginRequestModel userLoginRequestModel);
    Task RequestPasswordResetAsync(string email);
    Task<bool> ResetPasswordAsync(string email, string token, string newPassword);
}