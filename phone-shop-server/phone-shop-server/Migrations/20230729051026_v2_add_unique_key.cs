using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace phone_shop_server.Migrations
{
    public partial class v2_add_unique_key : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_PromoteDetail_PromoteId",
                table: "PromoteDetail");

            migrationBuilder.DropIndex(
                name: "IX_OrderStatus_OrderId",
                table: "OrderStatus");

            migrationBuilder.AlterColumn<string>(
                name: "PhoneNumber",
                table: "Users",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Brand",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                table: "Users",
                column: "Email",
                unique: true,
                filter: "[Email] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Users_PhoneNumber",
                table: "Users",
                column: "PhoneNumber",
                unique: true,
                filter: "[PhoneNumber] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_PromoteDetail_PromoteId_PhoneId",
                table: "PromoteDetail",
                columns: new[] { "PromoteId", "PhoneId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_OrderStatus_OrderId_StatusId",
                table: "OrderStatus",
                columns: new[] { "OrderId", "StatusId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Brand_Name",
                table: "Brand",
                column: "Name",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Users_Email",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_PhoneNumber",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_PromoteDetail_PromoteId_PhoneId",
                table: "PromoteDetail");

            migrationBuilder.DropIndex(
                name: "IX_OrderStatus_OrderId_StatusId",
                table: "OrderStatus");

            migrationBuilder.DropIndex(
                name: "IX_Brand_Name",
                table: "Brand");

            migrationBuilder.AlterColumn<string>(
                name: "PhoneNumber",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Brand",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.CreateIndex(
                name: "IX_PromoteDetail_PromoteId",
                table: "PromoteDetail",
                column: "PromoteId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderStatus_OrderId",
                table: "OrderStatus",
                column: "OrderId");
        }
    }
}
