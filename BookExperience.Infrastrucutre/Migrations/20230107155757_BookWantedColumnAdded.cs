using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookExperience.Infrastrucutre.Migrations
{
    public partial class BookWantedColumnAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "WantedBook",
                table: "Books",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "WantedBook",
                table: "Books");
        }
    }
}
