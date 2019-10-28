using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CM.Models
{
    public class CocktailReview
    {
        public CocktailReview()
        {

        }
        [Key]
        public string Id { get; set; }
        [Range(0,500)]
        public string Description { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        [Range(1,5)]
        public decimal Grade { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }
        public string BarId { get; set; }
        public Bar Bar { get; set; }
    }
}
