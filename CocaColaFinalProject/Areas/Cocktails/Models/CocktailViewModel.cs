using CM.DTOs;
using CM.Models;
using CM.Web.Infrastructure.Attributes;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CM.Web.Areas.Cocktails.Models
{
    public class CocktailViewModel
    {
        // some validations ?
        [Key]
        public string Id { get; set; }

        //care B-52 ;)
        [Display(Name = "Cocktail's Name")]
        [Required(ErrorMessage = "Name is required!")]
        [RegularExpression("[A-Za-z ]+", ErrorMessage = "Cocktail's name should only contain latin letters!")]
        public string Name { get; set; }
        [Url]
        [Display(Name = "Image")]
        public string Image { get; set; }
        [Display(Name = "Cocktail's Rating")]
        [Range(0,10, ErrorMessage = "Cocktail's rating should be between 0 and 10!")]
        public double? Rating { get; set; }
        [MaxImageSize(500000)]
        [AllowedImageFormat(new string[] { ".jpg", ".png", "jpeg" })]
        public IFormFile CocktailImage { set; get; }
        public List<string> IngredientsIDs { get; set; } = new List<string>();
        public List<CocktailComponentViewModel> CocktailComponents { get; set; } = new List<CocktailComponentViewModel>();
        public List<BarCocktail> BarCocktails { get; set; } = new List<BarCocktail>();
        //make view model TODO
        public DateTime? DateDeleted { get; set; }
        public string Recepie { get; set; }
        public int LikeCount { get; set; }
        public List<string> LikeByUsers { get; set; }
        public List<SelectListItem> AllIngredients { get; set; } 
                                        = new List<SelectListItem>();
        public List<SelectListItem> IngredientsNames { get; set; } = new List<SelectListItem>();
        [BindProperty]
        public List<CocktailComponentViewModel> Ingredients { get; set; }
        public List<string> AllIngredientsCocktail { get; set; } = new List<string>();
        
        public string Recipe { get; set; }
    }
}
