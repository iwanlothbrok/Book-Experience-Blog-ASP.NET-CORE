using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookExperience.Infrastrucutre.Migrations
{
    public partial class IsRecommendedAndPagesWereChange : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsRecomended",
                table: "Books",
                newName: "IsRecommended");

            migrationBuilder.AlterColumn<int>(
                name: "Pages",
                table: "Books",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<byte[]>(
                name: "BookPhoto",
                table: "Books",
                type: "varbinary(max)",
                nullable: false,
                defaultValue: new byte[0]);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BookPhoto",
                table: "Books");

            migrationBuilder.RenameColumn(
                name: "IsRecommended",
                table: "Books",
                newName: "IsRecomended");

            migrationBuilder.AlterColumn<int>(
                name: "Pages",
                table: "Books",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);
        }
    }
}
