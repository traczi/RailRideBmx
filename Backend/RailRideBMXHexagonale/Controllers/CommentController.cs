using System.Security.Claims;
using Application;
using Application.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace RailRideBMXHexagonale.Controllers;

public class CommentController : ApiController
{
    private readonly ICommentService _commentService;

    public CommentController(ICommentService commentService)
    {
        _commentService = commentService;
    }

    [HttpPost]
    [Route("AddComment")]
    [Authorize]
    public async Task<IActionResult> AddComment([FromBody] CommentModel commentModel)
    {
        var userIdString = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (!Guid.TryParse(userIdString, out Guid userId))
        {
            return BadRequest("Invalid User ID");
        }
        await _commentService.AddCommentAsync(commentModel.ProductId, userId,  commentModel.Rating, commentModel.CommentText);
        return Ok();
    }

    [HttpGet]
    [Route("GetComment/{productId}")]
    public async Task<IActionResult> GetComments(Guid productId)
    {
        var comments = await _commentService.GetCommentByProductIdAsync(productId);
        return Ok(comments);
    }
    [HttpDelete]
    [Route("DeleteComment/{commentId}")]
    [Authorize]
    public async Task<IActionResult> DeleteComment(Guid commentId)
    {
        var userIdString = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (!Guid.TryParse(userIdString, out Guid userId))
        {
            return BadRequest("Invalid User ID");
        }

        var comments = await _commentService.DeleteUserComment(commentId, userId);
        if (comments)
        {
            return Ok();
        }
        else
        {
            return Unauthorized("Vous n'êtes pas autorisé à supprimer ce commentaier");
        }
    }
}