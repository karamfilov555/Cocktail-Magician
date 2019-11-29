using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CM.Models
{
    public class Bar
    {
        public Bar()
        {
            this.Reviews = new List<BarReview>();
            this.BarCocktails = new List<BarCocktail>();
        }

        [Key]
        public string Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Url]
        public string Image { get; set; }
        [Url]
        public string Website { get; set; }
        public Address Address { get; set; }
        public ICollection<BarCocktail> BarCocktails { get; set; }
        public ICollection<BarReview> Reviews { get; set; }
        public double? BarRating { get; set; }
        public DateTime? DateDeleted { get; set; }
    }
}
