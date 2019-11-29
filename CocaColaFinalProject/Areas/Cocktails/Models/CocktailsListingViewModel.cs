using System.Collections.Generic;

namespace CM.Web.Areas.Cocktails.Models
{
    public class CocktailsListingViewModel
    {

        public int? PrevPage { get; set; }

        public int CurrPage { get; set; }

        public int? NextPage { get; set; }

        public int TotalPages { get; set; }

        public string SortOrder { get; set; }
        public bool MoreToLoad { get; set; }

        public IList<CocktailViewModel> FiveCocktailsList { get; set; }

    }
}
