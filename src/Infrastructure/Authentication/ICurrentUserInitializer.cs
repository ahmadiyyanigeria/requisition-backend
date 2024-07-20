using System.Security.Claims;

namespace Infrastructure.Authentication;
public interface ICurrentUserInitializer
{
    void SetCurrentUser(ClaimsPrincipal user);

    void SetCurrentUserId(string userId);
}