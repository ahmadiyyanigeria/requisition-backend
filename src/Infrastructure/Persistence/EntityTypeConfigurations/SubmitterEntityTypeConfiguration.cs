using Domain.Entities.Aggregates.SubmitterAggregate;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.EntityTypeConfigurations
{
    public class SubmitterEntityTypeConfiguration : IEntityTypeConfiguration<Submitter>
    {
        public void Configure(EntityTypeBuilder<Submitter> builder)
        {
            builder.ToTable("submitter");

            builder.HasKey(s => s.SubmitterId);

            builder.Property(e => e.SubmitterId)
                .IsRequired()
                .HasColumnName("submitter_id")
                .HasColumnType("uuid");

            builder.Property(s => s.UserId)
               .IsRequired()
               .HasColumnName("user_id")
               .HasColumnType("varchar(100)");

            builder.Property(s => s.Name)
                .IsRequired()
                .HasColumnName("name")
                .HasColumnType("varchar(100)");

            builder.Property(s => s.Email)
                .IsRequired()
                .HasColumnName("email")
                .HasColumnType("varchar(100)");

            builder.Property(s => s.Position)
                .IsRequired()
                .HasColumnName("position")
                .HasColumnType("varchar(100)");

            builder.Property(s => s.Department)
                .IsRequired()
                .HasColumnName("department")
                .HasColumnType("varchar(100)");
        }
    }
}
