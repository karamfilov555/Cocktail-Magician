using System.Linq;
using CM.DTOs;
using CM.Models;
using CM.Web.Areas.Cocktails.Models;

namespace CM.Web.Mappers
{
    public static class CocktailMapper
    {
        public static CocktailViewModel MapToCocktailViewModel(this CocktailDto cocktailDto)
        {
            var cocktailVm = new CocktailViewModel();
            cocktailVm.Id = cocktailDto.Id;
            cocktailVm.Name = cocktailDto.Name;
            cocktailVm.Rating = cocktailDto.Rating;
            cocktailVm.Image = cocktailDto.Image;
            cocktailVm.DateDeleted = cocktailDto.DateDeleted;
            cocktailVm.CocktailIngredients = cocktailDto.CocktailIngredients;
            cocktailVm.BarCocktails = cocktailDto.BarCocktails;
            return cocktailVm; 

        }
        public static CocktailDto MapToCocktailDto(this CocktailViewModel cocktailVm)
        {
            var cocktailDto = new CocktailDto();
            cocktailDto.Id = cocktailVm.Id;
            cocktailDto.Name = cocktailVm.Name;
            cocktailDto.Image = cocktailVm.Image;
            cocktailDto.Rating = cocktailVm.Rating;
            //TODO
            cocktailDto.CocktailIngredients = cocktailVm.IngredientsIDs
                .Select(i=>new CocktailIngredient()
                {
                    CocktailId = cocktailVm.Id, IngredientId=i
                })
                .ToList();
            // colleciton bars , kudeto se predlaga kokteila ? 
            return cocktailDto;
        }
        public static CocktailDto MapToCocktailDto(this CocktailReviewViewModel cocktailVm)
        {
            var cocktailDto = new CocktailDto();
            cocktailDto.Id = cocktailVm.Id;
            cocktailDto.Description = cocktailVm.Description;
            cocktailDto.Rating = cocktailVm.Rating;
            return cocktailDto;
        }
        public static CocktailReviewViewModel MapToCocktailReviewViewModel(this CocktailDto cocktailDto)
        {
            var cocktailVm = new CocktailReviewViewModel();
            cocktailVm.Id = cocktailDto.Id;
            cocktailVm.Name = cocktailDto.Name;
            cocktailVm.Rating = cocktailDto.Rating;
            cocktailVm.Description = cocktailDto.Description;
            cocktailVm.Image = cocktailDto.Image;
            cocktailVm.CocktailIngredients = cocktailDto.CocktailIngredients;
            return cocktailVm;
        }
    }
}
