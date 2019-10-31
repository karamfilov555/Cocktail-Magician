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
            cocktailDto.CocktailIngredients = cocktail.CocktailIngredients;
            cocktailDto.CocktailReviews = cocktail.CocktailReviews;

            return cocktailDto;
        }
        public static Cocktail MapToCocktailModel(this CocktailDto cocktailDto)
        {
            var cocktailModel = new Cocktail();
            // Care !!! --> vse oshte nqma Id!
            cocktailModel.Name = cocktailDto.Name;
            cocktailModel.Rating = cocktailDto.Rating;
            cocktailModel.Image = cocktailDto.Image;
            cocktailModel.CocktailIngredients = cocktailDto.CocktailIngredients;
            cocktailModel.CocktailReviews = cocktailDto.CocktailReviews;
            return cocktailModel;
        }
    }
}
