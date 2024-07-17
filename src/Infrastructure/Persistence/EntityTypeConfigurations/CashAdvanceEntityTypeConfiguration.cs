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
                .ValueGeneratedNever();

            builder.Property(e => e.RequisitionId)
                .IsRequired()
                .HasColumnName("requisition_id");

            builder.Property(e => e.SubmitterId)
                .IsRequired()
                .HasColumnName("submitter_id");

            builder.Property(e => e.AdvanceAmount)
                .IsRequired()
                .HasColumnName("advance_amount")
                .HasColumnType("decimal(18,2)");

            builder.Property(e => e.AccountNumber)
                .IsRequired()
                .HasColumnName("account_number")
                .HasMaxLength(20);

            builder.Property(e => e.Status)
                .IsRequired()
                .HasColumnName("status")
                .HasConversion<int>();

            builder.HasOne(e => e.BankAccount)
                .WithMany()
                .HasForeignKey("bank_account_id")
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(e => e.RetirementEntry)
                .WithMany()
                .HasForeignKey("retirement_entry_id")
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(e => e.RefundEntry)
                .WithMany()
                .HasForeignKey("refund_entry_id")
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(e => e.ReimbursementEntry)
                .WithMany()
                .HasForeignKey("reimbursement_entry_id")
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
