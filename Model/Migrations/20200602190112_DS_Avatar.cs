using Microsoft.EntityFrameworkCore.Migrations;

namespace Model.Migrations
{
    public partial class DS_Avatar : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Category",
                columns: new[] { "Id", "Description" },
                values: new object[] { 6, "Inne" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Category",
                keyColumn: "Id",
                keyValue: 6);
        }
    }
}
