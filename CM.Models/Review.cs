using CM.Models.Abstractions;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CM.Models
{
    public class Review
    {
        public Review()
        {

        }
       
        public string Id { get; set; }

        [Range(0, 500)]
        public string Description { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        [Range(1, 5)]
        public decimal Rating { get; set; }
        public string UserId { get; set; }
        public AppUser User { get; set; }
        public string CocktailId { get; set; }
        public Cocktail Cocktail { get; set; }
        public string BarId { get; set; }
        public Bar Bar { get; set; }
        public string ReviewDate { get; set; }
    }
}