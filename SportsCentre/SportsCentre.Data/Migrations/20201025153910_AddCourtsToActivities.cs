using Microsoft.EntityFrameworkCore.Migrations;

namespace SportsCentre.Data.Migrations
{
    public partial class AddCourtsToActivities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CourtId",
                table: "Trainings",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CourtId",
                table: "Matches",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Trainings_CourtId",
                table: "Trainings",
                column: "CourtId");

            migrationBuilder.CreateIndex(
                name: "IX_Matches_CourtId",
                table: "Matches",
                column: "CourtId");

            migrationBuilder.AddForeignKey(
                name: "FK_Matches_Courts_CourtId",
                table: "Matches",
                column: "CourtId",
                principalTable: "Courts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Trainings_Courts_CourtId",
                table: "Trainings",
                column: "CourtId",
                principalTable: "Courts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Matches_Courts_CourtId",
                table: "Matches");

            migrationBuilder.DropForeignKey(
                name: "FK_Trainings_Courts_CourtId",
                table: "Trainings");

            migrationBuilder.DropIndex(
                name: "IX_Trainings_CourtId",
                table: "Trainings");

            migrationBuilder.DropIndex(
                name: "IX_Matches_CourtId",
                table: "Matches");

            migrationBuilder.DropColumn(
                name: "CourtId",
                table: "Trainings");

            migrationBuilder.DropColumn(
                name: "CourtId",
                table: "Matches");
        }
    }
}
