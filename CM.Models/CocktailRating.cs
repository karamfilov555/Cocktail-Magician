using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CM.Models
{
    public class CocktailRating
    {
        public CocktailRating()
        {

        }
        public string Id { get; set; }
        public ICollection<CocktailReview> CocktailReviews { get; set; }
        public string CocktailId { get; set; }
        public Cocktail Cocktail { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Rating { get; set; }
    }
}
