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
            return componentDTO;
        }

    }
}
