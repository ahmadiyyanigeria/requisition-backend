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
                .ValueGeneratedNever();

            builder.Property(e => e.CashAdvanceId)
                .IsRequired() 
                .HasColumnName("cash_advance_id");

            builder.Property(e => e.Amount)
                .IsRequired() 
                .HasColumnName("amount") 
                .HasColumnType("decimal(18,2)");

            builder.Property(e => e.Date)
                .IsRequired() 
                .HasColumnName("date"); 

            builder.Property(e => e.AccountNumber)
                .IsRequired() 
                .HasColumnName("account_number") 
                .HasMaxLength(20);

        }
    }
}
