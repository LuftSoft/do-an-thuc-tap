using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace phone_shop_server.Migrations
{
    public partial class v3_add_payment_method : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PaymentMethod",
                table: "Order",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PaymentStatus",
                table: "Order",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddCheckConstraint(
                name: "CK_Order_PaymentMethod",
                table: "Order",
                sql: "PaymentStatus = 'UNPAID' OR PaymentStatus='PAID' OR PaymentStatus='CONFIRMING'");

            migrationBuilder.AddCheckConstraint(
                name: "CK_Order_PaymentStatus",
                table: "Order",
                sql: "PaymentMethod = 'COD' OR PaymentMethod='ONLINE'");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropCheckConstraint(
                name: "CK_Order_PaymentMethod",
                table: "Order");

            migrationBuilder.DropCheckConstraint(
                name: "CK_Order_PaymentStatus",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "PaymentMethod",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "PaymentStatus",
                table: "Order");
        }
    }
}
