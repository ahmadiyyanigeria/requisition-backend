using Domain.Entities.Aggregates.RequisitionAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.EntityTypeConfigurations
{
    public class ApprovalStepEntityTypeConfiguration : IEntityTypeConfiguration<ApprovalStep>
    {
        public void Configure(EntityTypeBuilder<ApprovalStep> builder)
        {
            builder.ToTable("approval_steps");

            builder.HasKey(e => e.ApprovalStepId);

            builder.Property(e => e.ApprovalStepId)
                .HasColumnName("approval_step_id")
                .ValueGeneratedNever();

            builder.Property(e => e.ApproverId)
                .IsRequired()
                .HasColumnName("approver_id");

            builder.Property(e => e.Status)
                .IsRequired()
                .HasMaxLength(20)
                .HasColumnName("status");

            builder.Property(e => e.ApprovalDate)
                .HasColumnName("approval_date");

            builder.Property(e => e.Notes)
                .HasColumnName("notes")
                .HasColumnType("text");
        }
    }
}