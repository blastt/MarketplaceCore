using Microsoft.EntityFrameworkCore.Migrations;

namespace Marketplace.Data.Migrations
{
    public partial class Selectedvaluefilter : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Value",
                table: "FiltersText",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Value",
                table: "FiltersRange",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Value",
                table: "FiltersBoolean",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Value",
                table: "FiltersText");

            migrationBuilder.DropColumn(
                name: "Value",
                table: "FiltersRange");

            migrationBuilder.DropColumn(
                name: "Value",
                table: "FiltersBoolean");
        }
    }
}
