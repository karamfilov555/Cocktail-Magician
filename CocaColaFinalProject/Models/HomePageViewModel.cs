using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
