using System;
using System.Collections.Generic;
using System.Text;

namespace CM.Models
{
    public class CocktailReviewLike
    {
        public string Id { get; set; }
        public DateTime Date { get; set; }
        public string AppUserID { get; set; }
        public AppUser User { get; set; }
        public string CocktailReviewID { get; set; }
        public CocktailReview CocktailReview { get; set; }
    }
}
