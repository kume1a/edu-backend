using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EduBackend.Migrations
{
    public partial class GenreTitleToName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Title",
                table: "Genres",
                newName: "Name");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Genres",
                newName: "Title");
        }
    }
}
