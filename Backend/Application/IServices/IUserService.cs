using Application.Models.User;
using Core.Domain.Entity;
using OneOf;

namespace Application.IServices;

public interface IUserService
{
    Task<User> CreateUserAsync(UserResponseModel userResponseModel);
    Task<OneOf<string>> LoginUserAsync(UserLoginRequestModel userLoginRequestModel);
    Task RequestPasswordResetAsync(string email);
    Task<bool> ResetPasswordAsync(string email, string token, string newPassword);
    Task<List<User>> GetAllUserAsync();
    Task UpdateUserEmailAsync(Guid userId, string newEmail);
    Task UpdateUserNameAsync(Guid userId, UserModifyNameModel model);
    Task ChangeUserPasswordAsync(Guid userId, string currentPassword, string newPassword);
    Task<User> GetUserById(Guid userId);
}