using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Marketplace.Data.Migrations
{
    public partial class ManyToMany : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FilterTextValues_Offers_OfferId",
                table: "FilterTextValues");

            migrationBuilder.DropIndex(
                name: "IX_FilterTextValues_OfferId",
                table: "FilterTextValues");

            migrationBuilder.DropColumn(
                name: "OfferId",
                table: "FilterTextValues");

            migrationBuilder.CreateTable(
                name: "OfferFilterTextValue",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    OfferId = table.Column<int>(nullable: false),
                    FilterTextValueId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OfferFilterTextValue", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OfferFilterTextValue_FilterTextValues_FilterTextValueId",
                        column: x => x.FilterTextValueId,
                        principalTable: "FilterTextValues",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OfferFilterTextValue_Offers_OfferId",
                        column: x => x.OfferId,
                        principalTable: "Offers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OfferFilterTextValue_FilterTextValueId",
                table: "OfferFilterTextValue",
                column: "FilterTextValueId");

            migrationBuilder.CreateIndex(
                name: "IX_OfferFilterTextValue_OfferId",
                table: "OfferFilterTextValue",
                column: "OfferId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OfferFilterTextValue");

            migrationBuilder.AddColumn<int>(
                name: "OfferId",
                table: "FilterTextValues",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_FilterTextValues_OfferId",
                table: "FilterTextValues",
                column: "OfferId");

            migrationBuilder.AddForeignKey(
                name: "FK_FilterTextValues_Offers_OfferId",
                table: "FilterTextValues",
                column: "OfferId",
                principalTable: "Offers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
