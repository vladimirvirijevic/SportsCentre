using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SportsCentre.Data.Migrations
{
    public partial class AddMemebers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MemberId",
                table: "Tickets",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MemberId",
                table: "Permits",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Memebers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Firstname = table.Column<string>(nullable: true),
                    Lastname = table.Column<string>(nullable: true),
                    Birthdate = table.Column<string>(nullable: true),
                    Address = table.Column<string>(nullable: true),
                    Phone = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Memebers", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_MemberId",
                table: "Tickets",
                column: "MemberId");

            migrationBuilder.CreateIndex(
                name: "IX_Permits_MemberId",
                table: "Permits",
                column: "MemberId");

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Permits_Memebers_MemberId",
                table: "Permits");

            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_Memebers_MemberId",
                table: "Tickets");

            migrationBuilder.DropTable(
                name: "Memebers");

            migrationBuilder.DropIndex(
                name: "IX_Tickets_MemberId",
                table: "Tickets");

            migrationBuilder.DropIndex(
                name: "IX_Permits_MemberId",
                table: "Permits");

            migrationBuilder.DropColumn(
                name: "MemberId",
                table: "Tickets");

            migrationBuilder.DropColumn(
                name: "MemberId",
                table: "Permits");
        }
    }
}
