using Domain.Entities.Aggregates.RequisitionAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.EntityTypeConfigurations
{
    public class RequisitionEntityTypeConfiguration : IEntityTypeConfiguration<Requisition>
    {
        public void Configure(EntityTypeBuilder<Requisition> builder)
        {
            builder.ToTable("requisitions"); // Specify table name if different

            builder.HasKey(e => e.RequisitionId);

            builder.Property(e => e.RequisitionId)
                .HasColumnName("requisition_id")
                .ValueGeneratedNever()
                .HasColumnType("uuid"); 

            builder.Property(e => e.SubmitterId)
                .IsRequired()
                .HasColumnName("submitter_id")
                .HasColumnType("uuid"); 

            builder.Property(e => e.Description)
                .IsRequired()
                .HasColumnName("description")
                .HasColumnType("text"); 

            builder.Property(e => e.ExpenseHead)
                .IsRequired()
                .HasColumnName("expense_head")
                .HasColumnType("varchar(100)"); 

            builder.Property(e => e.Status)
                .IsRequired()
                .HasMaxLength(20)
                .HasColumnName("status")
                .HasColumnType("varchar(20)");

            builder.Property(e => e.RequestedDate)
                .IsRequired()
                .HasColumnName("requested_date")
                .HasColumnType("timestamp with time zone");

            builder.Property(e => e.ApprovedDate)
                .HasColumnName("approved_date")
                .HasColumnType("timestamp with time zone"); 

            builder.Property(e => e.RejectedDate)
                .HasColumnName("rejected_date")
                .HasColumnType("timestamp with time zone"); 

            builder.Property(e => e.TotalAmount)
                .HasColumnType("decimal(18,2)")
                .IsRequired()
                .HasColumnName("total_amount");

            builder.Property(e => e.ExpenseAccountId)
                .HasColumnName("expense_account_id")
                .HasColumnType("uuid"); 

            builder.Property(e => e.RequisitionType)
                .IsRequired()
                .HasMaxLength(20)
                .HasColumnName("requisition_type")
                .HasColumnType("varchar(20)"); 

            builder.Property(e => e.Department)
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnName("department")
                .HasColumnType("varchar(100)"); 

            // Configure ApprovalFlow as a separate entity
            builder.HasOne(e => e.ApprovalFlow)
                .WithOne()
                .HasForeignKey<ApprovalFlow>(e => e.RequisitionId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            // Configure RequisitionItems as a collection
            builder.HasMany(e => e.Items)
                .WithOne()
                .HasForeignKey(item => item.RequisitionId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            // Configure BankAccount as a value object
            builder.OwnsOne(e => e.BankAccount, ba =>
            {
                ba.Property(b => b.AccountNumber)
                    .HasColumnName("bank_account_number")
                    .IsRequired()
                    .HasColumnType("varchar(50)"); 

                ba.Property(b => b.BankName)
                    .HasColumnName("bank_name")
                    .IsRequired()
                    .HasColumnType("varchar(100)"); 

                ba.Property(b => b.AccountName)
                    .HasColumnName("account_name")
                    .IsRequired()
                    .HasColumnType("varchar(100)"); 

                ba.Property(b => b.IBAN)
                    .HasColumnName("iban")
                    .HasColumnType("varchar(34)"); 

                ba.Property(b => b.SWIFT)
                    .HasColumnName("swift")
                    .HasColumnType("varchar(11)");
            });
        }
    }
}