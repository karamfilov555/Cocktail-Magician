using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CM.Models
{
    public class Ingredient
    {
        [Key]
        public string Id { get; set; }
        public string Name { get; set; }
        public string Brand { get;set; }
        public string Country { get; set; }
        public ICollection<CocktailComponent> CocktailComponents { get; set; }
        public DateTime? DateDeleted { get; set; }
    }
}