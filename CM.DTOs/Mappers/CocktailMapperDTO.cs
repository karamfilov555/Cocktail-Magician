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
        public static Cocktail MapToCocktailModel(this CocktailDto cocktail)
        {
            var cocktailModel = new Cocktail();
            // Care !!! --> vse oshte nqma Id!
            cocktailModel.Name = cocktail.Name;
            cocktailModel.Rating = cocktail.Rating;
            cocktailModel.Image = cocktail.Image;
            return cocktailModel;
        }
    }
}
