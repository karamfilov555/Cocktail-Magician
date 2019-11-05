using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CM.Web.Models
{
    public class CocktailsListingViewModel
    {

        public int? PrevPage { get; set; }

        public int CurrPage { get; set; }

        public int? NextPage { get; set; }

        public int TotalPages { get; set; }

        public string SortOrder { get; set; }

        public IList<CocktailViewModel> FiveCocktailsList { get; set; }

    }
}
