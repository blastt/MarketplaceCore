using Microsoft.EntityFrameworkCore.Migrations;

namespace Marketplace.Data.Migrations
{
    public partial class filterTextOffer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FilterTextValues_Offers_OfferId",
                table: "FilterTextValues");

            migrationBuilder.AlterColumn<int>(
                name: "OfferId",
                table: "FilterTextValues",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_FilterTextValues_Offers_OfferId",
                table: "FilterTextValues",
                column: "OfferId",
                principalTable: "Offers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FilterTextValues_Offers_OfferId",
                table: "FilterTextValues");

            migrationBuilder.AlterColumn<int>(
                name: "OfferId",
                table: "FilterTextValues",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_FilterTextValues_Offers_OfferId",
                table: "FilterTextValues",
                column: "OfferId",
                principalTable: "Offers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
