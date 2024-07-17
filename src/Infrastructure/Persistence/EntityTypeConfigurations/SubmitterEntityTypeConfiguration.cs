using Domain.Entities.Aggregates.SubmitterAggregate;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.EntityTypeConfigurations
{
    public class SubmitterEntityTypeConfiguration : IEntityTypeConfiguration<Submitter>
    {
        public void Configure(EntityTypeBuilder<Submitter> builder)
        {
            builder.HasKey(s => s.SubmitterId);

            builder.Property(s => s.SubmitterId)
                .IsRequired();

            builder.Property(s => s.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(s => s.Email)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(s => s.Position)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(s => s.Department)
                .IsRequired()
                .HasMaxLength(100);

            builder.OwnsOne(s => s.BankAccount, bankAccount =>
            {
                bankAccount.Property(b => b.AccountNumber).HasMaxLength(20).IsRequired();
                bankAccount.Property(b => b.BankName).HasMaxLength(100).IsRequired();
                bankAccount.Property(b => b.AccountName).HasMaxLength(100).IsRequired();
            });

            builder.HasMany(s => s.Requisitions)
                .WithOne()
                .HasForeignKey(r => r.SubmitterId);
        }
    }
}
