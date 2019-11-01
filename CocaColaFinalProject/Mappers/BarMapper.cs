using CM.DTOs;
using CM.Web.Areas.Bars.Models;
using CM.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CM.Web.Mappers
{
    public static class BarMapper
    {
        public static BarViewModel MapBarToVM(this BarDTO bar)
        {
            var newBarVM = new BarViewModel();
            newBarVM.Id = bar.Id;
            newBarVM.Name = bar.Name;
            newBarVM.Address = bar.Address;
            newBarVM.ImageURL = bar.ImageUrl;
            newBarVM.Website = bar.Website;

            return newBarVM;
        }

        public static BarDTO MapBarVMToDTO(this CreateBarVM bar)
        {
            var newBarDTO = new BarDTO();
            newBarDTO.Id = bar.Id;
            newBarDTO.Name = bar.Name;
            newBarDTO.Address = bar.Address;
            newBarDTO.ImageUrl = bar.ImageURL;
            newBarDTO.Website = bar.Website;
            newBarDTO.Cocktails = bar.AllCocktails
               .Select(i => new CocktailDto()
               {
                   Id = i.ToString()

               })
               .ToList();

            return newBarDTO;
        }
    }
}
