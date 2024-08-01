using Domain.Entities.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.EntityTypeConfigurations
{
    public class AttachmentEntityTypeConfiguration : IEntityTypeConfiguration<Attachment>
    {
        public void Configure(EntityTypeBuilder<Attachment> builder)
        {
            builder.ToTable("attachments");

            builder.HasKey(a => a.AttachmentId);

            builder.Property(a => a.AttachmentId)
                .HasColumnName("attachment_id")
                .ValueGeneratedNever()
                .HasColumnType("uuid");


            builder.Property(a => a.FileName)
                .IsRequired()
                .HasColumnName("file_name")
                .HasColumnType("varchar(255)");

            builder.Property(a => a.FileType)
                .IsRequired()
                .HasColumnName("file_type")
                .HasColumnType("varchar(50)");

            builder.Property(a => a.FileContent)
                .IsRequired()
                .HasColumnName("file_content")
                .HasColumnType("text");

            builder.Property(a => a.UploadedDate)
                .IsRequired()
                .HasColumnName("uploaded_date")
                .HasColumnType("timestamp with time zone");
        }
    }
}