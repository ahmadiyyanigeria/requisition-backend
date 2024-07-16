using Domain.Entities.Aggregates.GrantAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.EntityTypeConfigurations
{
    public class GrantEntityTypeConfiguration : IEntityTypeConfiguration<Grant>
    {
        public void Configure(EntityTypeBuilder<Grant> builder)
        {
            builder.ToTable("grant");

            builder.HasKey(e => e.GrantId);

            builder.Property(e => e.GrantId)
                .IsRequired()
                .HasColumnName("grant_id");

            builder.Property(e => e.RequisitionId)
                .IsRequired()
                .HasColumnName("requisition_id");

            builder.Property(e => e.SubmitterId)
                .IsRequired()
                .HasColumnName("submitter_id");

            builder.Property(e => e.GrantAmount)
                .IsRequired()
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("grant_amount");

            builder.Property(e => e.AccountNumber)
                .IsRequired()
                .HasColumnType("nvarchar(10)")
                .HasColumnName("account_number");

            builder.Property(e => e.Status)
                .IsRequired()
                .HasMaxLength(20)
                .HasColumnName("status");
        }
    }
}
