using CM.Web.Areas.Bars.Models;
using CM.Web.Areas.Cocktails.Models;
using System.Collections.Generic;

namespace CM.Web.Models
{
    public class HomePageViewModel
    {

        public ICollection<BarViewModel> Bars { get; set; }
        public ICollection<CocktailViewModel> Cocktails { get; set; }

        public HomePageViewModel(ICollection<BarViewModel> bars, 
            ICollection<CocktailViewModel> cocktails)
        {
            this.Bars = bars;
            this.Cocktails = cocktails;
        }
       
    }
}
