using CM.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CM.DTOs.Mappers
{
    public static class CocktailComponentMapperDTO
    {
       
        public static CocktailComponentDTO MapToCocktailDto(this CocktailComponent cocktailComponent)
        {
            var cocktailComponentDto = new CocktailComponentDTO();
            cocktailComponentDto.Id = cocktailComponent.Id;
            cocktailComponentDto.Ingredient = cocktailComponent.Ingredient.Name;
            cocktailComponentDto.IngredientId = cocktailComponent.Ingredient.Id;
            cocktailComponentDto.CocktailId = cocktailComponent.CocktailId;
            cocktailComponentDto.Quantity = cocktailComponent.Quantity;
            cocktailComponentDto.Unit = cocktailComponent.Unit;
            return cocktailComponentDto;
        }
        public static CocktailComponent MapToCocktailModel(this CocktailComponentDTO cocktailComponentDto )
        {
            var cocktailComponentModel = new CocktailComponent();
            cocktailComponentModel.IngredientId = cocktailComponentDto.IngredientId;
            cocktailComponentModel.CocktailId = cocktailComponentDto.CocktailId;
            //cocktailComponentModel.Ingredient = cocktailComponentDto.Ingredient;
            //cocktailComponentModel.IngredientId = cocktailComponentDto.Ingredient;
            cocktailComponentModel.Quantity = cocktailComponentDto.Quantity;
            cocktailComponentModel.Unit = cocktailComponentDto.Unit;
            //var ingr = new Ingredient { Name = cocktailComponentDto.Ingredient };
            //cocktailComponentModel.Ingredient = ingr;
            return cocktailComponentModel;
        }
    }
}
