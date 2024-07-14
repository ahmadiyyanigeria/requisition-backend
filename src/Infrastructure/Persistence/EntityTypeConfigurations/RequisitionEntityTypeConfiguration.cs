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

            // Configure ApprovalFlow as a separate entity
            builder.HasOne(e => e.ApprovalFlow)
                .WithOne()
                .HasForeignKey<ApprovalFlow>(e => e.RequisitionId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict); 

            // Configure RequisitionItems as a collection
            builder.HasMany(e => e.Items)
                .WithOne()
                .HasForeignKey(item => item.RequisitionId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            // Configure BankAccount as a separate entity
            builder.HasOne(e => e.BankAccount)
                .WithMany()
                .HasForeignKey(e => e.AccountNumber)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict); 
        }
    }
}