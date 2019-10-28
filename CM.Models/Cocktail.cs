using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

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

        public decimal Rating { get; set; }  // can be replaced with model CocktilRating
    }
}
