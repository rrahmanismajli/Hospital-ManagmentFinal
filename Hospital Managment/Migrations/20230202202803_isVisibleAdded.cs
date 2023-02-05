using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hospital_Managment.Migrations
{
    public partial class isVisibleAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "isVisible",
                table: "Doctors",
                type: "bit",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "isVisible",
                table: "Doctors");
        }
    }
}
