using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class POPay : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Payment_purchase_order_PurchaseOrderId",
                table: "Payment");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Payment",
                table: "Payment");

            migrationBuilder.RenameTable(
                name: "Payment",
                newName: "Payments");

            migrationBuilder.RenameIndex(
                name: "IX_Payment_PurchaseOrderId",
                table: "Payments",
                newName: "IX_Payments_PurchaseOrderId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Payments",
                table: "Payments",
                column: "PaymentId");

            migrationBuilder.CreateIndex(
                name: "IX_purchase_order_requisition_id",
                table: "purchase_order",
                column: "requisition_id");

            migrationBuilder.AddForeignKey(
                name: "FK_Payments_purchase_order_PurchaseOrderId",
                table: "Payments",
                column: "PurchaseOrderId",
                principalTable: "purchase_order",
                principalColumn: "purchase_order_id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_purchase_order_requisitions_requisition_id",
                table: "purchase_order",
                column: "requisition_id",
                principalTable: "requisitions",
                principalColumn: "requisition_id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Payments_purchase_order_PurchaseOrderId",
                table: "Payments");

            migrationBuilder.DropForeignKey(
                name: "FK_purchase_order_requisitions_requisition_id",
                table: "purchase_order");

            migrationBuilder.DropIndex(
                name: "IX_purchase_order_requisition_id",
                table: "purchase_order");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Payments",
                table: "Payments");

            migrationBuilder.RenameTable(
                name: "Payments",
                newName: "Payment");

            migrationBuilder.RenameIndex(
                name: "IX_Payments_PurchaseOrderId",
                table: "Payment",
                newName: "IX_Payment_PurchaseOrderId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Payment",
                table: "Payment",
                column: "PaymentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Payment_purchase_order_PurchaseOrderId",
                table: "Payment",
                column: "PurchaseOrderId",
                principalTable: "purchase_order",
                principalColumn: "purchase_order_id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
