using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Setup : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:CollationDefinition:case_insensitive", "en-u-ks-primary,en-u-ks-primary,icu,False");

            migrationBuilder.CreateTable(
                name: "CashAdvances",
                columns: table => new
                {
                    CashAdvanceId = table.Column<Guid>(type: "uuid", nullable: false),
                    RequisitionId = table.Column<Guid>(type: "uuid", nullable: false),
                    SubmitterId = table.Column<Guid>(type: "uuid", nullable: false),
                    AdvanceAmount = table.Column<decimal>(type: "numeric", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CashAdvances", x => x.CashAdvanceId);
                });

            migrationBuilder.CreateTable(
                name: "grant",
                columns: table => new
                {
                    grant_id = table.Column<Guid>(type: "uuid", nullable: false),
                    requisition_id = table.Column<Guid>(type: "uuid", nullable: false),
                    submitter_id = table.Column<Guid>(type: "uuid", nullable: false),
                    grant_amount = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    status = table.Column<int>(type: "integer", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_grant", x => x.grant_id);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    RoleId = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false, collation: "case_insensitive"),
                    Description = table.Column<string>(type: "text", nullable: false, collation: "case_insensitive")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.RoleId);
                });

            migrationBuilder.CreateTable(
                name: "Submitters",
                columns: table => new
                {
                    SubmitterId = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false, collation: "case_insensitive"),
                    UserId = table.Column<string>(type: "text", nullable: false, collation: "case_insensitive"),
                    Email = table.Column<string>(type: "text", nullable: true, collation: "case_insensitive"),
                    Position = table.Column<string>(type: "text", nullable: false, collation: "case_insensitive"),
                    Department = table.Column<string>(type: "text", nullable: false, collation: "case_insensitive")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Submitters", x => x.SubmitterId);
                });

            migrationBuilder.CreateTable(
                name: "Vendors",
                columns: table => new
                {
                    VendorId = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false, collation: "case_insensitive"),
                    Address = table.Column<string>(type: "text", nullable: false, collation: "case_insensitive"),
                    ContactPerson = table.Column<string>(type: "text", nullable: false, collation: "case_insensitive"),
                    ContactEmail = table.Column<string>(type: "text", nullable: false, collation: "case_insensitive"),
                    ContactPhone = table.Column<string>(type: "text", nullable: false, collation: "case_insensitive")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vendors", x => x.VendorId);
                });

            migrationBuilder.CreateTable(
                name: "RefundEntry",
                columns: table => new
                {
                    RefundEntryId = table.Column<Guid>(type: "uuid", nullable: false),
                    CashAdvanceId = table.Column<Guid>(type: "uuid", nullable: false),
                    Amount = table.Column<decimal>(type: "numeric", nullable: false),
                    Date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefundEntry", x => x.RefundEntryId);
                    table.ForeignKey(
                        name: "FK_RefundEntry_CashAdvances_CashAdvanceId",
                        column: x => x.CashAdvanceId,
                        principalTable: "CashAdvances",
                        principalColumn: "CashAdvanceId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "requisitions",
                columns: table => new
                {
                    requisition_id = table.Column<Guid>(type: "uuid", nullable: false),
                    submitter_id = table.Column<Guid>(type: "uuid", nullable: false),
                    description = table.Column<string>(type: "text", nullable: false, collation: "case_insensitive"),
                    expense_head = table.Column<string>(type: "varchar(100)", nullable: false, collation: "case_insensitive"),
                    status = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false),
                    requested_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    approved_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    rejected_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    total_amount = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    expense_account_id = table.Column<Guid>(type: "uuid", nullable: true),
                    requisition_type = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false),
                    department = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false, collation: "case_insensitive")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_requisitions", x => x.requisition_id);
                    table.ForeignKey(
                        name: "FK_requisitions_Submitters_submitter_id",
                        column: x => x.submitter_id,
                        principalTable: "Submitters",
                        principalColumn: "SubmitterId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "approval_flows",
                columns: table => new
                {
                    approval_flow_id = table.Column<Guid>(type: "uuid", nullable: false),
                    requisition_id = table.Column<Guid>(type: "uuid", nullable: false),
                    current_step = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_approval_flows", x => x.approval_flow_id);
                    table.ForeignKey(
                        name: "FK_approval_flows_requisitions_requisition_id",
                        column: x => x.requisition_id,
                        principalTable: "requisitions",
                        principalColumn: "requisition_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "attachments",
                columns: table => new
                {
                    attachment_id = table.Column<Guid>(type: "uuid", nullable: false),
                    file_name = table.Column<string>(type: "varchar(255)", nullable: false, collation: "case_insensitive"),
                    file_type = table.Column<string>(type: "varchar(50)", nullable: false, collation: "case_insensitive"),
                    file_content = table.Column<string>(type: "text", nullable: false, collation: "case_insensitive"),
                    uploaded_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    RequisitionId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_attachments", x => x.attachment_id);
                    table.ForeignKey(
                        name: "FK_attachments_requisitions_RequisitionId",
                        column: x => x.RequisitionId,
                        principalTable: "requisitions",
                        principalColumn: "requisition_id");
                });

            migrationBuilder.CreateTable(
                name: "BankAccounts",
                columns: table => new
                {
                    RequisitionId = table.Column<Guid>(type: "uuid", nullable: false),
                    bank_account_number = table.Column<string>(type: "varchar(50)", nullable: false, collation: "case_insensitive"),
                    bank_name = table.Column<string>(type: "varchar(100)", nullable: false, collation: "case_insensitive"),
                    account_name = table.Column<string>(type: "varchar(100)", nullable: false, collation: "case_insensitive"),
                    iban = table.Column<string>(type: "varchar(34)", nullable: false, collation: "case_insensitive"),
                    swift = table.Column<string>(type: "varchar(11)", nullable: false, collation: "case_insensitive")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BankAccounts", x => x.RequisitionId);
                    table.ForeignKey(
                        name: "FK_BankAccounts_requisitions_RequisitionId",
                        column: x => x.RequisitionId,
                        principalTable: "requisitions",
                        principalColumn: "requisition_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "requisition_items",
                columns: table => new
                {
                    requisition_item_id = table.Column<Guid>(type: "uuid", nullable: false),
                    requisition_id = table.Column<Guid>(type: "uuid", nullable: false),
                    description = table.Column<string>(type: "text", nullable: false, collation: "case_insensitive"),
                    quantity = table.Column<int>(type: "integer", nullable: false),
                    unit_price = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    total_price = table.Column<decimal>(type: "numeric(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_requisition_items", x => x.requisition_item_id);
                    table.ForeignKey(
                        name: "FK_requisition_items_requisitions_requisition_id",
                        column: x => x.requisition_id,
                        principalTable: "requisitions",
                        principalColumn: "requisition_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "approval_steps",
                columns: table => new
                {
                    approval_step_id = table.Column<Guid>(type: "uuid", nullable: false),
                    approver_id = table.Column<string>(type: "varchar(50)", nullable: false, collation: "case_insensitive"),
                    approval_flow_id = table.Column<Guid>(type: "uuid", nullable: false),
                    status = table.Column<string>(type: "varchar(20)", nullable: false),
                    approval_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    notes = table.Column<string>(type: "text", nullable: true, collation: "case_insensitive"),
                    approval_roles = table.Column<string>(type: "jsonb", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_approval_steps", x => x.approval_step_id);
                    table.ForeignKey(
                        name: "FK_approval_steps_approval_flows_approval_flow_id",
                        column: x => x.approval_flow_id,
                        principalTable: "approval_flows",
                        principalColumn: "approval_flow_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PurchaseOrders",
                columns: table => new
                {
                    PurchaseOrderId = table.Column<Guid>(type: "uuid", nullable: false),
                    RequisitionId = table.Column<Guid>(type: "uuid", nullable: false),
                    VendorId = table.Column<Guid>(type: "uuid", nullable: false),
                    OrderDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DeliveryDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    TotalAmount = table.Column<decimal>(type: "numeric", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    InvoiceAttachmentId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PurchaseOrders", x => x.PurchaseOrderId);
                    table.ForeignKey(
                        name: "FK_PurchaseOrders_Vendors_VendorId",
                        column: x => x.VendorId,
                        principalTable: "Vendors",
                        principalColumn: "VendorId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PurchaseOrders_attachments_InvoiceAttachmentId",
                        column: x => x.InvoiceAttachmentId,
                        principalTable: "attachments",
                        principalColumn: "attachment_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ReimbursementEntry",
                columns: table => new
                {
                    ReimbursementEntryId = table.Column<Guid>(type: "uuid", nullable: false),
                    CashAdvanceId = table.Column<Guid>(type: "uuid", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false, collation: "case_insensitive"),
                    Amount = table.Column<decimal>(type: "numeric", nullable: false),
                    Date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    AttachmentId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReimbursementEntry", x => x.ReimbursementEntryId);
                    table.ForeignKey(
                        name: "FK_ReimbursementEntry_CashAdvances_CashAdvanceId",
                        column: x => x.CashAdvanceId,
                        principalTable: "CashAdvances",
                        principalColumn: "CashAdvanceId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ReimbursementEntry_attachments_AttachmentId",
                        column: x => x.AttachmentId,
                        principalTable: "attachments",
                        principalColumn: "attachment_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RetirementEntry",
                columns: table => new
                {
                    RetirementEntryId = table.Column<Guid>(type: "uuid", nullable: false),
                    CashAdvanceId = table.Column<Guid>(type: "uuid", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false, collation: "case_insensitive"),
                    Amount = table.Column<decimal>(type: "numeric", nullable: false),
                    Date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ReceiptAttachmentId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RetirementEntry", x => x.RetirementEntryId);
                    table.ForeignKey(
                        name: "FK_RetirementEntry_CashAdvances_CashAdvanceId",
                        column: x => x.CashAdvanceId,
                        principalTable: "CashAdvances",
                        principalColumn: "CashAdvanceId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RetirementEntry_attachments_ReceiptAttachmentId",
                        column: x => x.ReceiptAttachmentId,
                        principalTable: "attachments",
                        principalColumn: "attachment_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Payment",
                columns: table => new
                {
                    PaymentId = table.Column<Guid>(type: "uuid", nullable: false),
                    Amount = table.Column<decimal>(type: "numeric", nullable: false),
                    PaymentDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    PaymentMethod = table.Column<int>(type: "integer", nullable: false),
                    ReferenceNumber = table.Column<string>(type: "text", nullable: false, collation: "case_insensitive"),
                    PurchaseOrderId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payment", x => x.PaymentId);
                    table.ForeignKey(
                        name: "FK_Payment_PurchaseOrders_PurchaseOrderId",
                        column: x => x.PurchaseOrderId,
                        principalTable: "PurchaseOrders",
                        principalColumn: "PurchaseOrderId");
                });

            migrationBuilder.CreateTable(
                name: "PurchaseOrderItems",
                columns: table => new
                {
                    PurchaseOrderItemId = table.Column<Guid>(type: "uuid", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false, collation: "case_insensitive"),
                    Quantity = table.Column<int>(type: "integer", nullable: false),
                    UnitPrice = table.Column<decimal>(type: "numeric", nullable: false),
                    PurchaseOrderId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PurchaseOrderItems", x => x.PurchaseOrderItemId);
                    table.ForeignKey(
                        name: "FK_PurchaseOrderItems_PurchaseOrders_PurchaseOrderId",
                        column: x => x.PurchaseOrderId,
                        principalTable: "PurchaseOrders",
                        principalColumn: "PurchaseOrderId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_approval_flows_requisition_id",
                table: "approval_flows",
                column: "requisition_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_approval_steps_approval_flow_id",
                table: "approval_steps",
                column: "approval_flow_id");

            migrationBuilder.CreateIndex(
                name: "IX_attachments_RequisitionId",
                table: "attachments",
                column: "RequisitionId");

            migrationBuilder.CreateIndex(
                name: "IX_Payment_PurchaseOrderId",
                table: "Payment",
                column: "PurchaseOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseOrderItems_PurchaseOrderId",
                table: "PurchaseOrderItems",
                column: "PurchaseOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseOrders_InvoiceAttachmentId",
                table: "PurchaseOrders",
                column: "InvoiceAttachmentId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseOrders_VendorId",
                table: "PurchaseOrders",
                column: "VendorId");

            migrationBuilder.CreateIndex(
                name: "IX_RefundEntry_CashAdvanceId",
                table: "RefundEntry",
                column: "CashAdvanceId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ReimbursementEntry_AttachmentId",
                table: "ReimbursementEntry",
                column: "AttachmentId");

            migrationBuilder.CreateIndex(
                name: "IX_ReimbursementEntry_CashAdvanceId",
                table: "ReimbursementEntry",
                column: "CashAdvanceId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_requisition_items_requisition_id",
                table: "requisition_items",
                column: "requisition_id");

            migrationBuilder.CreateIndex(
                name: "IX_requisitions_submitter_id",
                table: "requisitions",
                column: "submitter_id");

            migrationBuilder.CreateIndex(
                name: "IX_RetirementEntry_CashAdvanceId",
                table: "RetirementEntry",
                column: "CashAdvanceId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RetirementEntry_ReceiptAttachmentId",
                table: "RetirementEntry",
                column: "ReceiptAttachmentId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "approval_steps");

            migrationBuilder.DropTable(
                name: "BankAccounts");

            migrationBuilder.DropTable(
                name: "grant");

            migrationBuilder.DropTable(
                name: "Payment");

            migrationBuilder.DropTable(
                name: "PurchaseOrderItems");

            migrationBuilder.DropTable(
                name: "RefundEntry");

            migrationBuilder.DropTable(
                name: "ReimbursementEntry");

            migrationBuilder.DropTable(
                name: "requisition_items");

            migrationBuilder.DropTable(
                name: "RetirementEntry");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "approval_flows");

            migrationBuilder.DropTable(
                name: "PurchaseOrders");

            migrationBuilder.DropTable(
                name: "CashAdvances");

            migrationBuilder.DropTable(
                name: "Vendors");

            migrationBuilder.DropTable(
                name: "attachments");

            migrationBuilder.DropTable(
                name: "requisitions");

            migrationBuilder.DropTable(
                name: "Submitters");
        }
    }
}
