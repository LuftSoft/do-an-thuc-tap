using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace phone_shop_server.Migrations
{
    public partial class v5_update_phone : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ScreenResolution",
                table: "Phone",
                type: "nvarchar(100)",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AlterColumn<string>(
                name: "ROM",
                table: "Phone",
                type: "nvarchar(100)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "RAM",
                table: "Phone",
                type: "nvarchar(100)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "PIN",
                table: "Phone",
                type: "nvarchar(100)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Operation",
                table: "Phone",
                type: "nvarchar(200)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Phone",
                type: "nvarchar(1000)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "FrontCamera",
                table: "Phone",
                type: "nvarchar(200)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "CPU",
                table: "Phone",
                type: "nvarchar(100)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "BehindCamera",
                table: "Phone",
                type: "nvarchar(200)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "Slug",
                table: "Phone",
                type: "nvarchar(1000)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Phone_Slug",
                table: "Phone",
                column: "Slug",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Phone_Slug",
                table: "Phone");

            migrationBuilder.DropColumn(
                name: "Slug",
                table: "Phone");

            migrationBuilder.AlterColumn<double>(
                name: "ScreenResolution",
                table: "Phone",
                type: "float",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)");

            migrationBuilder.AlterColumn<string>(
                name: "ROM",
                table: "Phone",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)");

            migrationBuilder.AlterColumn<string>(
                name: "RAM",
                table: "Phone",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)");

            migrationBuilder.AlterColumn<string>(
                name: "PIN",
                table: "Phone",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)");

            migrationBuilder.AlterColumn<string>(
                name: "Operation",
                table: "Phone",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Phone",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(1000)");

            migrationBuilder.AlterColumn<string>(
                name: "FrontCamera",
                table: "Phone",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)");

            migrationBuilder.AlterColumn<string>(
                name: "CPU",
                table: "Phone",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)");

            migrationBuilder.AlterColumn<string>(
                name: "BehindCamera",
                table: "Phone",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)");
        }
    }
}
