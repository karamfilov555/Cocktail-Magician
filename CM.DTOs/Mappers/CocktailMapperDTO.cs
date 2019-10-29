using CM.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CM.DTOs.Mappers
{
    public static class CocktailMapperDTO
    {
        public static CocktailDto MapToCocktailDto(this Cocktail cocktail)
        {
            var cocktailDto = new CocktailDto();
            cocktailDto.Id = cocktail.Id;
            cocktailDto.Name = cocktail.Name;
            cocktailDto.Rating = cocktail.Rating;
            cocktailDto.Image = cocktail.Image;
            return cocktailDto;
        }

    }
}
