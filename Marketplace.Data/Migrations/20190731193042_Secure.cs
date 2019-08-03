using Microsoft.EntityFrameworkCore.Migrations;

namespace Marketplace.Data.Migrations
{
    public partial class Secure : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SellerPaysMiddleman",
                table: "Offers");

            migrationBuilder.AddColumn<int>(
                name: "SecureTransactionPayer",
                table: "Offers",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SecureTransactionPayer",
                table: "Offers");

            migrationBuilder.AddColumn<bool>(
                name: "SellerPaysMiddleman",
                table: "Offers",
                nullable: false,
                defaultValue: false);
        }
    }
}
