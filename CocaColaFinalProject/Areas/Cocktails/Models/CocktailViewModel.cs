using CM.Models;
using Microsoft.AspNetCore.Http;
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
        public decimal Rating { get; set; }

        public IFormFile CocktailImage { set; get; }

        public List<SelectListItem> Ingredients { get; set; } = new List<SelectListItem>();
        public List<string> IngredientsIDs { get; set; } = new List<string>();

        public List<CocktailIngredient> CocktailIngredients { get; set; } = new List<CocktailIngredient>();
        public List<BarCocktail> BarCocktails { get; set; } = new List<BarCocktail>();
        public DateTime? DateDeleted { get; set; }
    }
}
