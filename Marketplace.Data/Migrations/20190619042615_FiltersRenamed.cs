using Microsoft.EntityFrameworkCore.Migrations;

namespace Marketplace.Data.Migrations
{
    public partial class FiltersRenamed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FilterBooleanValues_FiltersBoolean_FilterBooleanId",
                table: "FilterBooleanValues");

            migrationBuilder.DropForeignKey(
                name: "FK_FilterRangeValues_FiltersRange_FilterRangeId",
                table: "FilterRangeValues");

            migrationBuilder.DropForeignKey(
                name: "FK_FilterTextValues_FiltersText_SelectedFilterTextId",
                table: "FilterTextValues");

            migrationBuilder.DropIndex(
                name: "IX_FilterTextValues_SelectedFilterTextId",
                table: "FilterTextValues");

            migrationBuilder.DropIndex(
                name: "IX_FilterRangeValues_FilterRangeId",
                table: "FilterRangeValues");

            migrationBuilder.DropIndex(
                name: "IX_FilterBooleanValues_FilterBooleanId",
                table: "FilterBooleanValues");

            migrationBuilder.DropColumn(
                name: "SelectedFilterTextId",
                table: "FilterTextValues");

            migrationBuilder.DropColumn(
                name: "FilterRangeId",
                table: "FilterRangeValues");

            migrationBuilder.DropColumn(
                name: "FilterBooleanId",
                table: "FilterBooleanValues");

            migrationBuilder.RenameColumn(
                name: "ValueId",
                table: "FiltersText",
                newName: "FilterTextValueId");

            migrationBuilder.AddColumn<int>(
                name: "FilterRangeValueId",
                table: "FiltersRange",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "FilterBooleanValueId",
                table: "FiltersBoolean",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_FiltersText_FilterTextValueId",
                table: "FiltersText",
                column: "FilterTextValueId",
                unique: true,
                filter: "[FilterTextValueId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_FiltersRange_FilterRangeValueId",
                table: "FiltersRange",
                column: "FilterRangeValueId",
                unique: true,
                filter: "[FilterRangeValueId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_FiltersBoolean_FilterBooleanValueId",
                table: "FiltersBoolean",
                column: "FilterBooleanValueId",
                unique: true,
                filter: "[FilterBooleanValueId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_FiltersBoolean_FilterBooleanValues_FilterBooleanValueId",
                table: "FiltersBoolean",
                column: "FilterBooleanValueId",
                principalTable: "FilterBooleanValues",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_FiltersRange_FilterRangeValues_FilterRangeValueId",
                table: "FiltersRange",
                column: "FilterRangeValueId",
                principalTable: "FilterRangeValues",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_FiltersText_FilterTextValues_FilterTextValueId",
                table: "FiltersText",
                column: "FilterTextValueId",
                principalTable: "FilterTextValues",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FiltersBoolean_FilterBooleanValues_FilterBooleanValueId",
                table: "FiltersBoolean");

            migrationBuilder.DropForeignKey(
                name: "FK_FiltersRange_FilterRangeValues_FilterRangeValueId",
                table: "FiltersRange");

            migrationBuilder.DropForeignKey(
                name: "FK_FiltersText_FilterTextValues_FilterTextValueId",
                table: "FiltersText");

            migrationBuilder.DropIndex(
                name: "IX_FiltersText_FilterTextValueId",
                table: "FiltersText");

            migrationBuilder.DropIndex(
                name: "IX_FiltersRange_FilterRangeValueId",
                table: "FiltersRange");

            migrationBuilder.DropIndex(
                name: "IX_FiltersBoolean_FilterBooleanValueId",
                table: "FiltersBoolean");

            migrationBuilder.DropColumn(
                name: "FilterRangeValueId",
                table: "FiltersRange");

            migrationBuilder.DropColumn(
                name: "FilterBooleanValueId",
                table: "FiltersBoolean");

            migrationBuilder.RenameColumn(
                name: "FilterTextValueId",
                table: "FiltersText",
                newName: "ValueId");

            migrationBuilder.AddColumn<int>(
                name: "SelectedFilterTextId",
                table: "FilterTextValues",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "FilterRangeId",
                table: "FilterRangeValues",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "FilterBooleanId",
                table: "FilterBooleanValues",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_FilterTextValues_SelectedFilterTextId",
                table: "FilterTextValues",
                column: "SelectedFilterTextId",
                unique: true,
                filter: "[SelectedFilterTextId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_FilterRangeValues_FilterRangeId",
                table: "FilterRangeValues",
                column: "FilterRangeId",
                unique: true,
                filter: "[FilterRangeId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_FilterBooleanValues_FilterBooleanId",
                table: "FilterBooleanValues",
                column: "FilterBooleanId",
                unique: true,
                filter: "[FilterBooleanId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_FilterBooleanValues_FiltersBoolean_FilterBooleanId",
                table: "FilterBooleanValues",
                column: "FilterBooleanId",
                principalTable: "FiltersBoolean",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_FilterRangeValues_FiltersRange_FilterRangeId",
                table: "FilterRangeValues",
                column: "FilterRangeId",
                principalTable: "FiltersRange",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_FilterTextValues_FiltersText_SelectedFilterTextId",
                table: "FilterTextValues",
                column: "SelectedFilterTextId",
                principalTable: "FiltersText",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
