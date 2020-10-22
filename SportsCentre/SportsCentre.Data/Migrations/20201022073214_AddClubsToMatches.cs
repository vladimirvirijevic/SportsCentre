using Microsoft.EntityFrameworkCore.Migrations;

namespace SportsCentre.Data.Migrations
{
    public partial class AddClubsToMatches : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ClubId",
                table: "Matches",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Matches_ClubId",
                table: "Matches",
                column: "ClubId");

            migrationBuilder.AddForeignKey(
                name: "FK_Matches_Clubs_ClubId",
                table: "Matches",
                column: "ClubId",
                principalTable: "Clubs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Matches_Clubs_ClubId",
                table: "Matches");

            migrationBuilder.DropIndex(
                name: "IX_Matches_ClubId",
                table: "Matches");

            migrationBuilder.DropColumn(
                name: "ClubId",
                table: "Matches");
        }
    }
}
