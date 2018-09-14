using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CG.Recruitment.Sweepstake.DataStore.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Sweepstake");

            migrationBuilder.CreateTable(
                name: "Competition",
                schema: "Sweepstake",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    EntryFee = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Competition", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Gambler",
                schema: "Sweepstake",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Gambler", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Competitor",
                schema: "Sweepstake",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CompetitionId = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Competitor", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Competitor_Competition_CompetitionId",
                        column: x => x.CompetitionId,
                        principalSchema: "Sweepstake",
                        principalTable: "Competition",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Tickets",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CompetitionId = table.Column<Guid>(nullable: false),
                    CompetitorId = table.Column<Guid>(nullable: false),
                    GamblerId = table.Column<Guid>(nullable: false),
                    BoughtAt = table.Column<DateTime>(nullable: false),
                    IsPaymentReceived = table.Column<bool>(nullable: false),
                    PaymentReceivedAt = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tickets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tickets_Gambler_GamblerId",
                        column: x => x.GamblerId,
                        principalSchema: "Sweepstake",
                        principalTable: "Gambler",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Message",
                schema: "Sweepstake",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    FromGamblerId = table.Column<Guid>(nullable: false),
                    ToGamblerId = table.Column<Guid>(nullable: false),
                    Subject = table.Column<string>(nullable: true),
                    Body = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Message", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Message_Gambler_FromGamblerId",
                        column: x => x.FromGamblerId,
                        principalSchema: "Sweepstake",
                        principalTable: "Gambler",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Message_Gambler_ToGamblerId",
                        column: x => x.ToGamblerId,
                        principalSchema: "Sweepstake",
                        principalTable: "Gambler",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_GamblerId",
                table: "Tickets",
                column: "GamblerId");

            migrationBuilder.CreateIndex(
                name: "IX_Competitor_CompetitionId",
                schema: "Sweepstake",
                table: "Competitor",
                column: "CompetitionId");

            migrationBuilder.CreateIndex(
                name: "IX_Message_FromGamblerId",
                schema: "Sweepstake",
                table: "Message",
                column: "FromGamblerId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Message_ToGamblerId",
                schema: "Sweepstake",
                table: "Message",
                column: "ToGamblerId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tickets");

            migrationBuilder.DropTable(
                name: "Competitor",
                schema: "Sweepstake");

            migrationBuilder.DropTable(
                name: "Message",
                schema: "Sweepstake");

            migrationBuilder.DropTable(
                name: "Competition",
                schema: "Sweepstake");

            migrationBuilder.DropTable(
                name: "Gambler",
                schema: "Sweepstake");
        }
    }
}
