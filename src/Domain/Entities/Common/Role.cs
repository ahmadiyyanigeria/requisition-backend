namespace Domain.Entities.Common
{
    public class Role(string name, string description)
    {
        public Guid RoleId { get; private set; } = Guid.NewGuid();
        public string Name { get; private set; } = name;
        public string Description { get; private set; } = description;
    }
}
