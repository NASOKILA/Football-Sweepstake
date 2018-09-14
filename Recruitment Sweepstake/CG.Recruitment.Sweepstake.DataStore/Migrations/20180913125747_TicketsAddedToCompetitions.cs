using Microsoft.EntityFrameworkCore.Migrations;

namespace CG.Recruitment.Sweepstake.DataStore.Migrations
{
    public partial class TicketsAddedToCompetitions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_Gambler_GamblerId",
                table: "Tickets");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Tickets",
                table: "Tickets");

            migrationBuilder.RenameTable(
                name: "Tickets",
                newName: "Ticket",
                newSchema: "Sweepstake");

            migrationBuilder.RenameIndex(
                name: "IX_Tickets_GamblerId",
                schema: "Sweepstake",
                table: "Ticket",
                newName: "IX_Ticket_GamblerId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Ticket",
                schema: "Sweepstake",
                table: "Ticket",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Ticket_CompetitionId",
                schema: "Sweepstake",
                table: "Ticket",
                column: "CompetitionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Ticket_Competition_CompetitionId",
                schema: "Sweepstake",
                table: "Ticket",
                column: "CompetitionId",
                principalSchema: "Sweepstake",
                principalTable: "Competition",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Ticket_Gambler_GamblerId",
                schema: "Sweepstake",
                table: "Ticket",
                column: "GamblerId",
                principalSchema: "Sweepstake",
                principalTable: "Gambler",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ticket_Competition_CompetitionId",
                schema: "Sweepstake",
                table: "Ticket");

            migrationBuilder.DropForeignKey(
                name: "FK_Ticket_Gambler_GamblerId",
                schema: "Sweepstake",
                table: "Ticket");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Ticket",
                schema: "Sweepstake",
                table: "Ticket");

            migrationBuilder.DropIndex(
                name: "IX_Ticket_CompetitionId",
                schema: "Sweepstake",
                table: "Ticket");

            migrationBuilder.RenameTable(
                name: "Ticket",
                schema: "Sweepstake",
                newName: "Tickets");

            migrationBuilder.RenameIndex(
                name: "IX_Ticket_GamblerId",
                table: "Tickets",
                newName: "IX_Tickets_GamblerId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Tickets",
                table: "Tickets",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_Gambler_GamblerId",
                table: "Tickets",
                column: "GamblerId",
                principalSchema: "Sweepstake",
                principalTable: "Gambler",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
