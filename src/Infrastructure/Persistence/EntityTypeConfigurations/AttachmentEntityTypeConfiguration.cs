using Domain.Entities.Aggregates.RequisitionAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.EntityTypeConfigurations
{
    public class AttachmentEntityTypeConfiguration : IEntityTypeConfiguration<Attachment>
    {
        public void Configure(EntityTypeBuilder<Attachment> builder)
        {
            builder.ToTable("attachments");

            builder.HasKey(e => e.AttachmentId);

            builder.Property(e => e.AttachmentId)
                .HasColumnName("attachment_id")
                .ValueGeneratedNever();

            builder.Property(e => e.FileName)
                .IsRequired()
                .HasColumnName("file_name")
                .HasColumnType("varchar(255)"); // Adjust type as per your needs

            builder.Property(e => e.FileType)
                .HasColumnName("file_type")
                .HasColumnType("varchar(50)"); // Adjust type as per your needs

            builder.Property(e => e.FileContent)
                .HasColumnName("file_content")
                .HasColumnType("varchar(255)"); 
        }
    }
}
