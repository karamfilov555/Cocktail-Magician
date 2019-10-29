using Microsoft.EntityFrameworkCore.Migrations;

namespace CM.Data.Migrations
{
    public partial class added_website_to_bars : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BarCocktails_Bar_BarId",
                table: "BarCocktails");

            migrationBuilder.DropForeignKey(
                name: "FK_BarReviews_Bar_BarId",
                table: "BarReviews");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Bar",
                table: "Bar");

            migrationBuilder.RenameTable(
                name: "Bar",
                newName: "Bars");

            migrationBuilder.AddColumn<string>(
                name: "Website",
                table: "Bars",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Bars",
                table: "Bars",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BarCocktails_Bars_BarId",
                table: "BarCocktails",
                column: "BarId",
                principalTable: "Bars",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BarReviews_Bars_BarId",
                table: "BarReviews",
                column: "BarId",
                principalTable: "Bars",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BarCocktails_Bars_BarId",
                table: "BarCocktails");

            migrationBuilder.DropForeignKey(
                name: "FK_BarReviews_Bars_BarId",
                table: "BarReviews");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Bars",
                table: "Bars");

            migrationBuilder.DropColumn(
                name: "Website",
                table: "Bars");

            migrationBuilder.RenameTable(
                name: "Bars",
                newName: "Bar");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Bar",
                table: "Bar",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BarCocktails_Bar_BarId",
                table: "BarCocktails",
                column: "BarId",
                principalTable: "Bar",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BarReviews_Bar_BarId",
                table: "BarReviews",
                column: "BarId",
                principalTable: "Bar",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
