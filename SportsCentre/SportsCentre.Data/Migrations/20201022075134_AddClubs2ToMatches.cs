using Microsoft.EntityFrameworkCore.Migrations;

namespace SportsCentre.Data.Migrations
{
    public partial class AddClubs2ToMatches : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<int>(
                name: "FirstClubId",
                table: "Matches",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SecondClubId",
                table: "Matches",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ClubId",
                table: "Clubs",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Clubs_ClubId",
                table: "Clubs",
                column: "ClubId");

            migrationBuilder.AddForeignKey(
                name: "FK_Clubs_Clubs_ClubId",
                table: "Clubs",
                column: "ClubId",
                principalTable: "Clubs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Clubs_Clubs_ClubId",
                table: "Clubs");

            migrationBuilder.DropIndex(
                name: "IX_Clubs_ClubId",
                table: "Clubs");

            migrationBuilder.DropColumn(
                name: "FirstClubId",
                table: "Matches");

            migrationBuilder.DropColumn(
                name: "SecondClubId",
                table: "Matches");

            migrationBuilder.DropColumn(
                name: "ClubId",
                table: "Clubs");

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
    }
}
