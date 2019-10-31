using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CM.Models
{
    public class Cocktail
    {
        public Cocktail()
        {
            this.CocktailReviews = new List<CocktailReview>();
        }
        [Key]
        public string Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Url]
        public string Image { get; set; }
        public ICollection<BarCocktail> BarCocktails { get; set; }
        public List<CocktailIngredient> CocktailIngredients { get; set; }
        public ICollection<CocktailReview> CocktailReviews { get; set; }
        public decimal Rating { get; set; }
        public DateTime? DateDeleted { get; set; }
    }
}
