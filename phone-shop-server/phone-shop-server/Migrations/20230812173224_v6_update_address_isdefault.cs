using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace phone_shop_server.Migrations
{
    public partial class v6_update_address_isdefault : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "isDefault",
                table: "Address",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "isDefault",
                table: "Address");
        }
    }
}
