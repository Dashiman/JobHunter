using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Model.Migrations
{
    public partial class recomendations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Recomendation",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Content = table.Column<string>(nullable: true),
                    ForUserId = table.Column<int>(nullable: true),
                    AddedById = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Recomendation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Recomendation_Users_AddedById",
                        column: x => x.AddedById,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Recomendation_Users_ForUserId",
                        column: x => x.ForUserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TakenOffer",
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
                    FinishDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TakenOffer", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TakenOffer_Users_AddedById",
                        column: x => x.AddedById,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TakenOffer_Users_TakenById",
                        column: x => x.TakenById,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Recomendation_AddedById",
                table: "Recomendation",
                column: "AddedById");

            migrationBuilder.CreateIndex(
                name: "IX_Recomendation_ForUserId",
                table: "Recomendation",
                column: "ForUserId");

            migrationBuilder.CreateIndex(
                name: "IX_TakenOffer_AddedById",
                table: "TakenOffer",
                column: "AddedById");

            migrationBuilder.CreateIndex(
                name: "IX_TakenOffer_TakenById",
                table: "TakenOffer",
                column: "TakenById");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Recomendation");

            migrationBuilder.DropTable(
                name: "TakenOffer");
        }
    }
}
