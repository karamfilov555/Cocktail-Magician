using CM.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CM.DTOs.Mappers
{
    public static class IngredientMapperDTO
    {
        public static Ingredient MapToCtxModel(this IngredientDTO ingredientDto)
        {
            var ingredientCtx = new Ingredient();
            ingredientCtx.Id = ingredientDto.Id;
            ingredientCtx.Name = ingredientDto.Name;
            ingredientCtx.Country = ingredientDto.Country;
            ingredientCtx.Brand = ingredientDto.Brand;

            return ingredientCtx;
        }
        public static IngredientDTO MapToDtoModel(this Ingredient ingredient)
        {
            var ingredientDto = new IngredientDTO();
            ingredientDto.Id = ingredient.Id;
            ingredientDto.Name = ingredient.Name;
            ingredientDto.Country = ingredient.Country;
            ingredientDto.Brand = ingredient.Brand;

            return ingredientDto;
        }
    }
}
