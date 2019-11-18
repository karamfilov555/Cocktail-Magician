using CM.Models;
using CM.Web.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CM.Web.Areas.Bars.Models
{
    public class CreateBarVM
    {
        public string Id { get; set; }

        [Required(ErrorMessage = "Name is required!")]
        [MinLength(2, ErrorMessage = "Name must be between 3 and 30 symbols"),
            MaxLength(30, ErrorMessage = "Title must be between 3 and 30 symbols")]
        public string Name { get; set; }

        [Required(ErrorMessage = "City is required!")]
        [MinLength(3, ErrorMessage = "City must be between 3 and 100 symbols"),
            MaxLength(100, ErrorMessage = "City must be between 3 and 100 symbols")]
        public string City { get; set; }
        [Required(ErrorMessage = "Country is required!")]
        
        public string Country { get; set; }
        //[Required(ErrorMessage = "Address details are required!")]
        //[MinLength(3, ErrorMessage = "Address must be between 3 and 100 symbols"),
        //    MaxLength(100, ErrorMessage = "Address must be between 3 and 100 symbols")]
        public string Details { get; set; }
        [Required(ErrorMessage = "Website is required!")]
        [Url]
        public string Website { get; set; }
        
        public IFormFile BarImage { set; get; }

        public List<SelectListItem> AllCocktails { get; set; } = new List<SelectListItem>();
        public List<SelectListItem> AllCountries { get; set; } = new List<SelectListItem>();

        [Required(ErrorMessage = "You must specify cocktails for the bar!")]
        public List<string> AllCocktailsIDs { get; set; } = new List<string>();
        public List<BarCocktail> CocktailsInBar { get; set; } = new List<BarCocktail>();//make view model


    }
}
