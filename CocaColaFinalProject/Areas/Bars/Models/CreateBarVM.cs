﻿using CM.Models;
using CM.Web.Models;
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

        [Required(ErrorMessage = "Address is required!")]
        [MinLength(3, ErrorMessage = "Address must be between 3 and 30 symbols"),
            MaxLength(30, ErrorMessage = "Address must be between 3 and 30 symbols")]
        public string Address { get; set; }

        [Required(ErrorMessage = "A website is required!")]
        public string Website { get; set; }

        public string ImageURL { get; set; }

        public ICollection<SelectListItem> AllCocktails { get; set; }
        public IList<BarCocktail> CocktailsInBar { get; set; }
    }
}