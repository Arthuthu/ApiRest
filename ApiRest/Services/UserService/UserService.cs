namespace ApiRest.Services.UserService;

public class UserService : IUserService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public UserService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public string GetUserName()
    {
        var results = string.Empty;

        if (_httpContextAccessor.HttpContext is not null)
        {
            results = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Name);
        }

        return results;
    }

    public string GetUserRole()
    {
        var results = string.Empty;

        if (_httpContextAccessor.HttpContext is not null)
        {
            results = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Role);
        }

        return results;
    }
}
