using Domain.Entities;
using System.Security.Claims;

namespace Application.Common.Interfaces;

public interface ICurrentUser
{
    //Task<User?> GetUserAsync();
    string? Name { get; }

    Guid GetUserId();

    string? GetUserEmail();

    string? GetUserRole();

    bool IsAuthenticated();

    bool IsInRole(string role);

    IEnumerable<Claim>? GetUserClaims();
}