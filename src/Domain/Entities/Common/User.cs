namespace Domain.Entities.Common
{
    public class User
    {
        public Guid UserId { get; set; }
        public string Role { get; set; }
        public string Department { get; set; }
    }
}
