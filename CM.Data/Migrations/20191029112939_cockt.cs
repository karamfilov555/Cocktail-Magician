using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CM.Data.Migrations
{
    public partial class cockt : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CocktailReviews_Bar_BarId",
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
                name: "BarId",
                table: "CocktailReviews");

            migrationBuilder.AddColumn<DateTime>(
                name: "DateDeleted",
                table: "Ingredients",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateDeleted",
                table: "Cocktails",
                nullable: true);

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

            migrationBuilder.AddColumn<DateTime>(
                name: "DateDeleted",
                table: "Bar",
                nullable: true);

            migrationBuilder.AddUniqueConstraint(
                name: "AK_CocktailReviews_Id",
                table: "CocktailReviews",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CocktailReviews",
                table: "CocktailReviews",
                columns: new[] { "UserId", "CocktailId" });

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CocktailReviews_Cocktails_CocktailId",
                table: "CocktailReviews");

            migrationBuilder.DropForeignKey(
                name: "FK_CocktailReviews_AspNetUsers_UserId",
                table: "CocktailReviews");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_CocktailReviews_Id",
                table: "CocktailReviews");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CocktailReviews",
                table: "CocktailReviews");

            migrationBuilder.DropColumn(
                name: "DateDeleted",
                table: "Ingredients");

            migrationBuilder.DropColumn(
                name: "DateDeleted",
                table: "Cocktails");

            migrationBuilder.DropColumn(
                name: "DateDeleted",
                table: "Bar");

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
                name: "FK_CocktailReviews_Bar_BarId",
                table: "CocktailReviews",
                column: "BarId",
                principalTable: "Bar",
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
