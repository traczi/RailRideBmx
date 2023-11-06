using System.Net;
using System.Text.Json;
using Microsoft.EntityFrameworkCore;

namespace RailRideBMX.Middleware;

public class ErrorHandlerMiddleware
{
    private readonly RequestDelegate _next;

    public ErrorHandlerMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    private static Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        HttpStatusCode status;
        var stackTrace = string.Empty;
        string message;

        var exceptionType = exception.GetType();
        Console.WriteLine(exceptionType);

        if (exceptionType == typeof(BadRequestException))
        {
            message = exception.Message;
            status = HttpStatusCode.BadRequest;
            stackTrace = exception.StackTrace;
        }
        else if (exceptionType == typeof(NotFoundException))
        {
            message = exception.Message;
            status = HttpStatusCode.NotFound;
            stackTrace = exception.StackTrace;
        }
        else if (exceptionType == typeof(NotImplementedException))
        {
            status = HttpStatusCode.NotImplemented;
            message = exception.Message;
            stackTrace = exception.StackTrace;
        }
        else if (exceptionType == typeof(UnauthorizedAccessException))
        {
            status = HttpStatusCode.Unauthorized;
            message = exception.Message;
            stackTrace = exception.StackTrace;
        }
        else if (exceptionType == typeof(KeyNotFoundException))
        {
            status = HttpStatusCode.Unauthorized;
            message = exception.Message;
            stackTrace = exception.StackTrace;
        }
        else if (exceptionType == typeof(DbUpdateException))
        {
            status = HttpStatusCode.Conflict;
            message = "Vous tentez de créer un compte avec un email déjà existant";
            stackTrace = exception.StackTrace;
        }
        else
        {
            status = HttpStatusCode.InternalServerError;
            message = exception.Message;
            stackTrace = exception.StackTrace;
        }

        var exceptionResult = JsonSerializer.Serialize(new { error = message, stackTrace });
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)status;

        return context.Response.WriteAsync(exceptionResult);
    }
}