using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hospital_Managment.Migrations
{
    public partial class notification : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AppointmentiId",
                table: "Doctors",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Appointmentis",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PatientName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Appointmentis", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Appointmentis_ApplicationUser_UserId",
                        column: x => x.UserId,
                        principalTable: "ApplicationUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Doctors_AppointmentiId",
                table: "Doctors",
                column: "AppointmentiId");

            migrationBuilder.CreateIndex(
                name: "IX_Appointmentis_UserId",
                table: "Appointmentis",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Doctors_Appointmentis_AppointmentiId",
                table: "Doctors",
                column: "AppointmentiId",
                principalTable: "Appointmentis",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Doctors_Appointmentis_AppointmentiId",
                table: "Doctors");

            migrationBuilder.DropTable(
                name: "Appointmentis");

            migrationBuilder.DropIndex(
                name: "IX_Doctors_AppointmentiId",
                table: "Doctors");

            migrationBuilder.DropColumn(
                name: "AppointmentiId",
                table: "Doctors");
        }
    }
}
