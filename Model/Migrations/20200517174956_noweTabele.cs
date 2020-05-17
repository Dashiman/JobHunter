using Microsoft.EntityFrameworkCore.Migrations;

namespace Model.Migrations
{
    public partial class noweTabele : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "JobOffer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Category",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Category", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Category",
                columns: new[] { "Id", "Description" },
                values: new object[,]
                {
                    { 1, "IT" },
                    { 2, "Budownictwo" },
                    { 3, "Gastronomia" },
                    { 4, "Ogrodnictwo" },
                    { 5, "Rolnictwo" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_JobOffer_CategoryId",
                table: "JobOffer",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_JobOffer_Category_CategoryId",
                table: "JobOffer",
                column: "CategoryId",
                principalTable: "Category",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JobOffer_Category_CategoryId",
                table: "JobOffer");

            migrationBuilder.DropTable(
                name: "Category");

            migrationBuilder.DropIndex(
                name: "IX_JobOffer_CategoryId",
                table: "JobOffer");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "JobOffer");
        }
    }
}
