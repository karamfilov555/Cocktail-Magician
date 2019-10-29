using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CM.Models
{
    public class Cocktail
    {
        public Cocktail()
        {

        }
        [Key]
        public string Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Url]
        public string Image { get; set; }
        public ICollection<BarCocktail> BarCocktails { get; set; }
        public ICollection<CocktailIngredient> CocktailIngredients { get; set; }
        public ICollection<CocktailReview> CocktailReviews { get; set; }
        public decimal Rating { get; set; }
        public DateTime? DateDeleted { get; set; }
    }
}
