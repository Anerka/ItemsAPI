using Microsoft.EntityFrameworkCore.Migrations;

namespace ItemsAPI.Migrations
{
    public partial class SampleData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Items",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[] { 1, "Big hammer", "Hammer" });

            migrationBuilder.InsertData(
                table: "Items",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[] { 2, "Smaller wrench", "Wrench" });

            migrationBuilder.InsertData(
                table: "Items",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[] { 3, "Medium screwdriver", "Screwdriver" });

            migrationBuilder.InsertData(
                table: "Rating",
                columns: new[] { "Id", "Description", "ItemId", "Name" },
                values: new object[,]
                {
                    { 1, "One", 1, "Id 1, Item 1" },
                    { 2, "Two", 1, "Id 2, Item 1" },
                    { 3, "Three", 2, "Id 3, Item 2" },
                    { 4, "Four", 2, "Id 4, Item 2" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Rating",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Rating",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Rating",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Rating",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}
