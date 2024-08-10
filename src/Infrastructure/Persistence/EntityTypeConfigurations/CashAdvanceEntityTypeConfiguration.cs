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
    public class CashAdvanceEntityTypeConfiguration : IEntityTypeConfiguration<CashAdvance>
    {
        public void Configure(EntityTypeBuilder<CashAdvance> builder)
        {
            builder.ToTable("cash_advances");

            builder.HasKey(e => e.CashAdvanceId);

            builder.Property(e => e.CashAdvanceId)
                .HasColumnName("cash_advance_id")
                .ValueGeneratedNever()
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

            builder.Property(e => e.AdvanceAmount)
                .IsRequired()
                .HasColumnName("advance_amount")
                .HasColumnType("decimal(18,2)");

            builder.Property(e => e.Status)
                .IsRequired()
                .HasMaxLength(20)
                .HasColumnName("status")
                .HasColumnType("varchar(20)");

            builder.Property(e => e.RequestedDate)
                .IsRequired()
                .HasColumnName("requested_date")
                .HasColumnType("timestamp with time zone");

            builder.Property(e => e.DisbursedDate)
                .HasColumnName("disbursed_date")
                .HasColumnType("timestamp with time zone");

            builder.Property(e => e.RetiredDate)
                .HasColumnName("retired_date")
                .HasColumnType("timestamp with time zone");

            // Configure relationships
            builder.HasOne(e => e.RetirementEntry)
                .WithOne()
                .HasForeignKey<RetirementEntry>(re => re.CashAdvanceId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(e => e.RefundEntry)
                .WithOne()
                .HasForeignKey<RefundEntry>(re => re.CashAdvanceId) // Adjust as necessary
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(e => e.ReimbursementEntry)
                .WithOne()
                .HasForeignKey<ReimbursementEntry>(re => re.CashAdvanceId) // Adjust as necessary
                .OnDelete(DeleteBehavior.Restrict);

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
