using Microsoft.EntityFrameworkCore.Migrations;

namespace CM.Data.Migrations
{
    public partial class dropCocktailIngr : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CocktailIngredients_Cocktails_CocktailId",
                table: "CocktailIngredients");

            migrationBuilder.DropForeignKey(
                name: "FK_CocktailIngredients_Ingredients_IngredientId",
                table: "CocktailIngredients");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CocktailIngredients",
                table: "CocktailIngredients");

            migrationBuilder.RenameTable(
                name: "CocktailIngredients",
                newName: "CocktailIngredient");

            migrationBuilder.RenameIndex(
                name: "IX_CocktailIngredients_IngredientId",
                table: "CocktailIngredient",
                newName: "IX_CocktailIngredient_IngredientId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CocktailIngredient",
                table: "CocktailIngredient",
                columns: new[] { "CocktailId", "IngredientId" });

            migrationBuilder.AddForeignKey(
                name: "FK_CocktailIngredient_Cocktails_CocktailId",
                table: "CocktailIngredient",
                column: "CocktailId",
                principalTable: "Cocktails",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CocktailIngredient_Ingredients_IngredientId",
                table: "CocktailIngredient",
                column: "IngredientId",
                principalTable: "Ingredients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CocktailIngredient_Cocktails_CocktailId",
                table: "CocktailIngredient");

            migrationBuilder.DropForeignKey(
                name: "FK_CocktailIngredient_Ingredients_IngredientId",
                table: "CocktailIngredient");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CocktailIngredient",
                table: "CocktailIngredient");

            migrationBuilder.RenameTable(
                name: "CocktailIngredient",
                newName: "CocktailIngredients");

            migrationBuilder.RenameIndex(
                name: "IX_CocktailIngredient_IngredientId",
                table: "CocktailIngredients",
                newName: "IX_CocktailIngredients_IngredientId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CocktailIngredients",
                table: "CocktailIngredients",
                columns: new[] { "CocktailId", "IngredientId" });

            migrationBuilder.AddForeignKey(
                name: "FK_CocktailIngredients_Cocktails_CocktailId",
                table: "CocktailIngredients",
                column: "CocktailId",
                principalTable: "Cocktails",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CocktailIngredients_Ingredients_IngredientId",
                table: "CocktailIngredients",
                column: "IngredientId",
                principalTable: "Ingredients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
