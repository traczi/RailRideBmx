﻿using Application;
using Application.Models.User;
using Microsoft.AspNetCore.Mvc;
using RailRideBMXHexagonale.Helpers;

namespace RailRideBMXHexagonale.Controllers;

public class AuthController : ApiController
{
    private readonly IUserService _userService;

    public AuthController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpPost]
    [Route("Register")]
    public async Task<IActionResult> CreateUserAsync(UserResponseModel userResponseModel)
    {
        var createUser = await _userService.CreateUserAsync(userResponseModel);
        return SuccessResponseHelper.CreatedResponse("Utilisateur créé",createUser);
    }

    [HttpPost]
    [Route("Login")]
    public async Task<IActionResult> LoginUserAsync(UserLoginRequestModel userLoginRequestModel)
    {
        var loginUser = await _userService.LoginUserAsync(userLoginRequestModel);

        return SuccessResponseHelper.SuccessResponse("Vous êtes bien logger", new
        {
            token = loginUser.AsT0
        });
    }
}