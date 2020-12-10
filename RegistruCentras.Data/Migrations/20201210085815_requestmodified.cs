using Microsoft.EntityFrameworkCore.Migrations;

namespace RegistruCentras.Data.Migrations
{
    public partial class requestmodified : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Responses_RequestForInfoID",
                table: "Responses");

            migrationBuilder.CreateIndex(
                name: "IX_Responses_RequestForInfoID",
                table: "Responses",
                column: "RequestForInfoID",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Responses_RequestForInfoID",
                table: "Responses");

            migrationBuilder.CreateIndex(
                name: "IX_Responses_RequestForInfoID",
                table: "Responses",
                column: "RequestForInfoID");
        }
    }
}
