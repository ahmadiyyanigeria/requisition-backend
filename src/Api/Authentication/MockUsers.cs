using Domain.Entities.Common;

namespace Api.Authentication
{
    public static class MockUsers
    {
        public static List<User> Users = new()
        {
             new User
            {
                UserId = "4",
                Name = "Cynthia Paul",
                Email = "employee@example.com",
                Role = "Employee"
            },
            new User
            {
                UserId = "1",
                Name = "Kenny Maax",
                Email = "hod1@example.com",
                Role = "HOD"
            },
            new User
            {
                UserId = "2",
                Name = "John Doe",
                Email = "accountant@example.com",
                Role = "Accountant"
            },
            new User
            {
                UserId = "3",
                Name = "Jane Smith",
                Email = "ceo@example.com",
                Role = "CEO"
            }
        };

        public static User GetUserByEmail(string email)
        {
            return Users.FirstOrDefault(user => user.Email.Equals(email, StringComparison.OrdinalIgnoreCase));
        }
    }

    public class User
    {
        public string UserId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
    }
}
