using CM.Models;
using CM.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CM.Services
{
    public class RecipeServices : IRecipeServices
    {
        public RecipeServices()
        {

        }
        public async Task<string> ExtractRecipe(Cocktail cocktail)
        {
            var recipeSB = new StringBuilder();
            foreach (var component in cocktail.CocktailComponents)
            {
                if (component.Ingredient != null)
                {
                    recipeSB.AppendLine(component.Ingredient.Name + " " + component.Quantity + " " + component.Unit);
                }
                else
                {
                    recipeSB.AppendLine(component.Name + " " + component.Quantity + " " + component.Unit);
                }
            }
            recipeSB.AppendLine(cocktail.Recepie);
            return recipeSB.ToString();
        }
    }
}
