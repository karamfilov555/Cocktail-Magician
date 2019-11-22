using System;
using System.Collections.Generic;
using System.Text;

namespace CM.DTOs
{
    public class CocktailSearchResultDTO
    {
        //image, name, ingredients(comma separated) and rating
        public string Id { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public string Ingredients { get; set; }
        public double? Rating { get; set; }
    }
}
