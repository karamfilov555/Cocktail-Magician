using Microsoft.EntityFrameworkCore.Migrations;

namespace CM.Data.Migrations
{
    public partial class initial2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BarReviews_BarRating_BarRatingId",
                table: "BarReviews");

            migrationBuilder.DropForeignKey(
                name: "FK_CocktailReviews_CocktailRatings_CocktailRatingId",
                table: "CocktailReviews");

            migrationBuilder.DropTable(
                name: "BarRating");

            migrationBuilder.DropTable(
                name: "CocktailRatings");

            migrationBuilder.DropIndex(
                name: "IX_BarReviews_BarRatingId",
                table: "BarReviews");

            migrationBuilder.DropColumn(
                name: "BarRatingId",
                table: "BarReviews");

            migrationBuilder.RenameColumn(
                name: "Grade",
                table: "CocktailReviews",
                newName: "Rating");

            migrationBuilder.RenameColumn(
                name: "CocktailRatingId",
                table: "CocktailReviews",
                newName: "CocktailId");

            migrationBuilder.RenameIndex(
                name: "IX_CocktailReviews_CocktailRatingId",
                table: "CocktailReviews",
                newName: "IX_CocktailReviews_CocktailId");

            migrationBuilder.RenameColumn(
                name: "Grade",
                table: "BarReviews",
                newName: "Rating");

            migrationBuilder.AddForeignKey(
                name: "FK_CocktailReviews_Cocktails_CocktailId",
                table: "CocktailReviews",
                column: "CocktailId",
                principalTable: "Cocktails",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CocktailReviews_Cocktails_CocktailId",
                table: "CocktailReviews");

            migrationBuilder.RenameColumn(
                name: "Rating",
                table: "CocktailReviews",
                newName: "Grade");

            migrationBuilder.RenameColumn(
                name: "CocktailId",
                table: "CocktailReviews",
                newName: "CocktailRatingId");

            migrationBuilder.RenameIndex(
                name: "IX_CocktailReviews_CocktailId",
                table: "CocktailReviews",
                newName: "IX_CocktailReviews_CocktailRatingId");

            migrationBuilder.RenameColumn(
                name: "Rating",
                table: "BarReviews",
                newName: "Grade");

            migrationBuilder.AddColumn<string>(
                name: "BarRatingId",
                table: "BarReviews",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "BarRating",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    BarId = table.Column<string>(nullable: true),
                    Rating = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BarRating", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BarRating_Bar_BarId",
                        column: x => x.BarId,
                        principalTable: "Bar",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CocktailRatings",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    CocktailId = table.Column<string>(nullable: true),
                    Rating = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CocktailRatings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CocktailRatings_Cocktails_CocktailId",
                        column: x => x.CocktailId,
                        principalTable: "Cocktails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BarReviews_BarRatingId",
                table: "BarReviews",
                column: "BarRatingId");

            migrationBuilder.CreateIndex(
                name: "IX_BarRating_BarId",
                table: "BarRating",
                column: "BarId");

            migrationBuilder.CreateIndex(
                name: "IX_CocktailRatings_CocktailId",
                table: "CocktailRatings",
                column: "CocktailId");

            migrationBuilder.AddForeignKey(
                name: "FK_BarReviews_BarRating_BarRatingId",
                table: "BarReviews",
                column: "BarRatingId",
                principalTable: "BarRating",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CocktailReviews_CocktailRatings_CocktailRatingId",
                table: "CocktailReviews",
                column: "CocktailRatingId",
                principalTable: "CocktailRatings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
