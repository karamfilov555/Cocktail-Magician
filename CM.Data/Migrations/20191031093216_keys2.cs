using Microsoft.EntityFrameworkCore.Migrations;

namespace CM.Data.Migrations
{
    public partial class keys2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CocktailReviews_Bars_BarId",
                table: "CocktailReviews");

            migrationBuilder.DropForeignKey(
                name: "FK_CocktailReviews_Cocktails_CocktailId",
                table: "CocktailReviews");

            migrationBuilder.DropForeignKey(
                name: "FK_CocktailReviews_AspNetUsers_UserId",
                table: "CocktailReviews");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CocktailReviews",
                table: "CocktailReviews");

            migrationBuilder.RenameTable(
                name: "CocktailReviews",
                newName: "Reviews");

            migrationBuilder.RenameIndex(
                name: "IX_CocktailReviews_UserId",
                table: "Reviews",
                newName: "IX_Reviews_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_CocktailReviews_CocktailId",
                table: "Reviews",
                newName: "IX_Reviews_CocktailId");

            migrationBuilder.RenameIndex(
                name: "IX_CocktailReviews_BarId",
                table: "Reviews",
                newName: "IX_Reviews_BarId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Reviews",
                table: "Reviews",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_Bars_BarId",
                table: "Reviews",
                column: "BarId",
                principalTable: "Bars",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_Cocktails_CocktailId",
                table: "Reviews",
                column: "CocktailId",
                principalTable: "Cocktails",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_AspNetUsers_UserId",
                table: "Reviews",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_Bars_BarId",
                table: "Reviews");

            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_Cocktails_CocktailId",
                table: "Reviews");

            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_AspNetUsers_UserId",
                table: "Reviews");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Reviews",
                table: "Reviews");

            migrationBuilder.RenameTable(
                name: "Reviews",
                newName: "CocktailReviews");

            migrationBuilder.RenameIndex(
                name: "IX_Reviews_UserId",
                table: "CocktailReviews",
                newName: "IX_CocktailReviews_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Reviews_CocktailId",
                table: "CocktailReviews",
                newName: "IX_CocktailReviews_CocktailId");

            migrationBuilder.RenameIndex(
                name: "IX_Reviews_BarId",
                table: "CocktailReviews",
                newName: "IX_CocktailReviews_BarId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CocktailReviews",
                table: "CocktailReviews",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CocktailReviews_Bars_BarId",
                table: "CocktailReviews",
                column: "BarId",
                principalTable: "Bars",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CocktailReviews_Cocktails_CocktailId",
                table: "CocktailReviews",
                column: "CocktailId",
                principalTable: "Cocktails",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CocktailReviews_AspNetUsers_UserId",
                table: "CocktailReviews",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
