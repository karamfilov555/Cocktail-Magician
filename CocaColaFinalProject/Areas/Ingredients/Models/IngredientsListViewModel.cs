using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CM.Web.Areas.Ingredients.Models
{
    public class IngredientsListViewModel
    {

        public int CurrPage { get; set; }

        public int TotalPages { get; set; }

        public bool MoreToLoad { get; set; }

        public IList<IngredientViewModel> TenIngredientsList { get; set; }
    }
}
