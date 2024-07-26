﻿// <auto-generated />
using System;
using Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Infrastructure.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:CollationDefinition:case_insensitive", "en-u-ks-primary,en-u-ks-primary,icu,False")
                .HasAnnotation("ProductVersion", "8.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Domain.Entities.Aggregates.CashAdvanceAggregate.CashAdvance", b =>
                {
                    b.Property<Guid>("CashAdvanceId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<decimal>("AdvanceAmount")
                        .HasColumnType("numeric");

                    b.Property<Guid>("RequisitionId")
                        .HasColumnType("uuid");

                    b.Property<int>("Status")
                        .HasColumnType("integer");

                    b.Property<Guid>("SubmitterId")
                        .HasColumnType("uuid");

                    b.HasKey("CashAdvanceId");

                    b.ToTable("CashAdvances");
                });

            modelBuilder.Entity("Domain.Entities.Aggregates.CashAdvanceAggregate.RefundEntry", b =>
                {
                    b.Property<Guid>("RefundEntryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<decimal>("Amount")
                        .HasColumnType("numeric");

                    b.Property<Guid>("CashAdvanceId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("Date")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("RefundEntryId");

                    b.HasIndex("CashAdvanceId")
                        .IsUnique();

                    b.ToTable("RefundEntry");
                });

            modelBuilder.Entity("Domain.Entities.Aggregates.CashAdvanceAggregate.ReimbursementEntry", b =>
                {
                    b.Property<Guid>("ReimbursementEntryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<decimal>("Amount")
                        .HasColumnType("numeric");

                    b.Property<Guid>("AttachmentId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("CashAdvanceId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("Date")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text")
                        .UseCollation("case_insensitive");

                    b.HasKey("ReimbursementEntryId");

                    b.HasIndex("AttachmentId");

                    b.HasIndex("CashAdvanceId")
                        .IsUnique();

                    b.ToTable("ReimbursementEntry");
                });

            modelBuilder.Entity("Domain.Entities.Aggregates.CashAdvanceAggregate.RetirementEntry", b =>
                {
                    b.Property<Guid>("RetirementEntryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<decimal>("Amount")
                        .HasColumnType("numeric");

                    b.Property<Guid>("CashAdvanceId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("Date")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text")
                        .UseCollation("case_insensitive");

                    b.Property<Guid>("ReceiptAttachmentId")
                        .HasColumnType("uuid");

                    b.HasKey("RetirementEntryId");

                    b.HasIndex("CashAdvanceId")
                        .IsUnique();

                    b.HasIndex("ReceiptAttachmentId");

                    b.ToTable("RetirementEntry");
                });

            modelBuilder.Entity("Domain.Entities.Aggregates.GrantAggregate.Grant", b =>
                {
                    b.Property<Guid>("GrantId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("grant_id");

                    b.Property<decimal>("GrantAmount")
                        .HasColumnType("decimal(18, 2)")
                        .HasColumnName("grant_amount");

                    b.Property<Guid>("RequisitionId")
                        .HasColumnType("uuid")
                        .HasColumnName("requisition_id");

                    b.Property<int>("Status")
                        .HasMaxLength(20)
                        .HasColumnType("integer")
                        .HasColumnName("status");

                    b.Property<Guid>("SubmitterId")
                        .HasColumnType("uuid")
                        .HasColumnName("submitter_id");

                    b.HasKey("GrantId");

                    b.ToTable("grant", (string)null);
                });

            modelBuilder.Entity("Domain.Entities.Aggregates.PurchaseOrderAggregate.Payment", b =>
                {
                    b.Property<Guid>("PaymentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<decimal>("Amount")
                        .HasColumnType("numeric");

                    b.Property<DateTime>("PaymentDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("PaymentMethod")
                        .HasColumnType("integer");

                    b.Property<Guid?>("PurchaseOrderId")
                        .HasColumnType("uuid");

                    b.Property<string>("ReferenceNumber")
                        .IsRequired()
                        .HasColumnType("text")
                        .UseCollation("case_insensitive");

                    b.HasKey("PaymentId");

                    b.HasIndex("PurchaseOrderId");

                    b.ToTable("Payment");
                });

            modelBuilder.Entity("Domain.Entities.Aggregates.PurchaseOrderAggregate.PurchaseOrder", b =>
                {
                    b.Property<Guid>("PurchaseOrderId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime?>("DeliveryDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("InvoiceAttachmentId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("OrderDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("RequisitionId")
                        .HasColumnType("uuid");

                    b.Property<int>("Status")
                        .HasColumnType("integer");

                    b.Property<decimal>("TotalAmount")
                        .HasColumnType("numeric");

                    b.Property<Guid>("VendorId")
                        .HasColumnType("uuid");

                    b.HasKey("PurchaseOrderId");

                    b.HasIndex("InvoiceAttachmentId");

                    b.HasIndex("VendorId");

                    b.ToTable("PurchaseOrders");
                });

            modelBuilder.Entity("Domain.Entities.Aggregates.PurchaseOrderAggregate.PurchaseOrderItem", b =>
                {
                    b.Property<Guid>("PurchaseOrderItemId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text")
                        .UseCollation("case_insensitive");

                    b.Property<Guid?>("PurchaseOrderId")
                        .HasColumnType("uuid");

                    b.Property<int>("Quantity")
                        .HasColumnType("integer");

                    b.Property<decimal>("UnitPrice")
                        .HasColumnType("numeric");

                    b.HasKey("PurchaseOrderItemId");

                    b.HasIndex("PurchaseOrderId");

                    b.ToTable("PurchaseOrderItems");
                });

            modelBuilder.Entity("Domain.Entities.Aggregates.PurchaseOrderAggregate.Vendor", b =>
                {
                    b.Property<Guid>("VendorId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("text")
                        .UseCollation("case_insensitive");

                    b.Property<string>("ContactEmail")
                        .IsRequired()
                        .HasColumnType("text")
                        .UseCollation("case_insensitive");

                    b.Property<string>("ContactPerson")
                        .IsRequired()
                        .HasColumnType("text")
                        .UseCollation("case_insensitive");

                    b.Property<string>("ContactPhone")
                        .IsRequired()
                        .HasColumnType("text")
                        .UseCollation("case_insensitive");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text")
                        .UseCollation("case_insensitive");

                    b.HasKey("VendorId");

                    b.ToTable("Vendors");
                });

            modelBuilder.Entity("Domain.Entities.Aggregates.RequisitionAggregate.ApprovalFlow", b =>
                {
                    b.Property<Guid>("ApprovalFlowId")
                        .HasColumnType("uuid")
                        .HasColumnName("approval_flow_id");

                    b.Property<int>("CurrentStep")
                        .HasColumnType("integer")
                        .HasColumnName("current_step");

                    b.Property<Guid>("RequisitionId")
                        .HasColumnType("uuid")
                        .HasColumnName("requisition_id");

                    b.HasKey("ApprovalFlowId");

                    b.HasIndex("RequisitionId")
                        .IsUnique();

                    b.ToTable("approval_flows", (string)null);
                });

            modelBuilder.Entity("Domain.Entities.Aggregates.RequisitionAggregate.ApprovalStep", b =>
                {
                    b.Property<Guid>("ApprovalStepId")
                        .HasColumnType("uuid")
                        .HasColumnName("approval_step_id");

                    b.Property<DateTime>("ApprovalDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("approval_date");

                    b.Property<Guid>("ApprovalFlowId")
                        .HasColumnType("uuid")
                        .HasColumnName("approval_flow_id");

                    b.Property<string>("ApprovalRoles")
                        .IsRequired()
                        .HasColumnType("jsonb")
                        .HasColumnName("approval_roles");

                    b.Property<string>("ApproverId")
                        .IsRequired()
                        .HasColumnType("varchar(50)")
                        .HasColumnName("approver_id")
                        .UseCollation("case_insensitive");

                    b.Property<string>("Notes")
                        .HasColumnType("text")
                        .HasColumnName("notes")
                        .UseCollation("case_insensitive");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("varchar(20)")
                        .HasColumnName("status");

                    b.HasKey("ApprovalStepId");

                    b.HasIndex("ApprovalFlowId");

                    b.ToTable("approval_steps", (string)null);
                });

            modelBuilder.Entity("Domain.Entities.Aggregates.RequisitionAggregate.Requisition", b =>
                {
                    b.Property<Guid>("RequisitionId")
                        .HasColumnType("uuid")
                        .HasColumnName("requisition_id");

                    b.Property<DateTime?>("ApprovedDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("approved_date");

                    b.Property<string>("Department")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)")
                        .HasColumnName("department")
                        .UseCollation("case_insensitive");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("description")
                        .UseCollation("case_insensitive");

                    b.Property<Guid?>("ExpenseAccountId")
                        .HasColumnType("uuid")
                        .HasColumnName("expense_account_id");

                    b.Property<string>("ExpenseHead")
                        .IsRequired()
                        .HasColumnType("varchar(100)")
                        .HasColumnName("expense_head")
                        .UseCollation("case_insensitive");

                    b.Property<DateTime?>("LastDateModified")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime?>("RejectedDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("rejected_date");

                    b.Property<DateTime>("RequestedDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("requested_date");

                    b.Property<string>("RequisitionType")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("varchar(20)")
                        .HasColumnName("requisition_type");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("varchar(20)")
                        .HasColumnName("status");

                    b.Property<Guid>("SubmitterId")
                        .HasColumnType("uuid")
                        .HasColumnName("submitter_id");

                    b.Property<decimal>("TotalAmount")
                        .HasColumnType("decimal(18,2)")
                        .HasColumnName("total_amount");

                    b.HasKey("RequisitionId");

                    b.HasIndex("SubmitterId");

                    b.ToTable("requisitions", (string)null);
                });

            modelBuilder.Entity("Domain.Entities.Aggregates.RequisitionAggregate.RequisitionItem", b =>
                {
                    b.Property<Guid>("RequisitionItemId")
                        .HasColumnType("uuid")
                        .HasColumnName("requisition_item_id");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("description")
                        .UseCollation("case_insensitive");

                    b.Property<int>("Quantity")
                        .HasColumnType("integer")
                        .HasColumnName("quantity");

                    b.Property<Guid>("RequisitionId")
                        .HasColumnType("uuid")
                        .HasColumnName("requisition_id");

                    b.Property<decimal>("TotalPrice")
                        .HasColumnType("decimal(18,2)")
                        .HasColumnName("total_price");

                    b.Property<decimal>("UnitPrice")
                        .HasColumnType("decimal(18,2)")
                        .HasColumnName("unit_price");

                    b.HasKey("RequisitionItemId");

                    b.HasIndex("RequisitionId");

                    b.ToTable("requisition_items", (string)null);
                });

            modelBuilder.Entity("Domain.Entities.Aggregates.SubmitterAggregate.Submitter", b =>
                {
                    b.Property<Guid>("SubmitterId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Department")
                        .IsRequired()
                        .HasColumnType("text")
                        .UseCollation("case_insensitive");

                    b.Property<string>("Email")
                        .HasColumnType("text")
                        .UseCollation("case_insensitive");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text")
                        .UseCollation("case_insensitive");

                    b.Property<string>("Position")
                        .IsRequired()
                        .HasColumnType("text")
                        .UseCollation("case_insensitive");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("text")
                        .UseCollation("case_insensitive");

                    b.HasKey("SubmitterId");

                    b.ToTable("Submitters");
                });

            modelBuilder.Entity("Domain.Entities.Common.Attachment", b =>
                {
                    b.Property<Guid>("AttachmentId")
                        .HasColumnType("uuid")
                        .HasColumnName("attachment_id");

                    b.Property<string>("FileContent")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("file_content")
                        .UseCollation("case_insensitive");

                    b.Property<string>("FileName")
                        .IsRequired()
                        .HasColumnType("varchar(255)")
                        .HasColumnName("file_name")
                        .UseCollation("case_insensitive");

                    b.Property<string>("FileType")
                        .IsRequired()
                        .HasColumnType("varchar(50)")
                        .HasColumnName("file_type")
                        .UseCollation("case_insensitive");

                    b.Property<Guid?>("RequisitionId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("UploadedDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("uploaded_date");

                    b.HasKey("AttachmentId");

                    b.HasIndex("RequisitionId");

                    b.ToTable("attachments", (string)null);
                });

            modelBuilder.Entity("Domain.Entities.Common.ExpenseHead", b =>
                {
                    b.Property<string>("Name")
                        .HasColumnType("varchar(50)")
                        .HasColumnName("name")
                        .UseCollation("case_insensitive");

                    b.Property<string>("Description")
                        .HasColumnType("varchar(100)")
                        .HasColumnName("description")
                        .UseCollation("case_insensitive");

                    b.HasKey("Name");

                    b.ToTable("expense_head", (string)null);
                });

            modelBuilder.Entity("Domain.Entities.Common.Role", b =>
                {
                    b.Property<Guid>("RoleId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text")
                        .UseCollation("case_insensitive");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text")
                        .UseCollation("case_insensitive");

                    b.HasKey("RoleId");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("Domain.Entities.Aggregates.CashAdvanceAggregate.RefundEntry", b =>
                {
                    b.HasOne("Domain.Entities.Aggregates.CashAdvanceAggregate.CashAdvance", null)
                        .WithOne("RefundEntry")
                        .HasForeignKey("Domain.Entities.Aggregates.CashAdvanceAggregate.RefundEntry", "CashAdvanceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Domain.Entities.Aggregates.CashAdvanceAggregate.ReimbursementEntry", b =>
                {
                    b.HasOne("Domain.Entities.Common.Attachment", "Receipt")
                        .WithMany()
                        .HasForeignKey("AttachmentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Entities.Aggregates.CashAdvanceAggregate.CashAdvance", null)
                        .WithOne("ReimbursementEntry")
                        .HasForeignKey("Domain.Entities.Aggregates.CashAdvanceAggregate.ReimbursementEntry", "CashAdvanceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Receipt");
                });

            modelBuilder.Entity("Domain.Entities.Aggregates.CashAdvanceAggregate.RetirementEntry", b =>
                {
                    b.HasOne("Domain.Entities.Aggregates.CashAdvanceAggregate.CashAdvance", null)
                        .WithOne("RetirementEntry")
                        .HasForeignKey("Domain.Entities.Aggregates.CashAdvanceAggregate.RetirementEntry", "CashAdvanceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Entities.Common.Attachment", "Receipt")
                        .WithMany()
                        .HasForeignKey("ReceiptAttachmentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Receipt");
                });

            modelBuilder.Entity("Domain.Entities.Aggregates.PurchaseOrderAggregate.Payment", b =>
                {
                    b.HasOne("Domain.Entities.Aggregates.PurchaseOrderAggregate.PurchaseOrder", null)
                        .WithMany("Payments")
                        .HasForeignKey("PurchaseOrderId");
                });

            modelBuilder.Entity("Domain.Entities.Aggregates.PurchaseOrderAggregate.PurchaseOrder", b =>
                {
                    b.HasOne("Domain.Entities.Common.Attachment", "Invoice")
                        .WithMany()
                        .HasForeignKey("InvoiceAttachmentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Entities.Aggregates.PurchaseOrderAggregate.Vendor", "Vendor")
                        .WithMany()
                        .HasForeignKey("VendorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Invoice");

                    b.Navigation("Vendor");
                });

            modelBuilder.Entity("Domain.Entities.Aggregates.PurchaseOrderAggregate.PurchaseOrderItem", b =>
                {
                    b.HasOne("Domain.Entities.Aggregates.PurchaseOrderAggregate.PurchaseOrder", null)
                        .WithMany("Items")
                        .HasForeignKey("PurchaseOrderId");
                });

            modelBuilder.Entity("Domain.Entities.Aggregates.RequisitionAggregate.ApprovalFlow", b =>
                {
                    b.HasOne("Domain.Entities.Aggregates.RequisitionAggregate.Requisition", null)
                        .WithOne("ApprovalFlow")
                        .HasForeignKey("Domain.Entities.Aggregates.RequisitionAggregate.ApprovalFlow", "RequisitionId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("Domain.Entities.Aggregates.RequisitionAggregate.ApprovalStep", b =>
                {
                    b.HasOne("Domain.Entities.Aggregates.RequisitionAggregate.ApprovalFlow", null)
                        .WithMany("ApproverSteps")
                        .HasForeignKey("ApprovalFlowId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("Domain.Entities.Aggregates.RequisitionAggregate.Requisition", b =>
                {
                    b.HasOne("Domain.Entities.Aggregates.SubmitterAggregate.Submitter", "Submitter")
                        .WithMany()
                        .HasForeignKey("SubmitterId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.OwnsOne("Domain.Entities.ValueObjects.BankAccount", "BankAccount", b1 =>
                        {
                            b1.Property<Guid>("RequisitionId")
                                .HasColumnType("uuid");

                            b1.Property<string>("AccountName")
                                .IsRequired()
                                .HasColumnType("varchar(100)")
                                .HasColumnName("account_name")
                                .UseCollation("case_insensitive");

                            b1.Property<string>("AccountNumber")
                                .IsRequired()
                                .HasColumnType("varchar(50)")
                                .HasColumnName("bank_account_number")
                                .UseCollation("case_insensitive");

                            b1.Property<string>("BankName")
                                .IsRequired()
                                .HasColumnType("varchar(100)")
                                .HasColumnName("bank_name")
                                .UseCollation("case_insensitive");

                            b1.Property<string>("IBAN")
                                .IsRequired()
                                .HasColumnType("varchar(34)")
                                .HasColumnName("iban")
                                .UseCollation("case_insensitive");

                            b1.Property<string>("SWIFT")
                                .IsRequired()
                                .HasColumnType("varchar(11)")
                                .HasColumnName("swift")
                                .UseCollation("case_insensitive");

                            b1.HasKey("RequisitionId");

                            b1.ToTable("BankAccounts");

                            b1.WithOwner()
                                .HasForeignKey("RequisitionId");
                        });

                    b.Navigation("BankAccount");

                    b.Navigation("Submitter");
                });

            modelBuilder.Entity("Domain.Entities.Aggregates.RequisitionAggregate.RequisitionItem", b =>
                {
                    b.HasOne("Domain.Entities.Aggregates.RequisitionAggregate.Requisition", null)
                        .WithMany("Items")
                        .HasForeignKey("RequisitionId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("Domain.Entities.Common.Attachment", b =>
                {
                    b.HasOne("Domain.Entities.Aggregates.RequisitionAggregate.Requisition", null)
                        .WithMany("Attachments")
                        .HasForeignKey("RequisitionId");
                });

            modelBuilder.Entity("Domain.Entities.Aggregates.CashAdvanceAggregate.CashAdvance", b =>
                {
                    b.Navigation("RefundEntry")
                        .IsRequired();

                    b.Navigation("ReimbursementEntry")
                        .IsRequired();

                    b.Navigation("RetirementEntry")
                        .IsRequired();
                });

            modelBuilder.Entity("Domain.Entities.Aggregates.PurchaseOrderAggregate.PurchaseOrder", b =>
                {
                    b.Navigation("Items");

                    b.Navigation("Payments");
                });

            modelBuilder.Entity("Domain.Entities.Aggregates.RequisitionAggregate.ApprovalFlow", b =>
                {
                    b.Navigation("ApproverSteps");
                });

            modelBuilder.Entity("Domain.Entities.Aggregates.RequisitionAggregate.Requisition", b =>
                {
                    b.Navigation("ApprovalFlow")
                        .IsRequired();

                    b.Navigation("Attachments");

                    b.Navigation("Items");
                });
#pragma warning restore 612, 618
        }
    }
}
