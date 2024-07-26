using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class expensehead : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "expense_head");
        }
    }
}
