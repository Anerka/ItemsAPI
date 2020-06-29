using Microsoft.EntityFrameworkCore.Migrations;

namespace ItemsAPI.Migrations
{
    public partial class ItemInfoDBAddRatingDescription : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Rating",
                maxLength: 200,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Rating");
        }
    }
}
