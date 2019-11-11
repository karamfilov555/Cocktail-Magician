using Microsoft.EntityFrameworkCore.Migrations;

namespace CM.Data.Migrations
{
    public partial class NewCocktailComponentTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CocktailComponent",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    IngredientId = table.Column<string>(nullable: true),
                    Quantity = table.Column<int>(nullable: false),
                    Unit = table.Column<string>(nullable: false),
                    CocktailId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CocktailComponent", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CocktailComponent_Cocktails_CocktailId",
                        column: x => x.CocktailId,
                        principalTable: "Cocktails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CocktailComponent_Ingredients_IngredientId",
                        column: x => x.IngredientId,
                        principalTable: "Ingredients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CocktailComponent_CocktailId",
                table: "CocktailComponent",
                column: "CocktailId");

            migrationBuilder.CreateIndex(
                name: "IX_CocktailComponent_IngredientId",
                table: "CocktailComponent",
                column: "IngredientId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CocktailComponent");
        }
    }
}
