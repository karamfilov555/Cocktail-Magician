using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CM.Web.Areas.Cocktails.Models
{
    public class CreateCocktailViewModel
    {

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
        public string Recipe { get; set; }

        public IFormFile CocktailImage { set; get; }
        [BindProperty]
        public List<CocktailComponentViewModel> Ingredients { get; set; }
        public List<SelectListItem> IngredientsNames { get; set; } = new List<SelectListItem>();


        public CreateCocktailViewModel()
        {
            
        }
    }
}
