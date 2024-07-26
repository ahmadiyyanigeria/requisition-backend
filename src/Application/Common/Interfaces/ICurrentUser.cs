using Application.DTOs;
using Domain.Entities;
using System.Security.Claims;

namespace Application.Common.Interfaces;

public interface ICurrentUser
{
    UserDetails GetUserDetails();
}