using Domain.Entities.Aggregates.RequisitionAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System.Text.Json;

namespace Infrastructure.Persistence.EntityTypeConfigurations
{
    public class ApprovalStepEntityTypeConfiguration : IEntityTypeConfiguration<ApprovalStep>
    {
        public void Configure(EntityTypeBuilder<ApprovalStep> builder)
        {
            builder.ToTable("approval_steps"); // Specify the table name

            builder.HasKey(e => e.ApprovalStepId);

            builder.Property(e => e.ApprovalStepId)
                .HasColumnName("approval_step_id")
                .ValueGeneratedNever() // Assuming the value is generated outside of EF Core
                .HasColumnType("uuid"); // PostgreSQL type for GUID

            builder.Property(e => e.ApprovalFlowId)
                .IsRequired()
                .HasColumnName("approval_flow_id")
                .HasColumnType("uuid"); // PostgreSQL type for GUID

            builder.Property(e => e.ApproverId)
                .IsRequired()
                .HasColumnName("approver_id")
                .HasColumnType("varchar(50)"); // Adjust size as needed

            builder.Property(e => e.Status)
                .IsRequired()
                .HasConversion<string>() // Convert enum to string for storage
                .HasColumnName("status")
                .HasColumnType("varchar(20)"); // Adjust size as needed

            builder.Property(e => e.ApprovalDate)
                .HasColumnName("approval_date")
                .HasColumnType("timestamp with time zone"); // PostgreSQL type for DateTime with timezone

            builder.Property(e => e.Notes)
                .HasColumnName("notes")
                .HasColumnType("text"); // PostgreSQL type for text

            // Configure the ApprovalRoles property to be stored as a JSONB column
            builder.Property(e => e.ApprovalRoles)
                .HasColumnName("approval_roles")
                .HasConversion(
                    new ValueConverter<IReadOnlyList<string>, string>(
                        v => JsonSerializer.Serialize(v, (JsonSerializerOptions)null),
                        v => JsonSerializer.Deserialize<IReadOnlyList<string>>(v, (JsonSerializerOptions)null)))
                .HasColumnType("jsonb");
        }
    }
}