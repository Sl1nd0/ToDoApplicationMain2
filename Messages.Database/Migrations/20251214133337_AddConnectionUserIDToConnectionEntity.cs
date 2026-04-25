using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Messages.Database.Migrations
{
    public partial class AddConnectionUserIDToConnectionEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ConnectionUserID",
                table: "Connections",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ConnectionUserID",
                table: "Connections");
        }
    }
}
