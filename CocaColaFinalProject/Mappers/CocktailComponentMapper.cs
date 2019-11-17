using CM.DTOs;
using CM.Web.Areas.Cocktails.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CM.Web.Mappers
{
    public static class CocktailComponentMapper
    {
        public static CocktailComponentDTO MapCockTailComponentVMToDTo(this CocktailComponentViewModel componentViewModel)
        {
            var componentDTO = new CocktailComponentDTO();
            componentDTO.Quantity = componentViewModel.Quantity;
            componentDTO.Unit = componentViewModel.Unit;
            componentDTO.Ingredient = componentViewModel.Ingredient;
            componentDTO.CocktailId = componentViewModel.CocktailId;
            componentDTO.IngredientId = componentViewModel.IngredientId;
            return componentDTO;
        }
        public static CocktailComponentViewModel MapToCocktailComponentVM(this CocktailComponentDTO componentDto)
        {
            var componentVm = new CocktailComponentViewModel();
            componentVm.Quantity = componentDto.Quantity;
            componentVm.Unit = componentDto.Unit;
            componentVm.Ingredient = componentDto.Ingredient;
            componentVm.CocktailId = componentDto.CocktailId;
            componentVm.IngredientId = componentDto.IngredientId;
            return componentVm;
        }
    }
}
