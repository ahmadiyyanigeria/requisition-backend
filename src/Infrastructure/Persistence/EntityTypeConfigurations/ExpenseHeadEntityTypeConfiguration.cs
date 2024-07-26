using Domain.Entities.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.EntityTypeConfigurations
{
    public class ExpenseHeadEntityTypeConfiguration : IEntityTypeConfiguration<ExpenseHead>
    {
        public void Configure(EntityTypeBuilder<ExpenseHead> builder)
        {
            builder.ToTable("expense_head");

            builder.HasKey(e => e.Name);

            builder.Property(e => e.Name)
                .IsRequired()
                .HasColumnName("name")
                .HasColumnType("varchar(50)");

            builder.Property(e => e.Description)
                .HasColumnName("description")
                .HasColumnType("varchar(100)");
        }
    }
}
