using CM.Web.Areas.Cocktails.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CM.Web.Areas.Reviews.Models
{
    public class ListReviewViewModel
    {
        public string Id { get; set; }
        public string Description { get; set; }
        public string Name { get; set; }
        public double? Rating { get; set; }
        public string Image { get; set; }
        public bool CanReview { get; set; }
        public int CurrPage { get; set; }
        public int TotalReviewsForCocktail { get; set; }

        public int TotalPages { get; set; }

        public bool MoreToLoad { get; set; }

        public IList<CocktailReviewViewModel> ReviewsPerPageForCocktail { get; set; }
        public IList<CocktailComponentViewModel>  Ingredients { get; set; }
    }
}
