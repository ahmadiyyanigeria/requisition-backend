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
                .ValueGeneratedNever(); 

            builder.Property(e => e.CashAdvanceId)
                .IsRequired()
                .HasColumnName("cash_advance_id");

            builder.Property(e => e.Description)
                .IsRequired()
                .HasColumnName("description");

            builder.Property(e => e.Amount)
                .HasColumnType("decimal(18,2)")
                .IsRequired()
                .HasColumnName("amount");

            builder.Property(e => e.Date)
                .IsRequired()
                .HasColumnName("date");

            builder.Property(e => e.AttachmentId)
                .IsRequired()
                .HasColumnName("attachment_id");

            builder.OwnsOne(e => e.Receipt, attachment =>
            {
                attachment.Property<Guid>("AttachmentId").HasColumnName("attachment_id");
                attachment.Property(att => att.FileName).HasColumnName("file_name").HasColumnType("varchar(255)");
                attachment.Property(att => att.FileType).HasColumnName("file_type").HasColumnType("varchar(50)");
                attachment.Property(att => att.FileContent).HasColumnName("file_content").HasColumnType("text");
            });
        }
    }
}
