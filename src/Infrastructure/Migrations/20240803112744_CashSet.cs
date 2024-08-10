using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class CashSet : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "submitter_id",
                table: "purchase_order",
                newName: "processor_id");

            migrationBuilder.RenameColumn(
                name: "submitter_id",
                table: "grant",
                newName: "processor_id");

            migrationBuilder.RenameColumn(
                name: "submitter_id",
                table: "cash_advances",
                newName: "processor_id");

            migrationBuilder.AddColumn<string>(
                name: "notes",
                table: "purchase_order",
                type: "text",
                nullable: false,
                defaultValue: "",
                collation: "case_insensitive");

            migrationBuilder.AddColumn<string>(
                name: "notes",
                table: "grant",
                type: "text",
                nullable: false,
                defaultValue: "",
                collation: "case_insensitive");

            migrationBuilder.AddColumn<string>(
                name: "notes",
                table: "cash_advances",
                type: "text",
                nullable: false,
                defaultValue: "",
                collation: "case_insensitive");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "notes",
                table: "purchase_order");

            migrationBuilder.DropColumn(
                name: "notes",
                table: "grant");

            migrationBuilder.DropColumn(
                name: "notes",
                table: "cash_advances");

            migrationBuilder.RenameColumn(
                name: "processor_id",
                table: "purchase_order",
                newName: "submitter_id");

            migrationBuilder.RenameColumn(
                name: "processor_id",
                table: "grant",
                newName: "submitter_id");

            migrationBuilder.RenameColumn(
                name: "processor_id",
                table: "cash_advances",
                newName: "submitter_id");
        }
    }
}
