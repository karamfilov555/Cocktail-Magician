using CM.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CM.DTOs
{
    public class CocktailComponentDTO
    {
        public string Id { get; set; }
        public string IngredientId { get; set; }
        public string Ingredient { get; set; }
        public int Quantity { get; set; }
        public Unit Unit { get; set; }
        public string CocktailId { get; set; }
        public Cocktail Cocktail { get; set; }
    }
}
