using Domain.Entities.Aggregates.SubmitterAggregate;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.EntityTypeConfigurations
{
    public class SubmitterEntityTypeConfiguration : IEntityTypeConfiguration<Submitter>
    {
        public void Configure(EntityTypeBuilder<Submitter> builder)
        {
            builder.HasKey(s => s.SubmitterId);

            builder.Property(s => s.SubmitterId)
                .IsRequired();

            builder.Property(s => s.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(s => s.Email)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(s => s.Position)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(s => s.Department)
                .IsRequired()
                .HasMaxLength(100);
        }
    }
}
