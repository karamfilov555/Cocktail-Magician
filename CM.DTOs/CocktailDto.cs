using CM.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CM.DTOs
{
    public class CocktailDto
    {
        // some validations ?
        [Key]
        public string Id { get; set; }

        //care B-52 ;)
        [Display(Name = "Cocktail's Name")]
        [Required, RegularExpression("[A-Za-z ]+", ErrorMessage = "Cocktail's name should only contain latin letters!")]
        public string Name { get; set; }
        [Url]
        [Display(Name = "Image")]

        public string Image { get; set; }
        [Display(Name = "Cocktail's Rating")]
        public decimal Rating { get; set; }
        [Display(Name = "Type your Review Here")]
        public string Description { get; set; }
        // model?
        public ICollection<CocktailIngredient> CocktailIngredients { get; set; }
            = new List<CocktailIngredient>();
        public ICollection<CocktailReview> CocktailReviews { get; set; }
           = new List<CocktailReview>();
    }
}
