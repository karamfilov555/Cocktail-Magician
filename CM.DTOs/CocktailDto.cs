using CM.Models;
using Microsoft.AspNetCore.Http;
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
        public string CocktailId { get; set; }
        public string IngredientId { get; set; }

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

        public IFormFile CocktailImage { set; get; }
        // model?
        public List<CocktailComponentDTO> Ingredients { get; set; }
            = new List<CocktailComponentDTO>();
        //public List<CocktailIngredient> CocktailIngredients { get; set; }
        //    = new List<CocktailIngredient>();
        public List<BarCocktail> BarCocktails { get; set; }
           = new List<BarCocktail>(); // todo
        public ICollection<CocktailReview> CocktailReviews { get; set; }
           = new List<CocktailReview>(); //todo
        public DateTime? DateDeleted { get; set; }
        public string Recipe { get; set; }
        public int LikeCount { get; set; }
        public List<string> LikedByUsers { get; set; }
    }
}
