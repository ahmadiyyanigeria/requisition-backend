using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class StatusRefund : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "status",
                table: "reimbursement_entries",
                type: "varchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "swift",
                table: "refund_entries",
                type: "varchar(11)",
                nullable: true,
                collation: "case_insensitive",
                oldClrType: typeof(string),
                oldType: "varchar(11)",
                oldCollation: "case_insensitive");

            migrationBuilder.AlterColumn<string>(
                name: "iban",
                table: "refund_entries",
                type: "varchar(34)",
                nullable: true,
                collation: "case_insensitive",
                oldClrType: typeof(string),
                oldType: "varchar(34)",
                oldCollation: "case_insensitive");

            migrationBuilder.AlterColumn<string>(
                name: "bank_name",
                table: "refund_entries",
                type: "varchar(100)",
                nullable: true,
                collation: "case_insensitive",
                oldClrType: typeof(string),
                oldType: "varchar(100)",
                oldCollation: "case_insensitive");

            migrationBuilder.AlterColumn<string>(
                name: "bank_account_number",
                table: "refund_entries",
                type: "varchar(50)",
                nullable: true,
                collation: "case_insensitive",
                oldClrType: typeof(string),
                oldType: "varchar(50)",
                oldCollation: "case_insensitive");

            migrationBuilder.AlterColumn<string>(
                name: "account_name",
                table: "refund_entries",
                type: "varchar(100)",
                nullable: true,
                collation: "case_insensitive",
                oldClrType: typeof(string),
                oldType: "varchar(100)",
                oldCollation: "case_insensitive");

            migrationBuilder.AddColumn<string>(
                name: "status",
                table: "refund_entries",
                type: "varchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_cash_advances_requisition_id",
                table: "cash_advances",
                column: "requisition_id");

            migrationBuilder.AddForeignKey(
                name: "FK_cash_advances_requisitions_requisition_id",
                table: "cash_advances",
                column: "requisition_id",
                principalTable: "requisitions",
                principalColumn: "requisition_id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_cash_advances_requisitions_requisition_id",
                table: "cash_advances");

            migrationBuilder.DropIndex(
                name: "IX_cash_advances_requisition_id",
                table: "cash_advances");

            migrationBuilder.DropColumn(
                name: "status",
                table: "reimbursement_entries");

            migrationBuilder.DropColumn(
                name: "status",
                table: "refund_entries");

            migrationBuilder.AlterColumn<string>(
                name: "swift",
                table: "refund_entries",
                type: "varchar(11)",
                nullable: false,
                defaultValue: "",
                collation: "case_insensitive",
                oldClrType: typeof(string),
                oldType: "varchar(11)",
                oldNullable: true,
                oldCollation: "case_insensitive");

            migrationBuilder.AlterColumn<string>(
                name: "iban",
                table: "refund_entries",
                type: "varchar(34)",
                nullable: false,
                defaultValue: "",
                collation: "case_insensitive",
                oldClrType: typeof(string),
                oldType: "varchar(34)",
                oldNullable: true,
                oldCollation: "case_insensitive");

            migrationBuilder.AlterColumn<string>(
                name: "bank_name",
                table: "refund_entries",
                type: "varchar(100)",
                nullable: false,
                defaultValue: "",
                collation: "case_insensitive",
                oldClrType: typeof(string),
                oldType: "varchar(100)",
                oldNullable: true,
                oldCollation: "case_insensitive");

            migrationBuilder.AlterColumn<string>(
                name: "bank_account_number",
                table: "refund_entries",
                type: "varchar(50)",
                nullable: false,
                defaultValue: "",
                collation: "case_insensitive",
                oldClrType: typeof(string),
                oldType: "varchar(50)",
                oldNullable: true,
                oldCollation: "case_insensitive");

            migrationBuilder.AlterColumn<string>(
                name: "account_name",
                table: "refund_entries",
                type: "varchar(100)",
                nullable: false,
                defaultValue: "",
                collation: "case_insensitive",
                oldClrType: typeof(string),
                oldType: "varchar(100)",
                oldNullable: true,
                oldCollation: "case_insensitive");
        }
    }
}
