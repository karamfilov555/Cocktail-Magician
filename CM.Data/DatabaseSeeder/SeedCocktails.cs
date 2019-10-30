using CM.Data.JsonManager;
using CM.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CM.Data.DatabaseSeeder
{
    public static class SeedCocktails
    {
        private const string cocktailsDirectory = @"..\CM.Data\JsonRaw\Cocktails.json";
        private const string ingredientsDirectory = @"..\CM.Data\JsonRaw\Ingredients.json";
        private const string cocktailIngredientsDirectory = @"..\CM.Data\JsonRaw\CocktailIngredients.json";

        public static void SeedDatabaseCocktails(this IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                var _context = serviceScope.ServiceProvider.GetService<CMContext>();
                var _jsonManager = serviceScope.ServiceProvider.GetService<IJsonManager>();
                var count = _context.Cocktails.Count();
                if (count == 0)
                {
                    _context.Database.Migrate();

                    var cocktails = _jsonManager.ExtractTypesFromJson<Cocktail>(cocktailsDirectory);
                    var ingredients = _jsonManager.ExtractTypesFromJson<Ingredient>(ingredientsDirectory);

                    var cocktailIngredients = _jsonManager
                                    .ExtractTypesFromJson<CocktailIngredient>(cocktailIngredientsDirectory);

                    _context.Ingredients.AddRange(ingredients);
                    _context.Cocktails.AddRange(cocktails);
                    _context.CocktailIngredients.AddRange(cocktailIngredients);
                    _context.SaveChanges();
                }
            }
        }
    }
}
