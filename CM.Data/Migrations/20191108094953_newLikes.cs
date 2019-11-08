using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CM.Data.Migrations
{
    public partial class newLikes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Likes");

            migrationBuilder.CreateTable(
                name: "BarReviewLikes",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false),
                    AppUserID = table.Column<string>(nullable: true),
                    BarReviewID = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BarReviewLikes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BarReviewLikes_AspNetUsers_AppUserID",
                        column: x => x.AppUserID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BarReviewLikes_BarReviews_BarReviewID",
                        column: x => x.BarReviewID,
                        principalTable: "BarReviews",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CocktailReviewLikes",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false),
                    AppUserID = table.Column<string>(nullable: true),
                    CocktailReviewID = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CocktailReviewLikes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CocktailReviewLikes_AspNetUsers_AppUserID",
                        column: x => x.AppUserID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CocktailReviewLikes_CocktailReviews_CocktailReviewID",
                        column: x => x.CocktailReviewID,
                        principalTable: "CocktailReviews",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BarReviewLikes_AppUserID",
                table: "BarReviewLikes",
                column: "AppUserID");

            migrationBuilder.CreateIndex(
                name: "IX_BarReviewLikes_BarReviewID",
                table: "BarReviewLikes",
                column: "BarReviewID");

            migrationBuilder.CreateIndex(
                name: "IX_CocktailReviewLikes_AppUserID",
                table: "CocktailReviewLikes",
                column: "AppUserID");

            migrationBuilder.CreateIndex(
                name: "IX_CocktailReviewLikes_CocktailReviewID",
                table: "CocktailReviewLikes",
                column: "CocktailReviewID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BarReviewLikes");

            migrationBuilder.DropTable(
                name: "CocktailReviewLikes");

            migrationBuilder.CreateTable(
                name: "Likes",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    AppUserID = table.Column<string>(nullable: true),
                    BarReviewID = table.Column<string>(nullable: true),
                    CocktailReviewID = table.Column<string>(nullable: true),
                    Date = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Likes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Likes_AspNetUsers_AppUserID",
                        column: x => x.AppUserID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Likes_BarReviews_BarReviewID",
                        column: x => x.BarReviewID,
                        principalTable: "BarReviews",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Likes_CocktailReviews_CocktailReviewID",
                        column: x => x.CocktailReviewID,
                        principalTable: "CocktailReviews",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Likes_AppUserID",
                table: "Likes",
                column: "AppUserID");

            migrationBuilder.CreateIndex(
                name: "IX_Likes_BarReviewID",
                table: "Likes",
                column: "BarReviewID");

            migrationBuilder.CreateIndex(
                name: "IX_Likes_CocktailReviewID",
                table: "Likes",
                column: "CocktailReviewID");
        }
    }
}
