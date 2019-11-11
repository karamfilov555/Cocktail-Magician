using System;
using System.Collections.Generic;
using System.Text;

namespace CM.Models
{
    public class CocktailComponent
    {
        public string Id { get; set; }
        public string IngredientId { get; set; }
        public Ingredient Ingredient { get; set; }
        public int Quantity { get; set; }
        public Unit Unit { get; set; }
        public string CocktailId { get; set; }
        public Cocktail Cocktail { get; set; }

    }
}
