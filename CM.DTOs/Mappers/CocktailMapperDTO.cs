using CM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
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
            cocktailDto.CocktailIngredients = cocktail.CocktailIngredients.ToList();
            cocktailDto.CocktailReviews = cocktail.Reviews;
            cocktailDto.DateDeleted = cocktail.DateDeleted;
            cocktailDto.BarCocktails = cocktail.BarCocktails.ToList();
            return cocktailDto;
        }
        public static Cocktail MapToCocktailModel(this CocktailDto cocktailDto)
        {
            var cocktailModel = new Cocktail();
           
            // Care !!! --> vse oshte nqma Id!
            cocktailModel.Name = cocktailDto.Name;
            cocktailModel.Id = cocktailDto.Id;
            cocktailModel.Rating = cocktailDto.Rating;
            cocktailModel.Image = cocktailDto.Image;
            cocktailModel.Recepie = cocktailDto.Recepie;
            //cocktailModel.CocktailIngredients = cocktailDto.CocktailIngredients;
            cocktailModel.Reviews = cocktailDto.CocktailReviews;
            return cocktailModel;
        }
    }
}
