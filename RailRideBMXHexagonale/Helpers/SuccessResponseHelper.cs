using Microsoft.AspNetCore.Mvc;

namespace Application.Helpers;

public static class SuccessResponseHelper
{
    public static IActionResult CreatedResponse(string message, object data = null)
    {
        var response = new
        {
            Message = message,
            Data = data,
        };

        return new OkObjectResult(response)
        {
            StatusCode = 201
        };
    }
    public static IActionResult SuccessResponse(string message, object data = null)
    {
        var response = new
        {
            Message = message,
            Data = data,
        };

        return new OkObjectResult(response)
        {
            StatusCode = 200
        };
    }
    
}