using Microsoft.EntityFrameworkCore.Migrations;

namespace CM.Data.Migrations
{
    public partial class AddedCountryToAddress : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Address",
                table: "Bars");

            migrationBuilder.AddColumn<string>(
                name: "Country",
                table: "Addresses",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Country",
                table: "Addresses");

            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "Bars",
                nullable: false,
                defaultValue: "");
        }
    }
}
