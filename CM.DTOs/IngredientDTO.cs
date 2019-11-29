using System;
using System.Collections.Generic;
using System.Text;

namespace CM.DTOs
{
    public class IngredientDTO
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Brand { get; set; }
        public string Country { get; set; }
        public ICollection<CocktailComponentDTO> CocktailComponentsDTO { get; set; } = new List<CocktailComponentDTO>();

    }
}
