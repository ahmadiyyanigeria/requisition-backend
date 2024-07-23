using Domain.Entities.Aggregates.RequisitionAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.EntityTypeConfigurations
{
    public class RequisitionItemEntityTypeConfiguration : IEntityTypeConfiguration<RequisitionItem>
    {
        public void Configure(EntityTypeBuilder<RequisitionItem> builder)
        {
            builder.ToTable("requisition_items");

            builder.HasKey(e => e.RequisitionItemId);

            builder.Property(e => e.RequisitionItemId)
                .HasColumnName("requisition_item_id")
                .ValueGeneratedNever()
                .HasColumnType("uuid"); 

            builder.Property(e => e.RequisitionId)
                .IsRequired()
                .HasColumnName("requisition_id")
                .HasColumnType("uuid"); 

            builder.Property(e => e.Description)
                .IsRequired()
                .HasColumnName("description")
                .HasColumnType("text"); 

            builder.Property(e => e.Quantity)
                .IsRequired()
                .HasColumnName("quantity")
                .HasColumnType("integer");

            builder.Property(e => e.UnitPrice)
                .HasColumnType("decimal(18,2)")
                .IsRequired()
                .HasColumnName("unit_price");

            builder.Property(e => e.TotalPrice)
                .HasColumnType("decimal(18,2)")
                .IsRequired()
                .HasColumnName("total_price");
        }
    }
}