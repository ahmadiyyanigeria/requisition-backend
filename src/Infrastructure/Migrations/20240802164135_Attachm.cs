using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Attachm : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_purchase_order_attachments_attachment_id",
                table: "purchase_order");

            migrationBuilder.DropForeignKey(
                name: "FK_reimbursement_entries_attachments_attachment_id",
                table: "reimbursement_entries");

            migrationBuilder.DropForeignKey(
                name: "FK_retirement_entries_attachments_attachment_id",
                table: "retirement_entries");

            migrationBuilder.DropIndex(
                name: "IX_retirement_entries_attachment_id",
                table: "retirement_entries");

            migrationBuilder.DropIndex(
                name: "IX_reimbursement_entries_attachment_id",
                table: "reimbursement_entries");

            migrationBuilder.DropIndex(
                name: "IX_purchase_order_attachment_id",
                table: "purchase_order");

            migrationBuilder.DropColumn(
                name: "attachment_id",
                table: "retirement_entries");

            migrationBuilder.DropColumn(
                name: "attachment_id",
                table: "reimbursement_entries");

            migrationBuilder.DropColumn(
                name: "attachment_id",
                table: "purchase_order");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "attachment_id",
                table: "retirement_entries",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "attachment_id",
                table: "reimbursement_entries",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "attachment_id",
                table: "purchase_order",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_retirement_entries_attachment_id",
                table: "retirement_entries",
                column: "attachment_id");

            migrationBuilder.CreateIndex(
                name: "IX_reimbursement_entries_attachment_id",
                table: "reimbursement_entries",
                column: "attachment_id");

            migrationBuilder.CreateIndex(
                name: "IX_purchase_order_attachment_id",
                table: "purchase_order",
                column: "attachment_id");

            migrationBuilder.AddForeignKey(
                name: "FK_purchase_order_attachments_attachment_id",
                table: "purchase_order",
                column: "attachment_id",
                principalTable: "attachments",
                principalColumn: "attachment_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_reimbursement_entries_attachments_attachment_id",
                table: "reimbursement_entries",
                column: "attachment_id",
                principalTable: "attachments",
                principalColumn: "attachment_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_retirement_entries_attachments_attachment_id",
                table: "retirement_entries",
                column: "attachment_id",
                principalTable: "attachments",
                principalColumn: "attachment_id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
