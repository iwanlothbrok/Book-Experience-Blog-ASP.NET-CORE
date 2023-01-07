using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookExperience.Infrastrucutre.Migrations
{
    public partial class BookIsWantedNameChanged : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "WantedBook",
                table: "Books",
                newName: "IsWantedBook");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsWantedBook",
                table: "Books",
                newName: "WantedBook");
        }
    }
}
