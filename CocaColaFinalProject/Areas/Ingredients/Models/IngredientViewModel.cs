using CM.Web.Areas.Cocktails.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CM.Web.Areas.Ingredients.Models
{
    public class IngredientViewModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Brand { get; set; }
        public string Country { get; set; }
        public ICollection<CocktailComponentViewModel> CocktailComponentsVm { get; set; }
        public DateTime? DateDeleted { get; set; }
    }
}
