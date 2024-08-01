using Domain.Entities.Aggregates.CashAdvanceAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

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
                .ValueGeneratedNever()
                .HasColumnType("uuid");

            builder.Property(e => e.CashAdvanceId)
                .IsRequired()
                .HasColumnName("cash_advance_id")
                .HasColumnType("uuid");

            builder.Property(e => e.Description)
                .IsRequired()
                .HasColumnName("description")
                .HasMaxLength(500)
                .HasColumnType("text");

            builder.Property(e => e.Amount)
                .IsRequired()
                .HasColumnName("amount")
                .HasColumnType("decimal(18,2)");

            builder.Property(e => e.Date)
                .IsRequired()
                .HasColumnName("date")
                .HasColumnType("timestamp with time zone");

            builder.Property(e => e.AttachmentId)
             .IsRequired()
             .HasColumnName("attachment_id")
             .HasColumnType("uuid");
        }
    }
}
