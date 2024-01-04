namespace RailRideBMXHexagonale.Middleware;

public class SessionCookieMiddleware
{
    private readonly RequestDelegate _next;
    private const string SessionCookieName = "UserSessionId";

    public SessionCookieMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        if (!context.Request.Cookies.TryGetValue(SessionCookieName, out var sessionId))
        {
            sessionId = Guid.NewGuid().ToString();
            var cookieOptions = new CookieOptions
            {
                HttpOnly = false,
                Secure = true,
                SameSite = SameSiteMode.Lax,
                Expires = DateTimeOffset.UtcNow.AddDays(7)
            };
            context.Response.Cookies.Append(SessionCookieName, sessionId, cookieOptions);
        }

        await _next(context);
    }
}
