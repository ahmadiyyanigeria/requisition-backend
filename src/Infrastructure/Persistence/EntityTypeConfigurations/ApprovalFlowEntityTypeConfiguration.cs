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
                .ValueGeneratedNever()
                .HasColumnType("uuid");

            builder.Property(e => e.RequisitionId)
                .IsRequired()
                .HasColumnName("requisition_id")
                .HasColumnType("uuid");

            builder.Property(e => e.CurrentStep)
                .IsRequired()
                .HasColumnName("current_step")
                .HasColumnType("integer");

            // Configure the relationship between ApprovalFlow and ApprovalStep
            builder.HasMany(e => e.ApproverSteps)
                .WithOne()
                .HasForeignKey(step => step.ApprovalFlowId) // Correct foreign key
                .OnDelete(DeleteBehavior.Restrict); // Adjust as necessary
        }
    }
}