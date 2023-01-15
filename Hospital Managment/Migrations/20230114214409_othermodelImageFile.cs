using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hospital_Managment.Migrations
{
    public partial class othermodelImageFile : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "Nurses",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "Departments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "Nurses");

            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "Departments");
        }
    }
}
