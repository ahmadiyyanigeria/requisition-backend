using Domain.Entities.Aggregates.CashAdvanceAggregate;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.EntityTypeConfigurations
{
    public class RefundEntryEntityTypeConfiguration : IEntityTypeConfiguration<RefundEntry>
    {
        public void Configure(EntityTypeBuilder<RefundEntry> builder)
        {
            builder.ToTable("refund_entries");

            builder.HasKey(e => e.RefundEntryId);

            builder.Property(e => e.RefundEntryId)
                .HasColumnName("refund_entry_id")
                .ValueGeneratedNever()
                .HasColumnType("uuid");

            builder.Property(e => e.CashAdvanceId)
                .IsRequired() 
                .HasColumnName("cash_advance_id")
                .HasColumnType("uuid");

            builder.Property(e => e.Amount)
                .IsRequired() 
                .HasColumnName("amount") 
                .HasColumnType("decimal(18,2)");

            builder.Property(e => e.Date)
                .IsRequired() 
                .HasColumnName("date")
                .HasColumnType("timestamp with time zone");

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
