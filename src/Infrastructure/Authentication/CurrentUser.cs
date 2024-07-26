using Application.Common.Interfaces;
using Application.DTOs;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Infrastructure.Authentication;

public class CurrentUser : ICurrentUser
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CurrentUser(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    private ClaimsPrincipal? User => _httpContextAccessor.HttpContext?.User;

    public UserDetails GetUserDetails()
    {
        return new UserDetails
        {
            UserId = GetClaimValue(ClaimTypes.NameIdentifier),
            Name = GetClaimValue(ClaimTypes.Name),
            Email = GetClaimValue(ClaimTypes.Email),
            //Department = GetClaimValue("department"),
            Role = GetClaimValue(ClaimTypes.Role)
        };
    }

    private string GetClaimValue(string claimType)
    {
        return User?.FindFirst(claimType)?.Value ?? string.Empty;
    }
}
