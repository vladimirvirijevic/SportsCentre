using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SportsCentre.Data.Migrations
{
    public partial class AddGuests : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_Members_MemberId",
                table: "Tickets");

            migrationBuilder.RenameColumn(
                name: "MemberId",
                table: "Tickets",
                newName: "GuestId");

            migrationBuilder.RenameIndex(
                name: "IX_Tickets_MemberId",
                table: "Tickets",
                newName: "IX_Tickets_GuestId");

            migrationBuilder.CreateTable(
                name: "Guests",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Firstname = table.Column<string>(nullable: true),
                    Lastname = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Guests", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_Guests_GuestId",
                table: "Tickets",
                column: "GuestId",
                principalTable: "Guests",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_Guests_GuestId",
                table: "Tickets");

            migrationBuilder.DropTable(
                name: "Guests");

            migrationBuilder.RenameColumn(
                name: "GuestId",
                table: "Tickets",
                newName: "MemberId");

            migrationBuilder.RenameIndex(
                name: "IX_Tickets_GuestId",
                table: "Tickets",
                newName: "IX_Tickets_MemberId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_Members_MemberId",
                table: "Tickets",
                column: "MemberId",
                principalTable: "Members",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
