using System.Security.Claims;
using Application;
using Application.IServices;
using Application.Models;
using Application.Models.Comment;
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
    [Route("DeleteComment")]
    [Authorize]
    public async Task<IActionResult> DeleteComment(Guid commentId)
    {
        var userIdString = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (!Guid.TryParse(userIdString, out Guid userId))
        {
            return BadRequest("Invalid User ID");
        }

        var userRole = User.FindFirst(ClaimTypes.Role)?.Value;
        await _commentService.DeleteUserComment(commentId, userId, userRole);
        return Ok();

    }

    [HttpGet]
    [Route("AverageRating")]
    public async Task<IActionResult> GetAverageRating(Guid productId)
    {
        var averageRating = await _commentService.CalculateAverageRatingAsync(productId);
        return Ok(averageRating);
    }

    [HttpPost]
    [Route("ReportComment")]
    public async Task<IActionResult> ReportComment(Guid commentId)
    {
        await _commentService.ReportComment(commentId);
        return Ok("le commentaire a bien été signaler");
    }

    [HttpGet]
    [Route("GetReportedComment")]
    public async Task<IActionResult> GetRepportedComment()
    {
        var reportedComment = await _commentService.GetReportedCommentAsync();
        return Ok(reportedComment);
    }

    [HttpPut]
    [Authorize]
    [Route("UpdateComment")]
    public async Task<IActionResult> UpdateComment(Guid commentId,[FromBody] string newCommentText)
    {
        var userIdString = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (!Guid.TryParse(userIdString, out Guid userId))
        {
            return BadRequest("Invalid User ID");
        }

        await _commentService.UpdateCommentAsync(commentId, userId, newCommentText);
        return Ok("Le commentaire a bien été modifier" + newCommentText);
    }
}