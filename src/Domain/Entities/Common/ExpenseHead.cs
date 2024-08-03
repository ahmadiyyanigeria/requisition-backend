namespace Domain.Entities.Common
{
    public class ExpenseHead
    {
        public string Name { get; private set; } = default!;
        public string? Description { get; private set; }


        private ExpenseHead() { }   
        public ExpenseHead(string name, string? description)
        {
            Name = name;
            Description = description;
        }
    }
}
