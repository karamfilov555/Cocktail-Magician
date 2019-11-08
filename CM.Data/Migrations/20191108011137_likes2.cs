using Microsoft.EntityFrameworkCore.Migrations;

namespace CM.Data.Migrations
{
    public partial class likes2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Likes_AspNetUsers_AppUserId",
                table: "Likes");

            migrationBuilder.DropForeignKey(
                name: "FK_Likes_BarReviews_BarReviewId",
                table: "Likes");

            migrationBuilder.DropForeignKey(
                name: "FK_Likes_CocktailReviews_CocktailReviewId",
                table: "Likes");

            migrationBuilder.RenameColumn(
                name: "CocktailReviewId",
                table: "Likes",
                newName: "CocktailReviewID");

            migrationBuilder.RenameColumn(
                name: "BarReviewId",
                table: "Likes",
                newName: "BarReviewID");

            migrationBuilder.RenameColumn(
                name: "AppUserId",
                table: "Likes",
                newName: "AppUserID");

            migrationBuilder.RenameIndex(
                name: "IX_Likes_CocktailReviewId",
                table: "Likes",
                newName: "IX_Likes_CocktailReviewID");

            migrationBuilder.RenameIndex(
                name: "IX_Likes_BarReviewId",
                table: "Likes",
                newName: "IX_Likes_BarReviewID");

            migrationBuilder.RenameIndex(
                name: "IX_Likes_AppUserId",
                table: "Likes",
                newName: "IX_Likes_AppUserID");

            migrationBuilder.AddForeignKey(
                name: "FK_Likes_AspNetUsers_AppUserID",
                table: "Likes",
                column: "AppUserID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Likes_BarReviews_BarReviewID",
                table: "Likes",
                column: "BarReviewID",
                principalTable: "BarReviews",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Likes_CocktailReviews_CocktailReviewID",
                table: "Likes",
                column: "CocktailReviewID",
                principalTable: "CocktailReviews",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Likes_AspNetUsers_AppUserID",
                table: "Likes");

            migrationBuilder.DropForeignKey(
                name: "FK_Likes_BarReviews_BarReviewID",
                table: "Likes");

            migrationBuilder.DropForeignKey(
                name: "FK_Likes_CocktailReviews_CocktailReviewID",
                table: "Likes");

            migrationBuilder.RenameColumn(
                name: "CocktailReviewID",
                table: "Likes",
                newName: "CocktailReviewId");

            migrationBuilder.RenameColumn(
                name: "BarReviewID",
                table: "Likes",
                newName: "BarReviewId");

            migrationBuilder.RenameColumn(
                name: "AppUserID",
                table: "Likes",
                newName: "AppUserId");

            migrationBuilder.RenameIndex(
                name: "IX_Likes_CocktailReviewID",
                table: "Likes",
                newName: "IX_Likes_CocktailReviewId");

            migrationBuilder.RenameIndex(
                name: "IX_Likes_BarReviewID",
                table: "Likes",
                newName: "IX_Likes_BarReviewId");

            migrationBuilder.RenameIndex(
                name: "IX_Likes_AppUserID",
                table: "Likes",
                newName: "IX_Likes_AppUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Likes_AspNetUsers_AppUserId",
                table: "Likes",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Likes_BarReviews_BarReviewId",
                table: "Likes",
                column: "BarReviewId",
                principalTable: "BarReviews",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Likes_CocktailReviews_CocktailReviewId",
                table: "Likes",
                column: "CocktailReviewId",
                principalTable: "CocktailReviews",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
