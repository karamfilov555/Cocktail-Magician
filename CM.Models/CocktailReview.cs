using CM.Models.Abstractions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CM.Models
{
    public class CocktailReview:BaseReview
    { 
        public CocktailReview()
        {

        }
        public string CocktailId { get; set; }
        public Cocktail Cocktail { get; set; }
        public ICollection<CocktailReviewLike> CocktailReviewLikes { get; set; }
    }
}