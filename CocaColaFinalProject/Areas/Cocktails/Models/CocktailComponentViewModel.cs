using CM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CM.Web.Areas.Cocktails.Models
{
    public class CocktailComponentViewModel
    {
        public string Ingredient { get; set; }
        public int Quantity { get; set; }
        public Unit Unit { get; set; }
        public string CocktailId { get; set; }
        public string IngredientId { get; set; }
    }
}
