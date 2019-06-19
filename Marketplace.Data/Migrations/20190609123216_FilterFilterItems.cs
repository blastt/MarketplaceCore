using Microsoft.EntityFrameworkCore.Migrations;

namespace Marketplace.Data.Migrations
{
    public partial class FilterFilterItems : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FilterItem_Games_GameId",
                table: "FilterItem");

            migrationBuilder.RenameColumn(
                name: "GameId",
                table: "FilterItem",
                newName: "OfferId");

            migrationBuilder.RenameIndex(
                name: "IX_FilterItem_GameId",
                table: "FilterItem",
                newName: "IX_FilterItem_OfferId");

            migrationBuilder.AddColumn<int>(
                name: "GameId",
                table: "Filter",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Filter_GameId",
                table: "Filter",
                column: "GameId");

            migrationBuilder.AddForeignKey(
                name: "FK_Filter_Games_GameId",
                table: "Filter",
                column: "GameId",
                principalTable: "Games",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_FilterItem_Offers_OfferId",
                table: "FilterItem",
                column: "OfferId",
                principalTable: "Offers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Filter_Games_GameId",
                table: "Filter");

            migrationBuilder.DropForeignKey(
                name: "FK_FilterItem_Offers_OfferId",
                table: "FilterItem");

            migrationBuilder.DropIndex(
                name: "IX_Filter_GameId",
                table: "Filter");

            migrationBuilder.DropColumn(
                name: "GameId",
                table: "Filter");

            migrationBuilder.RenameColumn(
                name: "OfferId",
                table: "FilterItem",
                newName: "GameId");

            migrationBuilder.RenameIndex(
                name: "IX_FilterItem_OfferId",
                table: "FilterItem",
                newName: "IX_FilterItem_GameId");

            migrationBuilder.AddForeignKey(
                name: "FK_FilterItem_Games_GameId",
                table: "FilterItem",
                column: "GameId",
                principalTable: "Games",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
