using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Marketplace.Data.Migrations
{
    public partial class NewFilters : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FilterItem");

            migrationBuilder.DropTable(
                name: "Filter");

            migrationBuilder.CreateTable(
                name: "FiltersBoolean",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    GameId = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FiltersBoolean", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FiltersBoolean_Games_GameId",
                        column: x => x.GameId,
                        principalTable: "Games",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FiltersRange",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    GameId = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    From = table.Column<double>(nullable: false),
                    To = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FiltersRange", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FiltersRange_Games_GameId",
                        column: x => x.GameId,
                        principalTable: "Games",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FiltersText",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    GameId = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    ValueId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FiltersText", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FiltersText_Games_GameId",
                        column: x => x.GameId,
                        principalTable: "Games",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FilterBooleanValues",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    OfferId = table.Column<int>(nullable: false),
                    Value = table.Column<bool>(nullable: false),
                    FilterBooleanId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FilterBooleanValues", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FilterBooleanValues_FiltersBoolean_FilterBooleanId",
                        column: x => x.FilterBooleanId,
                        principalTable: "FiltersBoolean",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FilterBooleanValues_Offers_OfferId",
                        column: x => x.OfferId,
                        principalTable: "Offers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FilterRangeValues",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    OfferId = table.Column<int>(nullable: false),
                    Value = table.Column<double>(nullable: false),
                    FilterRangeId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FilterRangeValues", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FilterRangeValues_FiltersRange_FilterRangeId",
                        column: x => x.FilterRangeId,
                        principalTable: "FiltersRange",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FilterRangeValues_Offers_OfferId",
                        column: x => x.OfferId,
                        principalTable: "Offers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FilterTextValues",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    OfferId = table.Column<int>(nullable: false),
                    Value = table.Column<string>(nullable: true),
                    FilterTextId = table.Column<int>(nullable: true),
                    SelectedFilterTextId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FilterTextValues", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FilterTextValues_FiltersText_FilterTextId",
                        column: x => x.FilterTextId,
                        principalTable: "FiltersText",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FilterTextValues_Offers_OfferId",
                        column: x => x.OfferId,
                        principalTable: "Offers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FilterTextValues_FiltersText_SelectedFilterTextId",
                        column: x => x.SelectedFilterTextId,
                        principalTable: "FiltersText",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FilterBooleanValues_FilterBooleanId",
                table: "FilterBooleanValues",
                column: "FilterBooleanId",
                unique: true,
                filter: "[FilterBooleanId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_FilterBooleanValues_OfferId",
                table: "FilterBooleanValues",
                column: "OfferId");

            migrationBuilder.CreateIndex(
                name: "IX_FilterRangeValues_FilterRangeId",
                table: "FilterRangeValues",
                column: "FilterRangeId",
                unique: true,
                filter: "[FilterRangeId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_FilterRangeValues_OfferId",
                table: "FilterRangeValues",
                column: "OfferId");

            migrationBuilder.CreateIndex(
                name: "IX_FiltersBoolean_GameId",
                table: "FiltersBoolean",
                column: "GameId");

            migrationBuilder.CreateIndex(
                name: "IX_FiltersRange_GameId",
                table: "FiltersRange",
                column: "GameId");

            migrationBuilder.CreateIndex(
                name: "IX_FiltersText_GameId",
                table: "FiltersText",
                column: "GameId");

            migrationBuilder.CreateIndex(
                name: "IX_FilterTextValues_FilterTextId",
                table: "FilterTextValues",
                column: "FilterTextId");

            migrationBuilder.CreateIndex(
                name: "IX_FilterTextValues_OfferId",
                table: "FilterTextValues",
                column: "OfferId");

            migrationBuilder.CreateIndex(
                name: "IX_FilterTextValues_SelectedFilterTextId",
                table: "FilterTextValues",
                column: "SelectedFilterTextId",
                unique: true,
                filter: "[SelectedFilterTextId] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FilterBooleanValues");

            migrationBuilder.DropTable(
                name: "FilterRangeValues");

            migrationBuilder.DropTable(
                name: "FilterTextValues");

            migrationBuilder.DropTable(
                name: "FiltersBoolean");

            migrationBuilder.DropTable(
                name: "FiltersRange");

            migrationBuilder.DropTable(
                name: "FiltersText");

            migrationBuilder.CreateTable(
                name: "Filter",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    GameId = table.Column<int>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Rank = table.Column<int>(nullable: false),
                    Type = table.Column<int>(nullable: false),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Filter", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Filter_Games_GameId",
                        column: x => x.GameId,
                        principalTable: "Games",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "FilterItem",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    FilterId = table.Column<int>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    OfferId = table.Column<int>(nullable: true),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FilterItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FilterItem_Filter_FilterId",
                        column: x => x.FilterId,
                        principalTable: "Filter",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FilterItem_Offers_OfferId",
                        column: x => x.OfferId,
                        principalTable: "Offers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Filter_GameId",
                table: "Filter",
                column: "GameId");

            migrationBuilder.CreateIndex(
                name: "IX_FilterItem_FilterId",
                table: "FilterItem",
                column: "FilterId");

            migrationBuilder.CreateIndex(
                name: "IX_FilterItem_OfferId",
                table: "FilterItem",
                column: "OfferId");
        }
    }
}
