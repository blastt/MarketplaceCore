using Microsoft.EntityFrameworkCore.Migrations;

namespace Marketplace.Data.Migrations
{
    public partial class FilterRank2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Types",
                table: "Filter");

            migrationBuilder.AddColumn<int>(
                name: "Type",
                table: "Filter",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Type",
                table: "Filter");

            migrationBuilder.AddColumn<int>(
                name: "Types",
                table: "Filter",
                nullable: false,
                defaultValue: 0);
        }
    }
}
