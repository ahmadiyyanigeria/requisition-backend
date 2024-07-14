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

            // Configure one-to-many relationship with ApprovalSteps
            builder.HasMany(flow => flow.Approvers)
                .WithOne(step => step.ApprovalFlow)
                .HasForeignKey(step => step.ApproverId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}