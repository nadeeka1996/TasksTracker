using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;

namespace TasksManagement.Application.Common;

internal sealed class CurrentUser(
    IHttpContextAccessor httpContextAccessor
) : ICurrentUser
{
    private readonly HttpContext? _httpContext = httpContextAccessor.HttpContext;

    public Guid Id =>
        _httpContext?.Request.Headers.TryGetValue("x-user-id", out StringValues userId) is true && Guid.TryParse(userId, out Guid parsedUserId)
            ? parsedUserId
            : Guid.Empty;
}