using Domain.Entities.Aggregates.CashAdvanceAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.EntityTypeConfigurations
{
    public class ReimbursementEntryEntityTypeConfiguration : IEntityTypeConfiguration<ReimbursementEntry>
    {
        public void Configure(EntityTypeBuilder<ReimbursementEntry> builder)
        {
            builder.ToTable("reimbursement_entries");

            builder.HasKey(e => e.ReimbursementEntryId);

            builder.Property(e => e.ReimbursementEntryId)
                .HasColumnName("reimbursement_entry_id")
                .ValueGeneratedNever()
                .HasColumnType("uuid");

            builder.Property(e => e.CashAdvanceId)
                .IsRequired()
                .HasColumnName("cash_advance_id")
                .HasColumnType("uuid");

            builder.Property(e => e.Description)
                .IsRequired()
                .HasColumnName("description")
                .HasColumnType("varchar(200)");

            builder.Property(e => e.Amount)
                .HasColumnType("decimal(18,2)")
                .IsRequired()
                .HasColumnName("amount");

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
