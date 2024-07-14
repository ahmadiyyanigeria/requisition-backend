namespace Domain.Entities.Common
{
    public class Role
    {
        public Guid RoleId { get; private set; } = Guid.NewGuid();
        public string Name { get; private set; }
        public string Description { get; private set; }

        private Role() { }
        public Role(string name, string description)
        {
            Name = name;
            Description = description;
        }
    }
}
