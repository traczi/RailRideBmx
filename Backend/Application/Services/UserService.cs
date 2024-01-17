using System.Security.Cryptography;
using System.Text.RegularExpressions;
using System.Web;
using Application.Exceptions;
using Application.IServices;
using Application.Models.User;
using Core.Domain.Entity;
using Core.Domain.Enums;
using Infrastructure.Ports;
using OneOf;

namespace Application.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IJwtService _jwtService;
    private readonly IEmailService _emailService;


    public UserService(IUserRepository userRepository, IJwtService jwtService,IEmailService emailService)
    {
        _userRepository = userRepository;
        _jwtService = jwtService;
        _emailService = emailService;
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
    public async Task<bool> ResetPasswordAsync(string email, string token, string newPassword)
    {
        var user = await _userRepository.FindByEmailUser(email);
        if (user == null || !await _userRepository.ValidatePasswordResetTokenAsync(user, token))
        {
            return false;
        }

        var condition = @"^(?=.*[A-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[#?!@$%^&*-])\S{7,15}$";
        Regex reg = new Regex(condition);
        if (!reg.IsMatch(newPassword))
        {
            throw new Exception("Le nouveau mot de passe ne répond pas aux exigences de sécurité.");
        }

        user.Password = BCrypt.Net.BCrypt.HashPassword(newPassword);
        user.ResetPassWordToken = null;
        user.ResetPasswordTokenExpiration = null;
        await _userRepository.UpdateAsync(user);

        return true;
    }

    public async Task<List<User>> GetAllUserAsync()
    {
        return await _userRepository.GetAllUserAsync();
    }

    public async Task UpdateUserEmailAsync(Guid userId, string newEmail)
    {
        var user = await _userRepository.FindUserById(userId);
        var conditionEmail = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
        Regex regEmail = new Regex(conditionEmail);
        if (!regEmail.IsMatch(newEmail))
        {
            throw new BadRequestException("Cette adresse email est invalide !");
        }
        
        if (user == null)
        {
            throw new InvalidOperationException("User not Found");
        }

        user.Email = newEmail;
        await _userRepository.UpdateAsync(user);
    }

    public async Task UpdateUserNameAsync(Guid userId, UserModifyNameModel model)
    {
        var user = await _userRepository.FindUserById(userId);
        if (user == null)
        {
            throw new InvalidOperationException("User not Found");
        }

        user.Lastname = model.LastName;
        user.Firstname = model.FirstName;
        await _userRepository.UpdateAsync(user);
    }

    public async Task ChangeUserPasswordAsync(Guid userId, string currentPassword, string newPassword)
    {
        var user = await _userRepository.FindUserById(userId);
        if (user == null)
        {
            throw new InvalidOperationException("User not found.");
        }

        if (!VerifyPassword(currentPassword, user.Password))
        {
            throw new InvalidOperationException("Current password is incorrect.");
        }
        var condition = @"^(?=.*[A-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[#?!@$%^&*-])\S{7,15}$";
        Regex reg = new Regex(condition);
        if (!reg.IsMatch(newPassword))
        {
            throw new Exception("Le nouveau mot de passe ne répond pas aux exigences de sécurité.");
        }

        user.Password = HashPassword(newPassword);
        await _userRepository.UpdateAsync(user);
    }

    public async Task<User> GetUserById(Guid userId)
    {
        var user = await _userRepository.FindUserById(userId);
        return user;
    }

    private bool VerifyPassword(string inputPassword, string storedHash)
    {
        return BCrypt.Net.BCrypt.Verify(inputPassword, storedHash);
    }

    private string HashPassword(string password)
    {
        return BCrypt.Net.BCrypt.HashPassword(password);
    }

    

    public async Task RequestPasswordResetAsync(string email)
    {
        var user = await _userRepository.FindByEmailUser(email);
        if (user != null)
        {
            var token = GenerateToken();
            await _userRepository.SavePasswordResetTokenAsync(user, token, DateTime.UtcNow.AddDays(1));
            var callbackUrl = $"https://votresite.com/reset-password?email={HttpUtility.UrlEncode(email)}&token={HttpUtility.UrlEncode(token)}";
            var message = $"Veuillez réinitialiser votre mot de passe en cliquant sur ce lien: <a href='{callbackUrl}'>Réinitialiser le mot de passe</a>" + token;
            await _emailService.SendEmailAsync(user.Email, "Mail de réinitialisation de mot de passe RailRideBMX" ,message);
        }
    }

    public string GenerateToken()
    {
        using (var rngCryptoServiceProvider = new RNGCryptoServiceProvider())
        {
            var randomBytes = new byte[32];
            rngCryptoServiceProvider.GetBytes(randomBytes);
            return Convert.ToBase64String(randomBytes);
        }
    }



}   