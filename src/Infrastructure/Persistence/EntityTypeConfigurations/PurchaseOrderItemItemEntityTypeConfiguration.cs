using Domain.Entities.Aggregates.PurchaseOrderAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.EntityTypeConfigurations
{
    public class PurchaseOrderItemEntityTypeConfiguration : IEntityTypeConfiguration<PurchaseOrderItem>
    {
        public void Configure(EntityTypeBuilder<PurchaseOrderItem> builder)
        {
            builder.ToTable("purchase_order_items");

            builder.HasKey(e => e.PurchaseOrderItemId);

            builder.Property(e => e.PurchaseOrderItemId)
                .HasColumnName("purchase_order_item_id")
                .ValueGeneratedNever()
                .HasColumnType("uuid"); 

            builder.Property(e => e.PurchaseOrderId)
                .IsRequired()
                .HasColumnName("purchase_order_id")
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