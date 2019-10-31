using CM.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CM.Web.Models
{
    public class CocktailReviewViewModel
    {
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
        public ICollection<CocktailIngredient> CocktailIngredients { get; set; } 
            = new List<CocktailIngredient>();
        public bool CanReview { get; set; }
        public IDictionary<string, Tuple<string, decimal, string >> Reviews { get; set; }
            = new Dictionary<string, Tuple<string, decimal, string>>();
    }
}
