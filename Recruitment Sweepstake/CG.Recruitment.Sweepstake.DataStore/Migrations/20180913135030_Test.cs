using Microsoft.EntityFrameworkCore.Migrations;

namespace CG.Recruitment.Sweepstake.DataStore.Migrations
{
    public partial class Test : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Ticket_CompetitorId",
                schema: "Sweepstake",
                table: "Ticket",
                column: "CompetitorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Ticket_Competitor_CompetitorId",
                schema: "Sweepstake",
                table: "Ticket",
                column: "CompetitorId",
                principalSchema: "Sweepstake",
                principalTable: "Competitor",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ticket_Competitor_CompetitorId",
                schema: "Sweepstake",
                table: "Ticket");

            migrationBuilder.DropIndex(
                name: "IX_Ticket_CompetitorId",
                schema: "Sweepstake",
                table: "Ticket");
        }
    }
}
