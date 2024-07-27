using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class OrderStep : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Payment_PurchaseOrders_PurchaseOrderId",
                table: "Payment");

            migrationBuilder.DropForeignKey(
                name: "FK_PurchaseOrderItems_PurchaseOrders_PurchaseOrderId",
                table: "PurchaseOrderItems");

            migrationBuilder.DropForeignKey(
                name: "FK_PurchaseOrders_Vendors_VendorId",
                table: "PurchaseOrders");

            migrationBuilder.DropForeignKey(
                name: "FK_PurchaseOrders_attachments_InvoiceAttachmentId",
                table: "PurchaseOrders");

            migrationBuilder.DropForeignKey(
                name: "FK_RefundEntry_CashAdvances_CashAdvanceId",
                table: "RefundEntry");

            migrationBuilder.DropForeignKey(
                name: "FK_ReimbursementEntry_CashAdvances_CashAdvanceId",
                table: "ReimbursementEntry");

            migrationBuilder.DropForeignKey(
                name: "FK_RetirementEntry_CashAdvances_CashAdvanceId",
                table: "RetirementEntry");

            migrationBuilder.DropForeignKey(
                name: "FK_RetirementEntry_attachments_ReceiptAttachmentId",
                table: "RetirementEntry");

            migrationBuilder.DropIndex(
                name: "IX_ReimbursementEntry_CashAdvanceId",
                table: "ReimbursementEntry");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RetirementEntry",
                table: "RetirementEntry");

            migrationBuilder.DropIndex(
                name: "IX_RetirementEntry_CashAdvanceId",
                table: "RetirementEntry");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RefundEntry",
                table: "RefundEntry");

            migrationBuilder.DropIndex(
                name: "IX_RefundEntry_CashAdvanceId",
                table: "RefundEntry");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PurchaseOrders",
                table: "PurchaseOrders");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CashAdvances",
                table: "CashAdvances");

            migrationBuilder.DropColumn(
                name: "approval_roles",
                table: "approval_steps");

            migrationBuilder.RenameTable(
                name: "RetirementEntry",
                newName: "retirement_entries");

            migrationBuilder.RenameTable(
                name: "RefundEntry",
                newName: "refund_entries");

            migrationBuilder.RenameTable(
                name: "PurchaseOrders",
                newName: "purchase_order");

            migrationBuilder.RenameTable(
                name: "CashAdvances",
                newName: "cash_advances");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "retirement_entries",
                newName: "description");

            migrationBuilder.RenameColumn(
                name: "Date",
                table: "retirement_entries",
                newName: "date");

            migrationBuilder.RenameColumn(
                name: "Amount",
                table: "retirement_entries",
                newName: "amount");

            migrationBuilder.RenameColumn(
                name: "CashAdvanceId",
                table: "retirement_entries",
                newName: "cash_advance_id");

            migrationBuilder.RenameColumn(
                name: "RetirementEntryId",
                table: "retirement_entries",
                newName: "retirement_entry_id");

            migrationBuilder.RenameColumn(
                name: "ReceiptAttachmentId",
                table: "retirement_entries",
                newName: "receipt_id");

            migrationBuilder.RenameIndex(
                name: "IX_RetirementEntry_ReceiptAttachmentId",
                table: "retirement_entries",
                newName: "IX_retirement_entries_receipt_id");

            migrationBuilder.RenameColumn(
                name: "Date",
                table: "refund_entries",
                newName: "date");

            migrationBuilder.RenameColumn(
                name: "Amount",
                table: "refund_entries",
                newName: "amount");

            migrationBuilder.RenameColumn(
                name: "CashAdvanceId",
                table: "refund_entries",
                newName: "cash_advance_id");

            migrationBuilder.RenameColumn(
                name: "RefundEntryId",
                table: "refund_entries",
                newName: "refund_entry_id");

            migrationBuilder.RenameColumn(
                name: "Status",
                table: "purchase_order",
                newName: "status");

            migrationBuilder.RenameColumn(
                name: "VendorId",
                table: "purchase_order",
                newName: "vendor_id");

            migrationBuilder.RenameColumn(
                name: "TotalAmount",
                table: "purchase_order",
                newName: "total_amount");

            migrationBuilder.RenameColumn(
                name: "RequisitionId",
                table: "purchase_order",
                newName: "requisition_id");

            migrationBuilder.RenameColumn(
                name: "OrderDate",
                table: "purchase_order",
                newName: "order_date");

            migrationBuilder.RenameColumn(
                name: "DeliveryDate",
                table: "purchase_order",
                newName: "delivery_date");

            migrationBuilder.RenameColumn(
                name: "PurchaseOrderId",
                table: "purchase_order",
                newName: "purchase_order_id");

            migrationBuilder.RenameIndex(
                name: "IX_PurchaseOrders_VendorId",
                table: "purchase_order",
                newName: "IX_purchase_order_vendor_id");

            migrationBuilder.RenameIndex(
                name: "IX_PurchaseOrders_InvoiceAttachmentId",
                table: "purchase_order",
                newName: "IX_purchase_order_InvoiceAttachmentId");

            migrationBuilder.RenameColumn(
                name: "Status",
                table: "cash_advances",
                newName: "status");

            migrationBuilder.RenameColumn(
                name: "SubmitterId",
                table: "cash_advances",
                newName: "submitter_id");

            migrationBuilder.RenameColumn(
                name: "RequisitionId",
                table: "cash_advances",
                newName: "requisition_id");

            migrationBuilder.RenameColumn(
                name: "AdvanceAmount",
                table: "cash_advances",
                newName: "advance_amount");

            migrationBuilder.RenameColumn(
                name: "CashAdvanceId",
                table: "cash_advances",
                newName: "cash_advance_id");

            migrationBuilder.AlterColumn<string>(
                name: "Position",
                table: "Submitters",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                collation: "case_insensitive",
                oldClrType: typeof(string),
                oldType: "text",
                oldCollation: "case_insensitive");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Submitters",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                collation: "case_insensitive",
                oldClrType: typeof(string),
                oldType: "text",
                oldCollation: "case_insensitive");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Submitters",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "",
                collation: "case_insensitive",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true,
                oldCollation: "case_insensitive");

            migrationBuilder.AlterColumn<string>(
                name: "Department",
                table: "Submitters",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                collation: "case_insensitive",
                oldClrType: typeof(string),
                oldType: "text",
                oldCollation: "case_insensitive");

            migrationBuilder.AlterColumn<Guid>(
                name: "PurchaseOrderId",
                table: "PurchaseOrderItems",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "PurchaseOrderId",
                table: "Payment",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Role",
                table: "approval_steps",
                type: "text",
                nullable: false,
                defaultValue: "",
                collation: "case_insensitive");

            migrationBuilder.AddColumn<string>(
                name: "order",
                table: "approval_steps",
                type: "varchar(50)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "description",
                table: "retirement_entries",
                type: "character varying(500)",
                maxLength: 500,
                nullable: false,
                collation: "case_insensitive",
                oldClrType: typeof(string),
                oldType: "text",
                oldCollation: "case_insensitive");

            migrationBuilder.AlterColumn<decimal>(
                name: "amount",
                table: "retirement_entries",
                type: "numeric(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric");

            migrationBuilder.AlterColumn<decimal>(
                name: "amount",
                table: "refund_entries",
                type: "numeric(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric");

            migrationBuilder.AlterColumn<decimal>(
                name: "total_amount",
                table: "purchase_order",
                type: "numeric(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric");

            migrationBuilder.AlterColumn<decimal>(
                name: "advance_amount",
                table: "cash_advances",
                type: "numeric(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric");

            migrationBuilder.AddColumn<Guid>(
                name: "refund_entry_id",
                table: "cash_advances",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "reimbursement_entry_id",
                table: "cash_advances",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "retirement_entry_id",
                table: "cash_advances",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_retirement_entries",
                table: "retirement_entries",
                column: "retirement_entry_id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_refund_entries",
                table: "refund_entries",
                column: "refund_entry_id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_purchase_order",
                table: "purchase_order",
                column: "purchase_order_id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_cash_advances",
                table: "cash_advances",
                column: "cash_advance_id");

            migrationBuilder.CreateIndex(
                name: "IX_cash_advances_refund_entry_id",
                table: "cash_advances",
                column: "refund_entry_id");

            migrationBuilder.CreateIndex(
                name: "IX_cash_advances_reimbursement_entry_id",
                table: "cash_advances",
                column: "reimbursement_entry_id");

            migrationBuilder.CreateIndex(
                name: "IX_cash_advances_retirement_entry_id",
                table: "cash_advances",
                column: "retirement_entry_id");

            migrationBuilder.AddForeignKey(
                name: "FK_cash_advances_ReimbursementEntry_reimbursement_entry_id",
                table: "cash_advances",
                column: "reimbursement_entry_id",
                principalTable: "ReimbursementEntry",
                principalColumn: "ReimbursementEntryId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_cash_advances_refund_entries_refund_entry_id",
                table: "cash_advances",
                column: "refund_entry_id",
                principalTable: "refund_entries",
                principalColumn: "refund_entry_id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_cash_advances_retirement_entries_retirement_entry_id",
                table: "cash_advances",
                column: "retirement_entry_id",
                principalTable: "retirement_entries",
                principalColumn: "retirement_entry_id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Payment_purchase_order_PurchaseOrderId",
                table: "Payment",
                column: "PurchaseOrderId",
                principalTable: "purchase_order",
                principalColumn: "purchase_order_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_purchase_order_attachments_InvoiceAttachmentId",
                table: "purchase_order",
                column: "InvoiceAttachmentId",
                principalTable: "attachments",
                principalColumn: "attachment_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_vendor_id",
                table: "purchase_order",
                column: "vendor_id",
                principalTable: "Vendors",
                principalColumn: "VendorId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PurchaseOrderItems_purchase_order_PurchaseOrderId",
                table: "PurchaseOrderItems",
                column: "PurchaseOrderId",
                principalTable: "purchase_order",
                principalColumn: "purchase_order_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_retirement_entries_attachments_receipt_id",
                table: "retirement_entries",
                column: "receipt_id",
                principalTable: "attachments",
                principalColumn: "attachment_id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_cash_advances_ReimbursementEntry_reimbursement_entry_id",
                table: "cash_advances");

            migrationBuilder.DropForeignKey(
                name: "FK_cash_advances_refund_entries_refund_entry_id",
                table: "cash_advances");

            migrationBuilder.DropForeignKey(
                name: "FK_cash_advances_retirement_entries_retirement_entry_id",
                table: "cash_advances");

            migrationBuilder.DropForeignKey(
                name: "FK_Payment_purchase_order_PurchaseOrderId",
                table: "Payment");

            migrationBuilder.DropForeignKey(
                name: "FK_purchase_order_attachments_InvoiceAttachmentId",
                table: "purchase_order");

            migrationBuilder.DropForeignKey(
                name: "fk_vendor_id",
                table: "purchase_order");

            migrationBuilder.DropForeignKey(
                name: "FK_PurchaseOrderItems_purchase_order_PurchaseOrderId",
                table: "PurchaseOrderItems");

            migrationBuilder.DropForeignKey(
                name: "FK_retirement_entries_attachments_receipt_id",
                table: "retirement_entries");

            migrationBuilder.DropPrimaryKey(
                name: "PK_retirement_entries",
                table: "retirement_entries");

            migrationBuilder.DropPrimaryKey(
                name: "PK_refund_entries",
                table: "refund_entries");

            migrationBuilder.DropPrimaryKey(
                name: "PK_purchase_order",
                table: "purchase_order");

            migrationBuilder.DropPrimaryKey(
                name: "PK_cash_advances",
                table: "cash_advances");

            migrationBuilder.DropIndex(
                name: "IX_cash_advances_refund_entry_id",
                table: "cash_advances");

            migrationBuilder.DropIndex(
                name: "IX_cash_advances_reimbursement_entry_id",
                table: "cash_advances");

            migrationBuilder.DropIndex(
                name: "IX_cash_advances_retirement_entry_id",
                table: "cash_advances");

            migrationBuilder.DropColumn(
                name: "Role",
                table: "approval_steps");

            migrationBuilder.DropColumn(
                name: "order",
                table: "approval_steps");

            migrationBuilder.DropColumn(
                name: "refund_entry_id",
                table: "cash_advances");

            migrationBuilder.DropColumn(
                name: "reimbursement_entry_id",
                table: "cash_advances");

            migrationBuilder.DropColumn(
                name: "retirement_entry_id",
                table: "cash_advances");

            migrationBuilder.RenameTable(
                name: "retirement_entries",
                newName: "RetirementEntry");

            migrationBuilder.RenameTable(
                name: "refund_entries",
                newName: "RefundEntry");

            migrationBuilder.RenameTable(
                name: "purchase_order",
                newName: "PurchaseOrders");

            migrationBuilder.RenameTable(
                name: "cash_advances",
                newName: "CashAdvances");

            migrationBuilder.RenameColumn(
                name: "description",
                table: "RetirementEntry",
                newName: "Description");

            migrationBuilder.RenameColumn(
                name: "date",
                table: "RetirementEntry",
                newName: "Date");

            migrationBuilder.RenameColumn(
                name: "amount",
                table: "RetirementEntry",
                newName: "Amount");

            migrationBuilder.RenameColumn(
                name: "cash_advance_id",
                table: "RetirementEntry",
                newName: "CashAdvanceId");

            migrationBuilder.RenameColumn(
                name: "retirement_entry_id",
                table: "RetirementEntry",
                newName: "RetirementEntryId");

            migrationBuilder.RenameColumn(
                name: "receipt_id",
                table: "RetirementEntry",
                newName: "ReceiptAttachmentId");

            migrationBuilder.RenameIndex(
                name: "IX_retirement_entries_receipt_id",
                table: "RetirementEntry",
                newName: "IX_RetirementEntry_ReceiptAttachmentId");

            migrationBuilder.RenameColumn(
                name: "date",
                table: "RefundEntry",
                newName: "Date");

            migrationBuilder.RenameColumn(
                name: "amount",
                table: "RefundEntry",
                newName: "Amount");

            migrationBuilder.RenameColumn(
                name: "cash_advance_id",
                table: "RefundEntry",
                newName: "CashAdvanceId");

            migrationBuilder.RenameColumn(
                name: "refund_entry_id",
                table: "RefundEntry",
                newName: "RefundEntryId");

            migrationBuilder.RenameColumn(
                name: "status",
                table: "PurchaseOrders",
                newName: "Status");

            migrationBuilder.RenameColumn(
                name: "vendor_id",
                table: "PurchaseOrders",
                newName: "VendorId");

            migrationBuilder.RenameColumn(
                name: "total_amount",
                table: "PurchaseOrders",
                newName: "TotalAmount");

            migrationBuilder.RenameColumn(
                name: "requisition_id",
                table: "PurchaseOrders",
                newName: "RequisitionId");

            migrationBuilder.RenameColumn(
                name: "order_date",
                table: "PurchaseOrders",
                newName: "OrderDate");

            migrationBuilder.RenameColumn(
                name: "delivery_date",
                table: "PurchaseOrders",
                newName: "DeliveryDate");

            migrationBuilder.RenameColumn(
                name: "purchase_order_id",
                table: "PurchaseOrders",
                newName: "PurchaseOrderId");

            migrationBuilder.RenameIndex(
                name: "IX_purchase_order_vendor_id",
                table: "PurchaseOrders",
                newName: "IX_PurchaseOrders_VendorId");

            migrationBuilder.RenameIndex(
                name: "IX_purchase_order_InvoiceAttachmentId",
                table: "PurchaseOrders",
                newName: "IX_PurchaseOrders_InvoiceAttachmentId");

            migrationBuilder.RenameColumn(
                name: "status",
                table: "CashAdvances",
                newName: "Status");

            migrationBuilder.RenameColumn(
                name: "submitter_id",
                table: "CashAdvances",
                newName: "SubmitterId");

            migrationBuilder.RenameColumn(
                name: "requisition_id",
                table: "CashAdvances",
                newName: "RequisitionId");

            migrationBuilder.RenameColumn(
                name: "advance_amount",
                table: "CashAdvances",
                newName: "AdvanceAmount");

            migrationBuilder.RenameColumn(
                name: "cash_advance_id",
                table: "CashAdvances",
                newName: "CashAdvanceId");

            migrationBuilder.AlterColumn<string>(
                name: "Position",
                table: "Submitters",
                type: "text",
                nullable: false,
                collation: "case_insensitive",
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100,
                oldCollation: "case_insensitive");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Submitters",
                type: "text",
                nullable: false,
                collation: "case_insensitive",
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100,
                oldCollation: "case_insensitive");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Submitters",
                type: "text",
                nullable: true,
                collation: "case_insensitive",
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100,
                oldCollation: "case_insensitive");

            migrationBuilder.AlterColumn<string>(
                name: "Department",
                table: "Submitters",
                type: "text",
                nullable: false,
                collation: "case_insensitive",
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100,
                oldCollation: "case_insensitive");

            migrationBuilder.AlterColumn<Guid>(
                name: "PurchaseOrderId",
                table: "PurchaseOrderItems",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AlterColumn<Guid>(
                name: "PurchaseOrderId",
                table: "Payment",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AddColumn<string>(
                name: "approval_roles",
                table: "approval_steps",
                type: "jsonb",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "RetirementEntry",
                type: "text",
                nullable: false,
                collation: "case_insensitive",
                oldClrType: typeof(string),
                oldType: "character varying(500)",
                oldMaxLength: 500,
                oldCollation: "case_insensitive");

            migrationBuilder.AlterColumn<decimal>(
                name: "Amount",
                table: "RetirementEntry",
                type: "numeric",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(18,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "Amount",
                table: "RefundEntry",
                type: "numeric",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(18,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "TotalAmount",
                table: "PurchaseOrders",
                type: "numeric",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(18,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "AdvanceAmount",
                table: "CashAdvances",
                type: "numeric",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(18,2)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RetirementEntry",
                table: "RetirementEntry",
                column: "RetirementEntryId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RefundEntry",
                table: "RefundEntry",
                column: "RefundEntryId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PurchaseOrders",
                table: "PurchaseOrders",
                column: "PurchaseOrderId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CashAdvances",
                table: "CashAdvances",
                column: "CashAdvanceId");

            migrationBuilder.CreateIndex(
                name: "IX_ReimbursementEntry_CashAdvanceId",
                table: "ReimbursementEntry",
                column: "CashAdvanceId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RetirementEntry_CashAdvanceId",
                table: "RetirementEntry",
                column: "CashAdvanceId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RefundEntry_CashAdvanceId",
                table: "RefundEntry",
                column: "CashAdvanceId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Payment_PurchaseOrders_PurchaseOrderId",
                table: "Payment",
                column: "PurchaseOrderId",
                principalTable: "PurchaseOrders",
                principalColumn: "PurchaseOrderId");

            migrationBuilder.AddForeignKey(
                name: "FK_PurchaseOrderItems_PurchaseOrders_PurchaseOrderId",
                table: "PurchaseOrderItems",
                column: "PurchaseOrderId",
                principalTable: "PurchaseOrders",
                principalColumn: "PurchaseOrderId");

            migrationBuilder.AddForeignKey(
                name: "FK_PurchaseOrders_Vendors_VendorId",
                table: "PurchaseOrders",
                column: "VendorId",
                principalTable: "Vendors",
                principalColumn: "VendorId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PurchaseOrders_attachments_InvoiceAttachmentId",
                table: "PurchaseOrders",
                column: "InvoiceAttachmentId",
                principalTable: "attachments",
                principalColumn: "attachment_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RefundEntry_CashAdvances_CashAdvanceId",
                table: "RefundEntry",
                column: "CashAdvanceId",
                principalTable: "CashAdvances",
                principalColumn: "CashAdvanceId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ReimbursementEntry_CashAdvances_CashAdvanceId",
                table: "ReimbursementEntry",
                column: "CashAdvanceId",
                principalTable: "CashAdvances",
                principalColumn: "CashAdvanceId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RetirementEntry_CashAdvances_CashAdvanceId",
                table: "RetirementEntry",
                column: "CashAdvanceId",
                principalTable: "CashAdvances",
                principalColumn: "CashAdvanceId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RetirementEntry_attachments_ReceiptAttachmentId",
                table: "RetirementEntry",
                column: "ReceiptAttachmentId",
                principalTable: "attachments",
                principalColumn: "attachment_id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
