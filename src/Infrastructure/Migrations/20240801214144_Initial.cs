using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:CollationDefinition:case_insensitive", "en-u-ks-primary,en-u-ks-primary,icu,False");

            migrationBuilder.CreateTable(
                name: "cash_advances",
                columns: table => new
                {
                    cash_advance_id = table.Column<Guid>(type: "uuid", nullable: false),
                    requisition_id = table.Column<Guid>(type: "uuid", nullable: false),
                    submitter_id = table.Column<Guid>(type: "uuid", nullable: false),
                    advance_amount = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    bank_account_number = table.Column<string>(type: "varchar(50)", nullable: false, collation: "case_insensitive"),
                    bank_name = table.Column<string>(type: "varchar(100)", nullable: false, collation: "case_insensitive"),
                    account_name = table.Column<string>(type: "varchar(100)", nullable: false, collation: "case_insensitive"),
                    iban = table.Column<string>(type: "varchar(34)", nullable: false, collation: "case_insensitive"),
                    swift = table.Column<string>(type: "varchar(11)", nullable: false, collation: "case_insensitive"),
                    status = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false),
                    requested_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    disbursed_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    retired_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cash_advances", x => x.cash_advance_id);
                });

            migrationBuilder.CreateTable(
                name: "expense_head",
                columns: table => new
                {
                    name = table.Column<string>(type: "varchar(50)", nullable: false, collation: "case_insensitive"),
                    description = table.Column<string>(type: "varchar(100)", nullable: true, collation: "case_insensitive")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_expense_head", x => x.name);
                });

            migrationBuilder.CreateTable(
                name: "grant",
                columns: table => new
                {
                    grant_id = table.Column<Guid>(type: "uuid", nullable: false),
                    requisition_id = table.Column<Guid>(type: "uuid", nullable: false),
                    submitter_id = table.Column<Guid>(type: "uuid", nullable: false),
                    grant_amount = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    requested_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    disbursed_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    bank_account_number = table.Column<string>(type: "varchar(50)", nullable: false, collation: "case_insensitive"),
                    bank_name = table.Column<string>(type: "varchar(100)", nullable: false, collation: "case_insensitive"),
                    account_name = table.Column<string>(type: "varchar(100)", nullable: false, collation: "case_insensitive"),
                    iban = table.Column<string>(type: "varchar(34)", nullable: false, collation: "case_insensitive"),
                    swift = table.Column<string>(type: "varchar(11)", nullable: false, collation: "case_insensitive"),
                    status = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_grant", x => x.grant_id);
                });

            migrationBuilder.CreateTable(
                name: "submitter",
                columns: table => new
                {
                    submitter_id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "varchar(100)", nullable: false, collation: "case_insensitive"),
                    user_id = table.Column<string>(type: "varchar(100)", nullable: false, collation: "case_insensitive"),
                    email = table.Column<string>(type: "varchar(100)", nullable: false, collation: "case_insensitive"),
                    position = table.Column<string>(type: "varchar(100)", nullable: false, collation: "case_insensitive"),
                    department = table.Column<string>(type: "varchar(100)", nullable: false, collation: "case_insensitive")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_submitter", x => x.submitter_id);
                });

            migrationBuilder.CreateTable(
                name: "Vendors",
                columns: table => new
                {
                    VendorId = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false, collation: "case_insensitive"),
                    Address = table.Column<string>(type: "text", nullable: false, collation: "case_insensitive"),
                    ContactPerson = table.Column<string>(type: "text", nullable: false, collation: "case_insensitive"),
                    ContactEmail = table.Column<string>(type: "text", nullable: true, collation: "case_insensitive"),
                    ContactPhone = table.Column<string>(type: "text", nullable: false, collation: "case_insensitive")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vendors", x => x.VendorId);
                });

            migrationBuilder.CreateTable(
                name: "refund_entries",
                columns: table => new
                {
                    refund_entry_id = table.Column<Guid>(type: "uuid", nullable: false),
                    cash_advance_id = table.Column<Guid>(type: "uuid", nullable: false),
                    amount = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    bank_account_number = table.Column<string>(type: "varchar(50)", nullable: false, collation: "case_insensitive"),
                    bank_name = table.Column<string>(type: "varchar(100)", nullable: false, collation: "case_insensitive"),
                    account_name = table.Column<string>(type: "varchar(100)", nullable: false, collation: "case_insensitive"),
                    iban = table.Column<string>(type: "varchar(34)", nullable: false, collation: "case_insensitive"),
                    swift = table.Column<string>(type: "varchar(11)", nullable: false, collation: "case_insensitive")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_refund_entries", x => x.refund_entry_id);
                    table.ForeignKey(
                        name: "FK_refund_entries_cash_advances_cash_advance_id",
                        column: x => x.cash_advance_id,
                        principalTable: "cash_advances",
                        principalColumn: "cash_advance_id",
                        onDelete: ReferentialAction.Restrict);
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
                    LastDateModified = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    total_amount = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    expense_account_id = table.Column<Guid>(type: "uuid", nullable: true),
                    requisition_type = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false),
                    bank_account_number = table.Column<string>(type: "varchar(50)", nullable: true, collation: "case_insensitive"),
                    bank_name = table.Column<string>(type: "varchar(100)", nullable: true, collation: "case_insensitive"),
                    account_name = table.Column<string>(type: "varchar(100)", nullable: true, collation: "case_insensitive"),
                    iban = table.Column<string>(type: "varchar(34)", nullable: true, collation: "case_insensitive"),
                    swift = table.Column<string>(type: "varchar(11)", nullable: true, collation: "case_insensitive"),
                    department = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false, collation: "case_insensitive")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_requisitions", x => x.requisition_id);
                    table.ForeignKey(
                        name: "FK_requisitions_submitter_submitter_id",
                        column: x => x.submitter_id,
                        principalTable: "submitter",
                        principalColumn: "submitter_id",
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
                    order = table.Column<string>(type: "varchar(50)", nullable: false),
                    Role = table.Column<string>(type: "text", nullable: false, collation: "case_insensitive")
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
                name: "purchase_order",
                columns: table => new
                {
                    purchase_order_id = table.Column<Guid>(type: "uuid", nullable: false),
                    requisition_id = table.Column<Guid>(type: "uuid", nullable: false),
                    vendor_id = table.Column<Guid>(type: "uuid", nullable: false),
                    order_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    delivery_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    total_amount = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    status = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false),
                    attachment_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_purchase_order", x => x.purchase_order_id);
                    table.ForeignKey(
                        name: "FK_purchase_order_attachments_attachment_id",
                        column: x => x.attachment_id,
                        principalTable: "attachments",
                        principalColumn: "attachment_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_vendor_id",
                        column: x => x.vendor_id,
                        principalTable: "Vendors",
                        principalColumn: "VendorId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "reimbursement_entries",
                columns: table => new
                {
                    reimbursement_entry_id = table.Column<Guid>(type: "uuid", nullable: false),
                    cash_advance_id = table.Column<Guid>(type: "uuid", nullable: false),
                    description = table.Column<string>(type: "varchar(200)", nullable: false, collation: "case_insensitive"),
                    amount = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    attachment_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_reimbursement_entries", x => x.reimbursement_entry_id);
                    table.ForeignKey(
                        name: "FK_reimbursement_entries_attachments_attachment_id",
                        column: x => x.attachment_id,
                        principalTable: "attachments",
                        principalColumn: "attachment_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_reimbursement_entries_cash_advances_cash_advance_id",
                        column: x => x.cash_advance_id,
                        principalTable: "cash_advances",
                        principalColumn: "cash_advance_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "retirement_entries",
                columns: table => new
                {
                    retirement_entry_id = table.Column<Guid>(type: "uuid", nullable: false),
                    cash_advance_id = table.Column<Guid>(type: "uuid", nullable: false),
                    description = table.Column<string>(type: "text", maxLength: 500, nullable: false, collation: "case_insensitive"),
                    amount = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    attachment_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_retirement_entries", x => x.retirement_entry_id);
                    table.ForeignKey(
                        name: "FK_retirement_entries_attachments_attachment_id",
                        column: x => x.attachment_id,
                        principalTable: "attachments",
                        principalColumn: "attachment_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_retirement_entries_cash_advances_cash_advance_id",
                        column: x => x.cash_advance_id,
                        principalTable: "cash_advances",
                        principalColumn: "cash_advance_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Payment",
                columns: table => new
                {
                    PaymentId = table.Column<Guid>(type: "uuid", nullable: false),
                    PurchaseOrderId = table.Column<Guid>(type: "uuid", nullable: false),
                    Amount = table.Column<decimal>(type: "numeric", nullable: false),
                    PaymentDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    PaymentMethod = table.Column<int>(type: "integer", nullable: false),
                    ReferenceNumber = table.Column<string>(type: "text", nullable: false, collation: "case_insensitive")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payment", x => x.PaymentId);
                    table.ForeignKey(
                        name: "FK_Payment_purchase_order_PurchaseOrderId",
                        column: x => x.PurchaseOrderId,
                        principalTable: "purchase_order",
                        principalColumn: "purchase_order_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "purchase_order_items",
                columns: table => new
                {
                    purchase_order_item_id = table.Column<Guid>(type: "uuid", nullable: false),
                    purchase_order_id = table.Column<Guid>(type: "uuid", nullable: false),
                    description = table.Column<string>(type: "text", nullable: false, collation: "case_insensitive"),
                    quantity = table.Column<int>(type: "integer", nullable: false),
                    unit_price = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    total_price = table.Column<decimal>(type: "numeric(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_purchase_order_items", x => x.purchase_order_item_id);
                    table.ForeignKey(
                        name: "FK_purchase_order_items_purchase_order_purchase_order_id",
                        column: x => x.purchase_order_id,
                        principalTable: "purchase_order",
                        principalColumn: "purchase_order_id",
                        onDelete: ReferentialAction.Restrict);
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
                name: "IX_purchase_order_attachment_id",
                table: "purchase_order",
                column: "attachment_id");

            migrationBuilder.CreateIndex(
                name: "IX_purchase_order_vendor_id",
                table: "purchase_order",
                column: "vendor_id");

            migrationBuilder.CreateIndex(
                name: "IX_purchase_order_items_purchase_order_id",
                table: "purchase_order_items",
                column: "purchase_order_id");

            migrationBuilder.CreateIndex(
                name: "IX_refund_entries_cash_advance_id",
                table: "refund_entries",
                column: "cash_advance_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_reimbursement_entries_attachment_id",
                table: "reimbursement_entries",
                column: "attachment_id");

            migrationBuilder.CreateIndex(
                name: "IX_reimbursement_entries_cash_advance_id",
                table: "reimbursement_entries",
                column: "cash_advance_id",
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
                name: "IX_retirement_entries_attachment_id",
                table: "retirement_entries",
                column: "attachment_id");

            migrationBuilder.CreateIndex(
                name: "IX_retirement_entries_cash_advance_id",
                table: "retirement_entries",
                column: "cash_advance_id",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "approval_steps");

            migrationBuilder.DropTable(
                name: "expense_head");

            migrationBuilder.DropTable(
                name: "grant");

            migrationBuilder.DropTable(
                name: "Payment");

            migrationBuilder.DropTable(
                name: "purchase_order_items");

            migrationBuilder.DropTable(
                name: "refund_entries");

            migrationBuilder.DropTable(
                name: "reimbursement_entries");

            migrationBuilder.DropTable(
                name: "requisition_items");

            migrationBuilder.DropTable(
                name: "retirement_entries");

            migrationBuilder.DropTable(
                name: "approval_flows");

            migrationBuilder.DropTable(
                name: "purchase_order");

            migrationBuilder.DropTable(
                name: "cash_advances");

            migrationBuilder.DropTable(
                name: "attachments");

            migrationBuilder.DropTable(
                name: "Vendors");

            migrationBuilder.DropTable(
                name: "requisitions");

            migrationBuilder.DropTable(
                name: "submitter");
        }
    }
}
