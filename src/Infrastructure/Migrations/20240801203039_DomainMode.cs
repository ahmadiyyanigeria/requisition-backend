using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class DomainMode : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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
                name: "FK_purchase_order_attachments_InvoiceAttachmentId",
                table: "purchase_order");

            migrationBuilder.DropForeignKey(
                name: "FK_PurchaseOrderItems_purchase_order_PurchaseOrderId",
                table: "PurchaseOrderItems");

            migrationBuilder.DropForeignKey(
                name: "FK_ReimbursementEntry_attachments_AttachmentId",
                table: "ReimbursementEntry");

            migrationBuilder.DropForeignKey(
                name: "FK_requisitions_Submitters_submitter_id",
                table: "requisitions");

            migrationBuilder.DropForeignKey(
                name: "FK_retirement_entries_attachments_receipt_id",
                table: "retirement_entries");

            migrationBuilder.DropTable(
                name: "BankAccounts");

            migrationBuilder.DropIndex(
                name: "IX_cash_advances_refund_entry_id",
                table: "cash_advances");

            migrationBuilder.DropIndex(
                name: "IX_cash_advances_reimbursement_entry_id",
                table: "cash_advances");

            migrationBuilder.DropIndex(
                name: "IX_cash_advances_retirement_entry_id",
                table: "cash_advances");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Submitters",
                table: "Submitters");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ReimbursementEntry",
                table: "ReimbursementEntry");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PurchaseOrderItems",
                table: "PurchaseOrderItems");

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
                name: "Submitters",
                newName: "submitter");

            migrationBuilder.RenameTable(
                name: "ReimbursementEntry",
                newName: "reimbursement_entries");

            migrationBuilder.RenameTable(
                name: "PurchaseOrderItems",
                newName: "purchase_order_items");

            migrationBuilder.RenameColumn(
                name: "receipt_id",
                table: "retirement_entries",
                newName: "attachment_id");

            migrationBuilder.RenameIndex(
                name: "IX_retirement_entries_receipt_id",
                table: "retirement_entries",
                newName: "IX_retirement_entries_attachment_id");

            migrationBuilder.RenameColumn(
                name: "InvoiceAttachmentId",
                table: "purchase_order",
                newName: "attachment_id");

            migrationBuilder.RenameIndex(
                name: "IX_purchase_order_InvoiceAttachmentId",
                table: "purchase_order",
                newName: "IX_purchase_order_attachment_id");

            migrationBuilder.RenameColumn(
                name: "Position",
                table: "submitter",
                newName: "position");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "submitter",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "Email",
                table: "submitter",
                newName: "email");

            migrationBuilder.RenameColumn(
                name: "Department",
                table: "submitter",
                newName: "department");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "submitter",
                newName: "user_id");

            migrationBuilder.RenameColumn(
                name: "SubmitterId",
                table: "submitter",
                newName: "submitter_id");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "reimbursement_entries",
                newName: "description");

            migrationBuilder.RenameColumn(
                name: "Date",
                table: "reimbursement_entries",
                newName: "date");

            migrationBuilder.RenameColumn(
                name: "Amount",
                table: "reimbursement_entries",
                newName: "amount");

            migrationBuilder.RenameColumn(
                name: "CashAdvanceId",
                table: "reimbursement_entries",
                newName: "cash_advance_id");

            migrationBuilder.RenameColumn(
                name: "AttachmentId",
                table: "reimbursement_entries",
                newName: "attachment_id");

            migrationBuilder.RenameColumn(
                name: "ReimbursementEntryId",
                table: "reimbursement_entries",
                newName: "reimbursement_entry_id");

            migrationBuilder.RenameIndex(
                name: "IX_ReimbursementEntry_AttachmentId",
                table: "reimbursement_entries",
                newName: "IX_reimbursement_entries_attachment_id");

            migrationBuilder.RenameColumn(
                name: "Quantity",
                table: "purchase_order_items",
                newName: "quantity");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "purchase_order_items",
                newName: "description");

            migrationBuilder.RenameColumn(
                name: "UnitPrice",
                table: "purchase_order_items",
                newName: "unit_price");

            migrationBuilder.RenameColumn(
                name: "PurchaseOrderId",
                table: "purchase_order_items",
                newName: "purchase_order_id");

            migrationBuilder.RenameColumn(
                name: "PurchaseOrderItemId",
                table: "purchase_order_items",
                newName: "purchase_order_item_id");

            migrationBuilder.RenameIndex(
                name: "IX_PurchaseOrderItems_PurchaseOrderId",
                table: "purchase_order_items",
                newName: "IX_purchase_order_items_purchase_order_id");

            migrationBuilder.AlterColumn<string>(
                name: "description",
                table: "retirement_entries",
                type: "text",
                maxLength: 500,
                nullable: false,
                collation: "case_insensitive",
                oldClrType: typeof(string),
                oldType: "character varying(500)",
                oldMaxLength: 500,
                oldCollation: "case_insensitive");

            migrationBuilder.AddColumn<string>(
                name: "account_name",
                table: "requisitions",
                type: "varchar(100)",
                nullable: true,
                collation: "case_insensitive");

            migrationBuilder.AddColumn<string>(
                name: "bank_account_number",
                table: "requisitions",
                type: "varchar(50)",
                nullable: true,
                collation: "case_insensitive");

            migrationBuilder.AddColumn<string>(
                name: "bank_name",
                table: "requisitions",
                type: "varchar(100)",
                nullable: true,
                collation: "case_insensitive");

            migrationBuilder.AddColumn<string>(
                name: "iban",
                table: "requisitions",
                type: "varchar(34)",
                nullable: true,
                collation: "case_insensitive");

            migrationBuilder.AddColumn<string>(
                name: "swift",
                table: "requisitions",
                type: "varchar(11)",
                nullable: true,
                collation: "case_insensitive");

            migrationBuilder.AddColumn<string>(
                name: "account_name",
                table: "refund_entries",
                type: "varchar(100)",
                nullable: false,
                defaultValue: "",
                collation: "case_insensitive");

            migrationBuilder.AddColumn<string>(
                name: "bank_account_number",
                table: "refund_entries",
                type: "varchar(50)",
                nullable: false,
                defaultValue: "",
                collation: "case_insensitive");

            migrationBuilder.AddColumn<string>(
                name: "bank_name",
                table: "refund_entries",
                type: "varchar(100)",
                nullable: false,
                defaultValue: "",
                collation: "case_insensitive");

            migrationBuilder.AddColumn<string>(
                name: "iban",
                table: "refund_entries",
                type: "varchar(34)",
                nullable: false,
                defaultValue: "",
                collation: "case_insensitive");

            migrationBuilder.AddColumn<string>(
                name: "swift",
                table: "refund_entries",
                type: "varchar(11)",
                nullable: false,
                defaultValue: "",
                collation: "case_insensitive");

            migrationBuilder.AlterColumn<string>(
                name: "status",
                table: "purchase_order",
                type: "varchar(20)",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<string>(
                name: "status",
                table: "grant",
                type: "varchar(20)",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer",
                oldMaxLength: 20);

            migrationBuilder.AddColumn<string>(
                name: "account_name",
                table: "grant",
                type: "varchar(100)",
                nullable: false,
                defaultValue: "",
                collation: "case_insensitive");

            migrationBuilder.AddColumn<string>(
                name: "bank_account_number",
                table: "grant",
                type: "varchar(50)",
                nullable: false,
                defaultValue: "",
                collation: "case_insensitive");

            migrationBuilder.AddColumn<string>(
                name: "bank_name",
                table: "grant",
                type: "varchar(100)",
                nullable: false,
                defaultValue: "",
                collation: "case_insensitive");

            migrationBuilder.AddColumn<string>(
                name: "iban",
                table: "grant",
                type: "varchar(34)",
                nullable: false,
                defaultValue: "",
                collation: "case_insensitive");

            migrationBuilder.AddColumn<string>(
                name: "swift",
                table: "grant",
                type: "varchar(11)",
                nullable: false,
                defaultValue: "",
                collation: "case_insensitive");

            migrationBuilder.AlterColumn<string>(
                name: "status",
                table: "cash_advances",
                type: "varchar(20)",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddColumn<string>(
                name: "account_name",
                table: "cash_advances",
                type: "varchar(100)",
                nullable: false,
                defaultValue: "",
                collation: "case_insensitive");

            migrationBuilder.AddColumn<DateTime>(
                name: "approved_date",
                table: "cash_advances",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "bank_account_number",
                table: "cash_advances",
                type: "varchar(50)",
                nullable: false,
                defaultValue: "",
                collation: "case_insensitive");

            migrationBuilder.AddColumn<string>(
                name: "bank_name",
                table: "cash_advances",
                type: "varchar(100)",
                nullable: false,
                defaultValue: "",
                collation: "case_insensitive");

            migrationBuilder.AddColumn<DateTime>(
                name: "disbursed_date",
                table: "cash_advances",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "iban",
                table: "cash_advances",
                type: "varchar(34)",
                nullable: false,
                defaultValue: "",
                collation: "case_insensitive");

            migrationBuilder.AddColumn<DateTime>(
                name: "requested_date",
                table: "cash_advances",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "retired_date",
                table: "cash_advances",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "swift",
                table: "cash_advances",
                type: "varchar(11)",
                nullable: false,
                defaultValue: "",
                collation: "case_insensitive");

            migrationBuilder.AlterColumn<string>(
                name: "position",
                table: "submitter",
                type: "varchar(100)",
                nullable: false,
                collation: "case_insensitive",
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100,
                oldCollation: "case_insensitive");

            migrationBuilder.AlterColumn<string>(
                name: "name",
                table: "submitter",
                type: "varchar(100)",
                nullable: false,
                collation: "case_insensitive",
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100,
                oldCollation: "case_insensitive");

            migrationBuilder.AlterColumn<string>(
                name: "email",
                table: "submitter",
                type: "varchar(100)",
                nullable: false,
                collation: "case_insensitive",
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100,
                oldCollation: "case_insensitive");

            migrationBuilder.AlterColumn<string>(
                name: "department",
                table: "submitter",
                type: "varchar(100)",
                nullable: false,
                collation: "case_insensitive",
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100,
                oldCollation: "case_insensitive");

            migrationBuilder.AlterColumn<string>(
                name: "user_id",
                table: "submitter",
                type: "varchar(100)",
                nullable: false,
                collation: "case_insensitive",
                oldClrType: typeof(string),
                oldType: "text",
                oldCollation: "case_insensitive");

            migrationBuilder.AlterColumn<string>(
                name: "description",
                table: "reimbursement_entries",
                type: "varchar(200)",
                nullable: false,
                collation: "case_insensitive",
                oldClrType: typeof(string),
                oldType: "text",
                oldCollation: "case_insensitive");

            migrationBuilder.AlterColumn<decimal>(
                name: "amount",
                table: "reimbursement_entries",
                type: "numeric(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric");

            migrationBuilder.AlterColumn<decimal>(
                name: "unit_price",
                table: "purchase_order_items",
                type: "numeric(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric");

            migrationBuilder.AddColumn<decimal>(
                name: "total_price",
                table: "purchase_order_items",
                type: "numeric(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddPrimaryKey(
                name: "PK_submitter",
                table: "submitter",
                column: "submitter_id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_reimbursement_entries",
                table: "reimbursement_entries",
                column: "reimbursement_entry_id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_purchase_order_items",
                table: "purchase_order_items",
                column: "purchase_order_item_id");

            migrationBuilder.CreateIndex(
                name: "IX_retirement_entries_cash_advance_id",
                table: "retirement_entries",
                column: "cash_advance_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_refund_entries_cash_advance_id",
                table: "refund_entries",
                column: "cash_advance_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_reimbursement_entries_cash_advance_id",
                table: "reimbursement_entries",
                column: "cash_advance_id",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_purchase_order_attachments_attachment_id",
                table: "purchase_order",
                column: "attachment_id",
                principalTable: "attachments",
                principalColumn: "attachment_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_purchase_order_items_purchase_order_purchase_order_id",
                table: "purchase_order_items",
                column: "purchase_order_id",
                principalTable: "purchase_order",
                principalColumn: "purchase_order_id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_refund_entries_cash_advances_cash_advance_id",
                table: "refund_entries",
                column: "cash_advance_id",
                principalTable: "cash_advances",
                principalColumn: "cash_advance_id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_reimbursement_entries_attachments_attachment_id",
                table: "reimbursement_entries",
                column: "attachment_id",
                principalTable: "attachments",
                principalColumn: "attachment_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_reimbursement_entries_cash_advances_cash_advance_id",
                table: "reimbursement_entries",
                column: "cash_advance_id",
                principalTable: "cash_advances",
                principalColumn: "cash_advance_id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_requisitions_submitter_submitter_id",
                table: "requisitions",
                column: "submitter_id",
                principalTable: "submitter",
                principalColumn: "submitter_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_retirement_entries_attachments_attachment_id",
                table: "retirement_entries",
                column: "attachment_id",
                principalTable: "attachments",
                principalColumn: "attachment_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_retirement_entries_cash_advances_cash_advance_id",
                table: "retirement_entries",
                column: "cash_advance_id",
                principalTable: "cash_advances",
                principalColumn: "cash_advance_id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_purchase_order_attachments_attachment_id",
                table: "purchase_order");

            migrationBuilder.DropForeignKey(
                name: "FK_purchase_order_items_purchase_order_purchase_order_id",
                table: "purchase_order_items");

            migrationBuilder.DropForeignKey(
                name: "FK_refund_entries_cash_advances_cash_advance_id",
                table: "refund_entries");

            migrationBuilder.DropForeignKey(
                name: "FK_reimbursement_entries_attachments_attachment_id",
                table: "reimbursement_entries");

            migrationBuilder.DropForeignKey(
                name: "FK_reimbursement_entries_cash_advances_cash_advance_id",
                table: "reimbursement_entries");

            migrationBuilder.DropForeignKey(
                name: "FK_requisitions_submitter_submitter_id",
                table: "requisitions");

            migrationBuilder.DropForeignKey(
                name: "FK_retirement_entries_attachments_attachment_id",
                table: "retirement_entries");

            migrationBuilder.DropForeignKey(
                name: "FK_retirement_entries_cash_advances_cash_advance_id",
                table: "retirement_entries");

            migrationBuilder.DropIndex(
                name: "IX_retirement_entries_cash_advance_id",
                table: "retirement_entries");

            migrationBuilder.DropIndex(
                name: "IX_refund_entries_cash_advance_id",
                table: "refund_entries");

            migrationBuilder.DropPrimaryKey(
                name: "PK_submitter",
                table: "submitter");

            migrationBuilder.DropPrimaryKey(
                name: "PK_reimbursement_entries",
                table: "reimbursement_entries");

            migrationBuilder.DropIndex(
                name: "IX_reimbursement_entries_cash_advance_id",
                table: "reimbursement_entries");

            migrationBuilder.DropPrimaryKey(
                name: "PK_purchase_order_items",
                table: "purchase_order_items");

            migrationBuilder.DropColumn(
                name: "account_name",
                table: "requisitions");

            migrationBuilder.DropColumn(
                name: "bank_account_number",
                table: "requisitions");

            migrationBuilder.DropColumn(
                name: "bank_name",
                table: "requisitions");

            migrationBuilder.DropColumn(
                name: "iban",
                table: "requisitions");

            migrationBuilder.DropColumn(
                name: "swift",
                table: "requisitions");

            migrationBuilder.DropColumn(
                name: "account_name",
                table: "refund_entries");

            migrationBuilder.DropColumn(
                name: "bank_account_number",
                table: "refund_entries");

            migrationBuilder.DropColumn(
                name: "bank_name",
                table: "refund_entries");

            migrationBuilder.DropColumn(
                name: "iban",
                table: "refund_entries");

            migrationBuilder.DropColumn(
                name: "swift",
                table: "refund_entries");

            migrationBuilder.DropColumn(
                name: "account_name",
                table: "grant");

            migrationBuilder.DropColumn(
                name: "bank_account_number",
                table: "grant");

            migrationBuilder.DropColumn(
                name: "bank_name",
                table: "grant");

            migrationBuilder.DropColumn(
                name: "iban",
                table: "grant");

            migrationBuilder.DropColumn(
                name: "swift",
                table: "grant");

            migrationBuilder.DropColumn(
                name: "account_name",
                table: "cash_advances");

            migrationBuilder.DropColumn(
                name: "approved_date",
                table: "cash_advances");

            migrationBuilder.DropColumn(
                name: "bank_account_number",
                table: "cash_advances");

            migrationBuilder.DropColumn(
                name: "bank_name",
                table: "cash_advances");

            migrationBuilder.DropColumn(
                name: "disbursed_date",
                table: "cash_advances");

            migrationBuilder.DropColumn(
                name: "iban",
                table: "cash_advances");

            migrationBuilder.DropColumn(
                name: "requested_date",
                table: "cash_advances");

            migrationBuilder.DropColumn(
                name: "retired_date",
                table: "cash_advances");

            migrationBuilder.DropColumn(
                name: "swift",
                table: "cash_advances");

            migrationBuilder.DropColumn(
                name: "total_price",
                table: "purchase_order_items");

            migrationBuilder.RenameTable(
                name: "submitter",
                newName: "Submitters");

            migrationBuilder.RenameTable(
                name: "reimbursement_entries",
                newName: "ReimbursementEntry");

            migrationBuilder.RenameTable(
                name: "purchase_order_items",
                newName: "PurchaseOrderItems");

            migrationBuilder.RenameColumn(
                name: "attachment_id",
                table: "retirement_entries",
                newName: "receipt_id");

            migrationBuilder.RenameIndex(
                name: "IX_retirement_entries_attachment_id",
                table: "retirement_entries",
                newName: "IX_retirement_entries_receipt_id");

            migrationBuilder.RenameColumn(
                name: "attachment_id",
                table: "purchase_order",
                newName: "InvoiceAttachmentId");

            migrationBuilder.RenameIndex(
                name: "IX_purchase_order_attachment_id",
                table: "purchase_order",
                newName: "IX_purchase_order_InvoiceAttachmentId");

            migrationBuilder.RenameColumn(
                name: "position",
                table: "Submitters",
                newName: "Position");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "Submitters",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "email",
                table: "Submitters",
                newName: "Email");

            migrationBuilder.RenameColumn(
                name: "department",
                table: "Submitters",
                newName: "Department");

            migrationBuilder.RenameColumn(
                name: "user_id",
                table: "Submitters",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "submitter_id",
                table: "Submitters",
                newName: "SubmitterId");

            migrationBuilder.RenameColumn(
                name: "description",
                table: "ReimbursementEntry",
                newName: "Description");

            migrationBuilder.RenameColumn(
                name: "date",
                table: "ReimbursementEntry",
                newName: "Date");

            migrationBuilder.RenameColumn(
                name: "amount",
                table: "ReimbursementEntry",
                newName: "Amount");

            migrationBuilder.RenameColumn(
                name: "cash_advance_id",
                table: "ReimbursementEntry",
                newName: "CashAdvanceId");

            migrationBuilder.RenameColumn(
                name: "attachment_id",
                table: "ReimbursementEntry",
                newName: "AttachmentId");

            migrationBuilder.RenameColumn(
                name: "reimbursement_entry_id",
                table: "ReimbursementEntry",
                newName: "ReimbursementEntryId");

            migrationBuilder.RenameIndex(
                name: "IX_reimbursement_entries_attachment_id",
                table: "ReimbursementEntry",
                newName: "IX_ReimbursementEntry_AttachmentId");

            migrationBuilder.RenameColumn(
                name: "quantity",
                table: "PurchaseOrderItems",
                newName: "Quantity");

            migrationBuilder.RenameColumn(
                name: "description",
                table: "PurchaseOrderItems",
                newName: "Description");

            migrationBuilder.RenameColumn(
                name: "unit_price",
                table: "PurchaseOrderItems",
                newName: "UnitPrice");

            migrationBuilder.RenameColumn(
                name: "purchase_order_id",
                table: "PurchaseOrderItems",
                newName: "PurchaseOrderId");

            migrationBuilder.RenameColumn(
                name: "purchase_order_item_id",
                table: "PurchaseOrderItems",
                newName: "PurchaseOrderItemId");

            migrationBuilder.RenameIndex(
                name: "IX_purchase_order_items_purchase_order_id",
                table: "PurchaseOrderItems",
                newName: "IX_PurchaseOrderItems_PurchaseOrderId");

            migrationBuilder.AlterColumn<string>(
                name: "description",
                table: "retirement_entries",
                type: "character varying(500)",
                maxLength: 500,
                nullable: false,
                collation: "case_insensitive",
                oldClrType: typeof(string),
                oldType: "text",
                oldMaxLength: 500,
                oldCollation: "case_insensitive");

            migrationBuilder.AlterColumn<int>(
                name: "status",
                table: "purchase_order",
                type: "integer",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(20)",
                oldMaxLength: 20);

            migrationBuilder.AlterColumn<int>(
                name: "status",
                table: "grant",
                type: "integer",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(20)",
                oldMaxLength: 20);

            migrationBuilder.AlterColumn<int>(
                name: "status",
                table: "cash_advances",
                type: "integer",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(20)",
                oldMaxLength: 20);

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

            migrationBuilder.AlterColumn<string>(
                name: "Position",
                table: "Submitters",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                collation: "case_insensitive",
                oldClrType: typeof(string),
                oldType: "varchar(100)",
                oldCollation: "case_insensitive");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Submitters",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                collation: "case_insensitive",
                oldClrType: typeof(string),
                oldType: "varchar(100)",
                oldCollation: "case_insensitive");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Submitters",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                collation: "case_insensitive",
                oldClrType: typeof(string),
                oldType: "varchar(100)",
                oldCollation: "case_insensitive");

            migrationBuilder.AlterColumn<string>(
                name: "Department",
                table: "Submitters",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                collation: "case_insensitive",
                oldClrType: typeof(string),
                oldType: "varchar(100)",
                oldCollation: "case_insensitive");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Submitters",
                type: "text",
                nullable: false,
                collation: "case_insensitive",
                oldClrType: typeof(string),
                oldType: "varchar(100)",
                oldCollation: "case_insensitive");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "ReimbursementEntry",
                type: "text",
                nullable: false,
                collation: "case_insensitive",
                oldClrType: typeof(string),
                oldType: "varchar(200)",
                oldCollation: "case_insensitive");

            migrationBuilder.AlterColumn<decimal>(
                name: "Amount",
                table: "ReimbursementEntry",
                type: "numeric",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(18,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "UnitPrice",
                table: "PurchaseOrderItems",
                type: "numeric",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(18,2)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Submitters",
                table: "Submitters",
                column: "SubmitterId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ReimbursementEntry",
                table: "ReimbursementEntry",
                column: "ReimbursementEntryId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PurchaseOrderItems",
                table: "PurchaseOrderItems",
                column: "PurchaseOrderItemId");

            migrationBuilder.CreateTable(
                name: "BankAccounts",
                columns: table => new
                {
                    RequisitionId = table.Column<Guid>(type: "uuid", nullable: false),
                    account_name = table.Column<string>(type: "varchar(100)", nullable: false, collation: "case_insensitive"),
                    bank_account_number = table.Column<string>(type: "varchar(50)", nullable: false, collation: "case_insensitive"),
                    bank_name = table.Column<string>(type: "varchar(100)", nullable: false, collation: "case_insensitive"),
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
                name: "FK_purchase_order_attachments_InvoiceAttachmentId",
                table: "purchase_order",
                column: "InvoiceAttachmentId",
                principalTable: "attachments",
                principalColumn: "attachment_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PurchaseOrderItems_purchase_order_PurchaseOrderId",
                table: "PurchaseOrderItems",
                column: "PurchaseOrderId",
                principalTable: "purchase_order",
                principalColumn: "purchase_order_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ReimbursementEntry_attachments_AttachmentId",
                table: "ReimbursementEntry",
                column: "AttachmentId",
                principalTable: "attachments",
                principalColumn: "attachment_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_requisitions_Submitters_submitter_id",
                table: "requisitions",
                column: "submitter_id",
                principalTable: "Submitters",
                principalColumn: "SubmitterId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_retirement_entries_attachments_receipt_id",
                table: "retirement_entries",
                column: "receipt_id",
                principalTable: "attachments",
                principalColumn: "attachment_id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
