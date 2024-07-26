using Application.DTOs;

namespace Application.Common.Interfaces;

public interface ICurrentUser
{
    UserDetails GetUserDetails();
}