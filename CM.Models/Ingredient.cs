using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CM.Models
{
    public class Ingredient
    {
        [Key]
        public string Id { get; set; }
        [Required]
        public string Name { get; set; }
        public ICollection<CocktailIngredient> CocktailIngredients { get; set; }
        public ICollection<CocktailComponent> CocktailComponents { get; set; }
        public DateTime? DateDeleted { get; set; }
    }
}