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

            builder.HasMany(po => po.Items)
                .WithOne()
                .HasForeignKey(i => i.PurchaseOrderId)
                .HasConstraintName("fk_purchase_order_id")
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(po => po.Payments)
                .WithOne()
                .HasForeignKey(p => p.PurchaseOrderId)
                .HasConstraintName("fk_purchase_order_id")
                .OnDelete(DeleteBehavior.Restrict);

            builder.OwnsOne(po => po.Invoice, a =>
            {
                a.Property(i => i.FileName).HasColumnName("invoice_file_name");
                a.Property(i => i.FileType).HasColumnName("invoice_file_type");
                a.Property(i => i.FileContent).HasColumnName("invoice_file_content");
            });
        }
    }
}

