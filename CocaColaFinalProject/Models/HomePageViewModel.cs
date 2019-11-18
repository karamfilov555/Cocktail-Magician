using CM.Web.Areas.Bars.Models;
using CM.Web.Areas.Cocktails.Models;
using System.Collections.Generic;

namespace CM.Web.Models
{
    public class HomePageViewModel
    {
        public List<string> IngredientsImgsForHP { get; set; }
        public ICollection<HomePageBarViewModel> Bars { get; set; }
        public ICollection<CocktailViewModel> Cocktails { get; set; }

        public HomePageViewModel(ICollection<HomePageBarViewModel> bars, 
            ICollection<CocktailViewModel> cocktails, List<string> ingredientsImgsForHP)
        {
            this.Bars = bars;
            this.Cocktails = cocktails;
            this.IngredientsImgsForHP = ingredientsImgsForHP;
        }
       
    }
}
