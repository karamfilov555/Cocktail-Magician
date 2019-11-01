using CM.Models.Abstractions;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CM.Models
{
    public class CocktailReview:AbstractReview
    { 
        public CocktailReview()
        {

        }
        
        public string CocktailId { get; set; }
        public Cocktail Cocktail { get; set; }
    }
}