using System.Text.RegularExpressions;
using Application.Models.User;  
using Core.Entities;
using Core.Enums;
using DataAccess.Repositories;
using OneOf;
using OneOf.Types;
using RailRideBMX.Middleware;


namespace Application.Services.Impl;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IJwtService _jwtService;
    

    public UserService(IUserRepository userRepository, IJwtService jwtService)
    {
        _userRepository = userRepository;
        _jwtService = jwtService;
    }

    public async Task<User> CreateUserAsync(UserResponseModel userResponseModel)
    {
        var conditionEmail = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
        Regex regEmail = new Regex(conditionEmail);
        if (!regEmail.IsMatch(userResponseModel.Email))
        {
            throw new BadRequestException("Cette adresse email est invalide !");
        }
        var condition = @"^(?=.*[A-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[#?!@$%^&*-])\S{7,15}$";
        Regex reg = new Regex(condition);
        if (!reg.IsMatch(userResponseModel.Password))
        {
            throw new BadRequestException("Le mot de passe doit contenir au moins 8 caractères, avec au moins une lettre majuscule, une lettre minuscule, un chiffre et un caractère spécial (#?!@$%^&*-).");
        }
        string passwordHash = BCrypt.Net.BCrypt.HashPassword(userResponseModel.Password);
        var user = new User()
        {
            Firstname = userResponseModel.Firstname,
            Lastname = userResponseModel.Lastname,
            Email = userResponseModel.Email,
            Password = passwordHash, 
            Role = UserRole.User
        };
        await _userRepository.CreateUser(user);
        return user;
    }

    public async Task<OneOf<string>> LoginUserAsync(UserLoginRequestModel userLoginRequestModel)
    {
        var user = new User()
        {
            Email = userLoginRequestModel.Email,
            Password = userLoginRequestModel.Password
        };
        var findUser = await _userRepository.FindByEmail(user);
        Console.WriteLine(user);
        if (findUser == null)
        {
            throw new NotFoundException("L'utilisateur n'a pas été trouvé");
        }
        bool passwordHash = BCrypt.Net.BCrypt.Verify(userLoginRequestModel.Password,findUser.Password);
        if (!passwordHash)
        {
            throw new BadRequestException("Le mot de passe est incorrect");
        }
        Console.WriteLine(findUser.Role.ToString());
        var token = _jwtService.GenerateToken(findUser.Id, findUser.Email, findUser.Role.ToString());
        return token;
    }

    public async Task<User> ForgotPassword(UserForgotPasswordRequestModel userForgotPassword)
    {
        var user = new User()
        {
            Email = userForgotPassword.Email
        };
        var findUser = await _userRepository.FindByEmail(user);
        if (findUser == null)
        {
            throw new NotFoundException("L'utilisateur n'a pas été trouvé");
        }
        return user;
    }
}   