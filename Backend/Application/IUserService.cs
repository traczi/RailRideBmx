using Application.Models.User;
using Core.Entities;
using OneOf;

namespace Application.Services;

public interface IUserService
{
    Task<User> CreateUserAsync(UserResponseModel userResponseModel);
    Task<OneOf<string>> LoginUserAsync(UserLoginRequestModel userLoginRequestModel);
    Task<User> ForgotPassword(UserForgotPasswordRequestModel userForgotPassword);
}