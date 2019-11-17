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
            cocktailDto.Ingredients = cocktail.CocktailComponents
                            .Select(c=>c.MapToCocktailDto()).ToList();
            cocktailDto.CocktailReviews = cocktail.Reviews; //mapToDto
            cocktailDto.DateDeleted = cocktail.DateDeleted;
            cocktailDto.BarCocktails = cocktail.BarCocktails.ToList(); // map to 
            return cocktailDto;
        }
        public static Cocktail MapToCocktailModel(this CocktailDto cocktailDto)
        {
            var cocktailModel = new Cocktail();
            //cocktailModel.CocktailComponents = cocktailDto.Ingredients.Select(c => new CocktailComponent() {Name = c.Ingredient , Quantity = c.Quantity, Unit = c.Unit, IngredientId = _ingredientService}).ToList();
            // Care !!! --> vse oshte nqma Id!
            cocktailModel.Name = cocktailDto.Name;
            cocktailModel.Id = cocktailDto.Id;
            cocktailModel.Rating = cocktailDto.Rating;
            cocktailModel.Image = cocktailDto.Image;
            //cocktailModel.Recepie = cocktailDto.Recepie;
            //cocktailModel.CocktailComponents = new List<CocktailComponent>();
            //cocktailModel.CocktailIngredients = cocktailDto.CocktailIngredients;
            cocktailModel.Reviews = cocktailDto.CocktailReviews;
            return cocktailModel;
        }
        public static Cocktail MapToEditModel(this CocktailDto cocktailDto)
        {
            var cocktailModel = new Cocktail();
            cocktailModel.CocktailComponents = cocktailDto.Ingredients.Select(c => new CocktailComponent() {Name = c.Ingredient , Quantity = c.Quantity, Unit = c.Unit}).ToList();
            // Care !!! --> vse oshte nqma Id!
            cocktailModel.Name = cocktailDto.Name;
            cocktailModel.Id = cocktailDto.Id;
            cocktailModel.Rating = cocktailDto.Rating;
            cocktailModel.Image = cocktailDto.Image;
            cocktailModel.Recepie = cocktailDto.Recipe;
            //cocktailModel.CocktailComponents = new List<CocktailComponent>();
            //cocktailModel.CocktailIngredients = cocktailDto.CocktailIngredients;
            cocktailModel.Reviews = cocktailDto.CocktailReviews;
            return cocktailModel;
        }
        public static CocktailComponent MapComponentsToModel(this CocktailComponentDTO cocktailDto)
        {
            var component = new CocktailComponent();
            component.IngredientId = cocktailDto.IngredientId;
            component.CocktailId = cocktailDto.CocktailId;
            component.Cocktail = cocktailDto.Cocktail;
            component.Unit = cocktailDto.Unit;
            //component.Ingredient= cocktailDto.Ingredient;
            component.Quantity = cocktailDto.Quantity;
            component.Id = cocktailDto.Id;
            return component;

        }
    }
}
