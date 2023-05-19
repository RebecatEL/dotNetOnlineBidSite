using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebProject2.Data.Migrations
{
    public partial class RolesConfiguration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ClientType",
                schema: "Identity",
                table: "Users");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ClientType",
                schema: "Identity",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
