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
    public class RetirementEntryEntityTypeConfiguration : IEntityTypeConfiguration<RetirementEntry>
    {
        public void Configure(EntityTypeBuilder<RetirementEntry> builder)
        {
            builder.ToTable("retirement_entries");

            builder.HasKey(e => e.RetirementEntryId);

            builder.Property(e => e.RetirementEntryId)
                .HasColumnName("retirement_entry_id")
                .ValueGeneratedNever();

            builder.Property(e => e.CashAdvanceId)
                .IsRequired()
                .HasColumnName("cash_advance_id");

            builder.Property(e => e.Description)
                .IsRequired()
                .HasColumnName("description")
                .HasMaxLength(500);

            builder.Property(e => e.Amount)
                .IsRequired()
                .HasColumnName("amount")
                .HasColumnType("decimal(18,2)");

            builder.Property(e => e.Date)
                .IsRequired()
                .HasColumnName("date");

            builder.HasOne(e => e.Receipt)
                .WithMany()
                .HasForeignKey("receipt_id")
                .OnDelete(DeleteBehavior.Restrict);

            
        }
    }
}
