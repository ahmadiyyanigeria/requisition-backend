using Domain.Entities.Aggregates.RequisitionAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.EntityTypeConfigurations
{
    public class ApprovalFlowEntityTypeConfiguration : IEntityTypeConfiguration<ApprovalFlow>
    {
        public void Configure(EntityTypeBuilder<ApprovalFlow> builder)
        {
            builder.ToTable("approval_flows");

            builder.HasKey(e => e.ApprovalFlowId);

            builder.Property(e => e.ApprovalFlowId)
                .HasColumnName("approval_flow_id")
                .ValueGeneratedNever();

            builder.Property(e => e.RequisitionId)
                .IsRequired()
                .HasColumnName("requisition_id");

            builder.Property(e => e.CurrentStep)
                .IsRequired()
                .HasColumnName("current_step");

            builder.HasMany(e => e.Approvers)
                .WithOne()
                .HasForeignKey("ApprovalFlowId")
                .OnDelete(DeleteBehavior.Cascade);

            builder.OwnsMany(e => e.Approvers, stepBuilder =>
            {
                stepBuilder.ToTable("approval_steps");
                stepBuilder.HasKey(s => s.ApprovalStepId);
                stepBuilder.Property(s => s.ApprovalStepId).HasColumnName("approval_step_id");
                stepBuilder.Property(s => s.ApproverId).HasColumnName("approver_id");
                stepBuilder.Property(s => s.Status).HasColumnName("status");
                stepBuilder.Property(s => s.ApprovalDate).HasColumnName("approval_date");
                stepBuilder.Property(s => s.Notes).HasColumnName("notes").HasColumnType("text");
            });
        }
    }
}
