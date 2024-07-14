using Domain.Entities.Aggregates.RequisitionAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.EntityTypeConfigurations
{
    public class RequisitionEntityTypeConfiguration : IEntityTypeConfiguration<Requisition>
    {
        public void Configure(EntityTypeBuilder<Requisition> builder)
        {
            builder.ToTable("requisitions"); // Specify table name if different

            builder.HasKey(e => e.RequisitionId);

            builder.Property(e => e.RequisitionId)
                .HasColumnName("requisition_id")
                .ValueGeneratedNever(); // Assuming you generate the value outside of EF Core

            builder.Property(e => e.SubmitterId)
                .IsRequired()
                .HasColumnName("submitter_id");

            builder.Property(e => e.Description)
                .IsRequired()
                .HasColumnName("description");

            builder.Property(e => e.Status)
                .IsRequired()
                .HasMaxLength(20)
                .HasColumnName("status");

            builder.Property(e => e.RequestedDate)
                .IsRequired()
                .HasColumnName("requested_date");

            builder.Property(e => e.ApprovedDate)
                .HasColumnName("approved_date");

            builder.Property(e => e.RejectedDate)
                .HasColumnName("rejected_date");

            builder.Property(e => e.TotalAmount)
                .HasColumnType("decimal(18,2)")
                .IsRequired()
                .HasColumnName("total_amount");

            // Configure navigation properties and owned entities
            builder.OwnsOne(e => e.ApprovalFlow, approvalFlow =>
            {
                approvalFlow.ToTable("approval_flows"); // Optional: Specify table name if different
                approvalFlow.Property<Guid>("RequisitionId").HasColumnName("requisition_id"); // Shadow property
                approvalFlow.WithOwner().HasForeignKey("RequisitionId");
            });

            builder.OwnsOne(e => e.BankAccount, bankAccount =>
            {
                bankAccount.Property<Guid>("RequisitionId").HasColumnName("requisition_id"); // Shadow property
                bankAccount.WithOwner().HasForeignKey("RequisitionId");
            });

            // Configure Items as owned collection (if using JSONB approach, omit if using separate entity)
            builder.OwnsMany(e => e.Items, items =>
            {
                items.WithOwner().HasForeignKey("RequisitionId");
                items.Property<int>("ItemId").HasColumnName("item_id");
                items.HasKey("RequisitionId", "ItemId");
                items.Property(item => item.Description).HasColumnName("item_description");
                items.Property(item => item.Quantity).HasColumnName("quantity");
                items.Property(item => item.UnitPrice).HasColumnName("unit_price");
            });

            // Configure Attachments as owned entity collection
            builder.OwnsMany(e => e.Attachments, attachments =>
            {
                attachments.WithOwner().HasForeignKey("RequisitionId");
                attachments.Property<Guid>("AttachmentId").HasColumnName("attachment_id");
                attachments.HasKey("RequisitionId", "AttachmentId");
                attachments.Property(att => att.FileName).HasColumnName("file_name").HasColumnType("varchar(255)"); // Example type
                attachments.Property(att => att.FileType).HasColumnName("file_type").HasColumnType("varchar(50)"); // Example type
                attachments.Property(att => att.FileContent).HasColumnName("file_content").HasColumnType("text");
            });

            // Configure other properties like ExpenseAccountId, RequisitionType, Department, etc.
            builder.Property(e => e.ExpenseAccountId)
                .IsRequired()
                .HasColumnName("expense_account_id");

            builder.Property(e => e.RequisitionType)
                .IsRequired()
                .HasMaxLength(20)
                .HasColumnName("requisition_type");

            builder.Property(e => e.Department)
                .HasMaxLength(100)
                .HasColumnName("department");
        }
    }
}
