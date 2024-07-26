using Domain.Entities.Aggregates.PurchaseOrderAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.EntityTypeConfigurations
{
    public class PurchaseOrderEntityTypeConfiguration : IEntityTypeConfiguration<PurchaseOrder>
    {
        public void Configure(EntityTypeBuilder<PurchaseOrder> builder)
        {
            builder.ToTable("purchase_order");

            builder.HasKey(po => po.PurchaseOrderId);

            builder.Property(po => po.PurchaseOrderId)
                .IsRequired()
                .HasColumnName("purchase_order_id");

            builder.Property(po => po.RequisitionId)
                .IsRequired()
                .HasColumnName("requisition_id");

            builder.Property(po => po.VendorId)
                .IsRequired()
                .HasColumnName("vendor_id");

            builder.Property(po => po.OrderDate)
                .IsRequired()
                .HasColumnName("order_date");

            builder.Property(po => po.DeliveryDate)
                .HasColumnName("delivery_date");

            builder.Property(po => po.TotalAmount)
                .IsRequired()
                .HasColumnName("total_amount")
                .HasColumnType("decimal(18,2)");

            builder.Property(po => po.Status)
                .IsRequired()
                .HasColumnName("status");

            builder.HasOne(po => po.Vendor)
                .WithMany()
                .HasForeignKey(po => po.VendorId)
                .HasConstraintName("fk_vendor_id");
        }
    }
}

