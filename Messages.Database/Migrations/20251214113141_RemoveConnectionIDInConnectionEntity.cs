using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Messages.Database.Migrations
{
    public partial class RemoveConnectionIDInConnectionEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ConnectionID",
                table: "Connections");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ConnectionID",
                table: "Connections",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
