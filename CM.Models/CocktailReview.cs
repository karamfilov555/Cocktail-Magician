using CM.Models.Abstractions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CM.Models
{
    public class CocktailReview:AbstractReview
    {
        public CocktailReview()
        {

        }
        public string UserId { get; set; }
        public AppUser User { get; set; }
        public string CocktailId { get; set; }
        public Cocktail Cocktail { get; set; }
    }
}
