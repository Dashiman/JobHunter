using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Model.Migrations
{
    public partial class joboffers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "JobOffer",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(nullable: true),
                    DeclaredCost = table.Column<decimal>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    Status = table.Column<int>(nullable: false),
                    TakenById = table.Column<int>(nullable: true),
                    AddedById = table.Column<int>(nullable: false),
                    EndOfferDate = table.Column<DateTime>(nullable: false),
                    EndedAs = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobOffer", x => x.Id);
                    table.ForeignKey(
                        name: "FK_JobOffer_Users_AddedById",
                        column: x => x.AddedById,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_JobOffer_Users_TakenById",
                        column: x => x.TakenById,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BidOffer",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(nullable: true),
                    JobOfferId = table.Column<int>(nullable: false),
                    OfferDate = table.Column<DateTime>(nullable: false),
                    Proposition = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BidOffer", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BidOffer_JobOffer_JobOfferId",
                        column: x => x.JobOfferId,
                        principalTable: "JobOffer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BidOffer_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BidOffer_JobOfferId",
                table: "BidOffer",
                column: "JobOfferId");

            migrationBuilder.CreateIndex(
                name: "IX_BidOffer_UserId",
                table: "BidOffer",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_JobOffer_AddedById",
                table: "JobOffer",
                column: "AddedById");

            migrationBuilder.CreateIndex(
                name: "IX_JobOffer_TakenById",
                table: "JobOffer",
                column: "TakenById");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BidOffer");

            migrationBuilder.DropTable(
                name: "JobOffer");
        }
    }
}
