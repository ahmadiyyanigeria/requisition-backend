using Domain.Entities.Common;

namespace Application.Services
{
    public interface IUserService
    {
        User GetUserByRole(string role);
        string GetUserRole(Guid userId);
    }
}
