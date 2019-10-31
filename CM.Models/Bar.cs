using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CM.Models
{
    public class Bar
    {
        public Bar()
        {

        }
        [Key]
        public string Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Url]
        public string Image { get; set; }
        [Url]
        public string Website { get; set; }
        [Required]
        public string Address { get; set; } // can be made with google maps api
        public ICollection<BarCocktail> BarCocktails { get; set; }
        public ICollection<Review> Reviews { get; set; }
        public decimal BarRating { get; set; }
        public DateTime? DateDeleted { get; set; }
    }
}
