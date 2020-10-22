using Microsoft.EntityFrameworkCore.Migrations;

namespace SportsCentre.Data.Migrations
{
    public partial class ChangeMembersTableName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Permits_Memebers_MemberId",
                table: "Permits");

            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_Memebers_MemberId",
                table: "Tickets");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Memebers",
                table: "Memebers");

            migrationBuilder.RenameTable(
                name: "Memebers",
                newName: "Members");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Members",
                table: "Members",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Permits_Members_MemberId",
                table: "Permits",
                column: "MemberId",
                principalTable: "Members",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_Members_MemberId",
                table: "Tickets",
                column: "MemberId",
                principalTable: "Members",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Permits_Members_MemberId",
                table: "Permits");

            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_Members_MemberId",
                table: "Tickets");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Members",
                table: "Members");

            migrationBuilder.RenameTable(
                name: "Members",
                newName: "Memebers");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Memebers",
                table: "Memebers",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Permits_Memebers_MemberId",
                table: "Permits",
                column: "MemberId",
                principalTable: "Memebers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_Memebers_MemberId",
                table: "Tickets",
                column: "MemberId",
                principalTable: "Memebers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
