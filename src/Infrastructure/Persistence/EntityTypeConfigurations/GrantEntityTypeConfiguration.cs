using Domain.Entities.Aggregates.GrantAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.EntityTypeConfigurations
{
    public class GrantEntityTypeConfiguration : IEntityTypeConfiguration<Grant>
    {
        public void Configure(EntityTypeBuilder<Grant> builder)
        {
            builder.ToTable("grant");

            builder.HasKey(e => e.GrantId);

            builder.Property(e => e.GrantId)
                .IsRequired()
                .HasColumnName("grant_id")
                .HasColumnType("uuid");

            builder.Property(e => e.RequisitionId)
                .IsRequired()
                .HasColumnName("requisition_id")
                .HasColumnType("uuid");

            builder.Property(e => e.ProcessorId)
                .IsRequired()
                .HasColumnName("processor_id")
                .HasColumnType("uuid");

            builder.Property(e => e.Notes)
             .IsRequired()
             .HasColumnName("notes")
             .HasColumnType("text");

            builder.Property(e => e.GrantAmount)
                .IsRequired()
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("grant_amount");

            builder.Property(e => e.RequestedDate)
               .IsRequired()
               .HasColumnName("requested_date")
               .HasColumnType("timestamp with time zone");

            builder.Property(e => e.DisbursedDate)
                .HasColumnName("disbursed_date")
                .HasColumnType("timestamp with time zone");

            builder.Property(e => e.Status)
                .IsRequired()
                .HasMaxLength(20)
                .HasColumnType("varchar(20)")
                .HasColumnName("status");

            // Configure BankAccount as a value object
            builder.OwnsOne(e => e.BankAccount, ba =>
            {
                ba.Property(b => b.AccountNumber)
                    .HasColumnName("bank_account_number")
                    .IsRequired()
                    .HasColumnType("varchar(50)");

                ba.Property(b => b.BankName)
                    .HasColumnName("bank_name")
                    .IsRequired()
                    .HasColumnType("varchar(100)");

                ba.Property(b => b.AccountName)
                    .HasColumnName("account_name")
                    .IsRequired()
                    .HasColumnType("varchar(100)");

                ba.Property(b => b.IBAN)
                    .HasColumnName("iban")
                    .HasColumnType("varchar(34)");

                ba.Property(b => b.SWIFT)
                    .HasColumnName("swift")
                    .HasColumnType("varchar(11)");
            });
        }
    }
}
