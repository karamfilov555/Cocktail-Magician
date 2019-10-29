using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CM.Web.Models
{
    public class CocktailViewModel
    {
        // some validations ?
        [Key]
        public string Id { get; set; }

        //care B-52 ;)
        [Display(Name = "Cocktail's Title")]
        [Required, RegularExpression("[A-Za-z ]+", ErrorMessage = "Cocktail's name should only contain latin letters!")]
        public string Name { get; set; }
        [Url]
        [Display(Name = "Image")]
        
        public string Image { get; set; }
        [Display(Name = "Cocktail's Rating")]
        public decimal Rating { get; set; }

    }
}
