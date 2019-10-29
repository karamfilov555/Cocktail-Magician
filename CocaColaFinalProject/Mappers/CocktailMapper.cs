using CM.Models;
using CM.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CM.Web.Mappers
{
    public static class MapToViewModel
    {
        public static CocktailViewModel MapToCocktailViewModel(this Cocktail cocktail)
        {
            var cocktailVm = new CocktailViewModel();
            cocktailVm.Id = cocktail.Id;
            cocktailVm.Name = cocktail.Name;
            cocktailVm.Rating = cocktail.Rating;
            cocktailVm.Image = cocktail.Image;
            return cocktailVm;
        }
    }
}
