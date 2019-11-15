using CM.DTOs;
using CM.Web.Areas.Ingredients.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CM.Web.Mappers
{
    public static class IngredientMapper
    {
        public static IngredientDTO MapToDto(this IngredientViewModel ingredientVm)
        {
            var ingredientDto = new IngredientDTO();
            ingredientDto.Id = ingredientVm.Id;
            ingredientDto.Name = ingredientVm.Name;
            ingredientDto.Brand = ingredientVm.Brand;
            ingredientDto.Country = ingredientVm.Country;

            return ingredientDto;
        }
        public static IngredientViewModel MapToViewModel(this IngredientDTO ingredientDto)
        {
            var ingredientVm = new IngredientViewModel();
            ingredientVm.Id = ingredientDto.Id;
            ingredientVm.Name = ingredientDto.Name;
            ingredientVm.Brand = ingredientDto.Brand;
            ingredientVm.Country = ingredientDto.Country;

            return ingredientVm;
        }
    }
}
