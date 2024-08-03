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
                .ValueGeneratedNever()
                .HasColumnType("uuid");

            builder.Property(e => e.ApprovalFlowId)
                .IsRequired()
                .HasColumnName("approval_flow_id")
                .HasColumnType("uuid");

            builder.Property(e => e.ApproverId)
                .IsRequired()
                .HasColumnName("approver_id")
                .HasColumnType("varchar(50)");

            builder.Property(e => e.Status)
                .IsRequired()
                .HasConversion<string>()
                .HasColumnName("status")
                .HasColumnType("varchar(20)");

            builder.Property(e => e.ApprovalDate)
                .HasColumnName("approval_date")
                .HasColumnType("timestamp with time zone");

            builder.Property(e => e.Notes)
                .HasColumnName("notes")
                .HasColumnType("text");

            builder.Property(e => e.Order)
               .IsRequired()
               .HasColumnName("order")
               .HasColumnType("varchar(50)");
        }
    }
}