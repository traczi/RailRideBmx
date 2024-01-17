using System.Security.Claims;
using Application;
using Application.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace RailRideBMXHexagonale.Controllers;

public class LikeController : ApiController
{
    private readonly ILikeService _likeService;

    public LikeController(ILikeService likeService)
    {
        _likeService = likeService;
    }
    
    [HttpPost]
    [Route("Like")]
    [Authorize]
    public async Task<IActionResult> LikeProduct(Guid productId)
    {
        var userIdString = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (!Guid.TryParse(userIdString, out Guid userId))
        {
            return BadRequest("Invalid User ID");
        }
        await _likeService.LikeProductAsync(userId, productId);
        return Ok();
    }
    
    [HttpPost]
    [Route("UnLike")]
    [Authorize]
    public async Task<IActionResult> UnLikeProduct(Guid productId)
    {
        var userIdString = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (!Guid.TryParse(userIdString, out Guid userId))
        {
            return BadRequest("Invalid User ID");
        }
        await _likeService.UnLikeProductAsync(userId, productId);
        return Ok();
    }
    
    [HttpGet]
    [Route("GetLike")]
    [Authorize]
    public async Task<IActionResult> GetLikeProduct()
    {
        var userIdString = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (!Guid.TryParse(userIdString, out Guid userId))
        {
            return BadRequest("Invalid User ID");
        }
        var product = await _likeService.GetLikeProductAsync(userId);
        return Ok(product);
    }
    [HttpGet]
    [Route("IsLiked")]
    [Authorize]
    public async Task<IActionResult> IsProductLikedByUser(Guid productId)
    {
        var userIdString = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (!Guid.TryParse(userIdString, out Guid userId))
        {
            return BadRequest("Invalid User ID");
        }
        try
        {
            bool isLiked = await _likeService.IsProductLikedByUserAsync(userId, productId);
            return Ok(isLiked);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}