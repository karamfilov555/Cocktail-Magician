using CM.DTOs;
using CM.Models;
using CM.Web.Areas.Cocktails.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CM.Web.Areas.Reviews.Models
{
    public class CocktailReviewViewModel
    {
        [Key]
        public string Id { get; set; }

        //care B-52 ;)
        [Display(Name = "Cocktail's Name")]
        [Required, RegularExpression("[A-Za-z ]+", ErrorMessage = "Cocktail's name should only contain latin letters!")]
        public string Name { get; set; }
        public string BarId { get; set; }
        public string UserId { get; set; }
        public DateTime ReviewDate { get; set; }
        public string UserName { get; set; }
        [Url]
        [Display(Name = "Image")]

        public string Image { get; set; }
        [Display(Name = "Cocktail's Rating")]
        public double? Rating { get; set; }
        [Display(Name = "Type your Review Here")]
        public string Description { get; set; }

        public int LikeCount { get; set; }
        public List<string> LikedByUsers { get; set; }
        public ICollection<CocktailComponentViewModel> Ingredients { get; set; } 
            = new List<CocktailComponentViewModel>(); //make view model
        public bool CanReview { get; set; }
        public List<CocktailReviewViewModel> Reviews { get; set; }
            = new List<CocktailReviewViewModel>();
    }
}
