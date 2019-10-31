using Microsoft.EntityFrameworkCore.Migrations;

namespace CM.Data.Migrations
{
    public partial class noAbstractReview : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CocktailReviews_Cocktails_CocktailId",
                table: "CocktailReviews");

            migrationBuilder.DropForeignKey(
                name: "FK_CocktailReviews_AspNetUsers_UserId",
                table: "CocktailReviews");

            migrationBuilder.DropTable(
                name: "BarReviews");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CocktailReviews",
                table: "CocktailReviews");

            migrationBuilder.AlterColumn<string>(
                name: "CocktailId",
                table: "CocktailReviews",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "CocktailReviews",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AddColumn<string>(
                name: "Id",
                table: "CocktailReviews",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "BarId",
                table: "CocktailReviews",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_CocktailReviews",
                table: "CocktailReviews",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_CocktailReviews_BarId",
                table: "CocktailReviews",
                column: "BarId");

            migrationBuilder.CreateIndex(
                name: "IX_CocktailReviews_UserId",
                table: "CocktailReviews",
                column: "UserId");

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

        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropIndex(
                name: "IX_CocktailReviews_BarId",
                table: "CocktailReviews");

            migrationBuilder.DropIndex(
                name: "IX_CocktailReviews_UserId",
                table: "CocktailReviews");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "CocktailReviews");

            migrationBuilder.DropColumn(
                name: "BarId",
                table: "CocktailReviews");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "CocktailReviews",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CocktailId",
                table: "CocktailReviews",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_CocktailReviews",
                table: "CocktailReviews",
                columns: new[] { "UserId", "CocktailId" });

            migrationBuilder.CreateTable(
                name: "BarReviews",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    BarId = table.Column<string>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    Rating = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BarReviews", x => new { x.UserId, x.BarId });
                    table.ForeignKey(
                        name: "FK_BarReviews_Bars_BarId",
                        column: x => x.BarId,
                        principalTable: "Bars",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BarReviews_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BarReviews_BarId",
                table: "BarReviews",
                column: "BarId");

            migrationBuilder.AddForeignKey(
                name: "FK_CocktailReviews_Cocktails_CocktailId",
                table: "CocktailReviews",
                column: "CocktailId",
                principalTable: "Cocktails",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CocktailReviews_AspNetUsers_UserId",
                table: "CocktailReviews",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
