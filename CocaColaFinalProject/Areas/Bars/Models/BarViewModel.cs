using CM.Web.Areas.Cocktails.Models;
using CM.Web.Areas.Reviews.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CM.Web.Areas.Bars.Models
{
    public class BarViewModel
    {
        public string Id { get; set; }

        [Required(ErrorMessage = "Name is required!")]
        [MinLength(2, ErrorMessage = "Name must be between 3 and 30 symbols"),
            MaxLength(30, ErrorMessage = "Title must be between 3 and 30 symbols")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Address is required!")]
        [MinLength(3, ErrorMessage = "Address must be between 3 and 30 symbols"),
            MaxLength(30, ErrorMessage = "Address must be between 3 and 30 symbols")]
        public string City { get; set; }
        public string Country { get; set; }
        public string Details { get; set; }
        public double? Rating { get; set; }
        public string Website { get; set; }
        public string ImageURL { get; set; }
        public List<BarReviewViewModel> Reviews { get; set; } = new List<BarReviewViewModel>();
        public List<CocktailViewModel> Cocktails { get; set; } = new List<CocktailViewModel>();

    }
}
