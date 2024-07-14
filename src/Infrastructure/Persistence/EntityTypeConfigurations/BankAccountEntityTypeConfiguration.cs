using Domain.Entities.Aggregates.RequisitionAggregate;
using Domain.Entities.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.EntityTypeConfigurations
{
    public class BankAccountEntityTypeConfiguration : IEntityTypeConfiguration<BankAccount>
    {
        public void Configure(EntityTypeBuilder<BankAccount> builder)
        {
            builder.ToTable("bank_account");

            builder.HasKey(e => e.AccountNumber);

            builder.Property(e => e.AccountNumber)
                .IsRequired()
                .HasColumnName("account_number");

            builder.Property(e => e.BankName)
                .IsRequired()
                .HasColumnName("bank_name");

            builder.Property(e => e.AccountName)
                .IsRequired()
                .HasColumnName("account_name");

            builder.Property(e => e.IBAN)
                .HasColumnName("iban");

            builder.Property(e => e.SWIFT)
                .HasColumnName("swift");
        }
    }
}
