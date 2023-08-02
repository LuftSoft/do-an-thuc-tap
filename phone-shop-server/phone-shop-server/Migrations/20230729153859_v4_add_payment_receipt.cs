using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace phone_shop_server.Migrations
{
    public partial class v4_add_payment_receipt : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PaymentOnlineReceipt",
                table: "Order",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PaymentOnlineReceipt",
                table: "Order");
        }
    }
}
