﻿using Domain.Constants;
using Domain.Entities.Common;

namespace Application.Services
{
    public class UserService : IUserService
    {
        // Assume we have a way to get users and their roles
        private readonly List<User> _users;

        public UserService()
        {
            // Populate with sample users
            _users = new List<User>
            {
                new User { UserId = "1", Role = Roles.HOD, Department = "Welfare" },
                new User { UserId = "2", Role = Roles.Accountant, Department = "Account" },
                new User { UserId = "3", Role = Roles.CEO, Department = "Tabligh" },
            };
        }

        public User GetUserByRole(string role)
        {
            return _users.FirstOrDefault(u => u.Role == role);
        }

        public string GetUserRole(string userId)
        {
            var user = _users.FirstOrDefault(u => u.UserId == userId);
            if (user == null)
            {
                throw new InvalidOperationException("User not found");
            }
            return user.Role;
        }
    }
}
