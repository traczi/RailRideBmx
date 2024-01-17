using System.Security.Claims;
using Application;
using Application.IServices;
using Application.Models.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace RailRideBMXHexagonale.Controllers;

public class UserController : ApiController
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> GetAllUser()
    {
        var users = await _userService.GetAllUserAsync();
        return Ok(users);
    }
    
    [HttpPut("ModifyEmail")]
    public async Task<IActionResult> UpdateUserEmail([FromBody] string newEmail)
    {
        
        var userIdString = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (!Guid.TryParse(userIdString, out Guid userId))
        {
            return BadRequest("Invalid User ID");
        }

        await _userService.UpdateUserEmailAsync(userId, newEmail);
        return Ok();
    }
    
    [HttpPut("ModifyName")]
    public async Task<IActionResult> UpdateUserName(UserModifyNameModel model)
    {
        
        var userIdString = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (!Guid.TryParse(userIdString, out Guid userId))
        {
            return BadRequest("Invalid User ID");
        }

        await _userService.UpdateUserNameAsync(userId,model);
        return Ok();
    }
    
    [HttpPut("ChangePassword")]
    public async Task<IActionResult> ChangePassword( [FromBody] ChangePasswordModel model)
    {
        var userIdString = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (!Guid.TryParse(userIdString, out Guid userId))
        {
            return BadRequest("Invalid User ID");
        }

        await _userService.ChangeUserPasswordAsync(userId, model.CurrentPassword, model.NewPassword);
        return Ok();
    }
    
    [HttpGet]
    [Route("GetUserById")]
    public async Task<IActionResult> GetUserById()
    {
        var userIdString = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (!Guid.TryParse(userIdString, out Guid userId))
        {
            return BadRequest("Invalid User ID");
        }

        var user = await _userService.GetUserById(userId);
        return Ok(user);
    }

}