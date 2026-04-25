using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Messages.Database.Migrations
{
    public partial class AddTitleToToDos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ToDoTitle",
                table: "ToDos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ToDoTitle",
                table: "ToDos");
        }
    }
}
