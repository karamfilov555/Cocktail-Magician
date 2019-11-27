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
       
        public string Name { get; set; }
        public string CocktailID { get; set; }
        public string UserId { get; set; }
        public DateTime ReviewDate { get; set; }
        public string UserName { get; set; }
        [Url]
        [Display(Name = "Image")]

        public string Image { get; set; }
        [Display(Name = "Cocktail's Rating")]
        [Required(ErrorMessage = "Rating is required!")]
        public double? Rating { get; set; }
        [Display(Name = "Type your Review Here")]
        [Required(ErrorMessage = "Description is required!")]
        [MinLength(5, ErrorMessage = "Description be between 5 and 500 symbols"),
            MaxLength(500, ErrorMessage = "Description must be between 5 and 500 symbols")]
        public string Description { get; set; }
        
        public int LikeCount { get; set; }
        public List<string> LikedByUsers { get; set; }
        public ICollection<CocktailComponentViewModel> Ingredients { get; set; } 
            = new List<CocktailComponentViewModel>(); 
        public bool CanReview { get; set; }
        //public List<CocktailReviewViewModel> Reviews { get; set; }
        //    = new List<CocktailReviewViewModel>();
    }
}
