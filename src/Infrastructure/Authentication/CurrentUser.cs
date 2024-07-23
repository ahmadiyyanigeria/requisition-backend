using Application.Common.Interfaces;
using System.Security.Claims;

namespace Infrastructure.Authentication;

public class CurrentUser : ICurrentUser, ICurrentUserInitializer
{
   /* private ClaimsPrincipal? User => _httpContextAccessor.HttpContext?.User;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IUserRepository _userRepository;
    public CurrentUser(IHttpContextAccessor httpContextAccessor, IUserRepository userRepository)
    {
        _httpContextAccessor = httpContextAccessor;
        _userRepository = userRepository;
    }
    public async Task<User?> GetUserAsync()
    {
        var currentUserEmail = GetUserEmail();
        return currentUserEmail is not null ? await _userRepository.FindByEmailAsync(currentUserEmail) : default;
    }

    }
    private string? GetUserEmail()
    {
        return IsAuthenticated()
            ? User!.FindFirst(ClaimTypes.Email).Value
            : string.Empty;
    }*/

    private ClaimsPrincipal? _user;

    public string? Name => _user?.Identity?.Name;

    private Guid _userId = Guid.Empty;

    public Guid GetUserId()
    {
        return IsAuthenticated()
            ? Guid.Parse(_user?.GetUserId() ?? Guid.Empty.ToString())
            : _userId;
    }

    public string? GetUserEmail()
    {
        return IsAuthenticated()
            ? _user!.GetEmail()
            : string.Empty;
    }

    public string? GetUserRole()
    {
        return IsAuthenticated()
            ? _user!.GetUserRole()
            : string.Empty;
    }

    public bool IsAuthenticated() =>
        _user?.Identity?.IsAuthenticated is true;

    public bool IsInRole(string role) =>
        _user?.IsInRole(role) is true;

    public IEnumerable<Claim>? GetUserClaims() =>
        _user?.Claims;

    public void SetCurrentUser(ClaimsPrincipal user)
    {
        if (_user != null)
        {
            throw new Exception("Method reserved for in-scope initialization");
        }

        _user = user;
    }

    public void SetCurrentUserId(string userId)
    {
        if (_userId != Guid.Empty)
        {
            throw new Exception("Method reserved for in-scope initialization");
        }

        if (!string.IsNullOrEmpty(userId))
        {
            _userId = Guid.Parse(userId);
        }
    }
}