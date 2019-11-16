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
            newBarVM.City = bar.City;
            newBarVM.Country = bar.Country;
            newBarVM.Details = bar.Details;
            newBarVM.ImageURL = bar.ImageUrl;
            newBarVM.Rating = bar.Rating;
            newBarVM.Website = bar.Website;
            newBarVM.Cocktails = bar.Cocktails.Select(c=>c.MapToCocktailViewModel()).ToList();

            return newBarVM;
        }

        public static CreateBarVM MapBarToCreateBarVM(this BarDTO bar)
        {
            var newBarVM = new CreateBarVM();
            newBarVM.Id = bar.Id;
            newBarVM.Name = bar.Name;
            newBarVM.City = bar.City;
            newBarVM.Country = bar.Country;
            newBarVM.Details = bar.Details;
            newBarVM.BarImage = bar.BarImage;
            newBarVM.Website = bar.Website; 
            return newBarVM;
        }

        public static HomePageBarViewModel MapToHomePageBarVM(this HomePageBarDTO bar)
        {
            var homePageBarVM = new HomePageBarViewModel();
            homePageBarVM.Id = bar.Id;
            homePageBarVM.Name = bar.Name;
            homePageBarVM.City = bar.City;
            homePageBarVM.ImageUrl = bar.ImageUrl;
            return homePageBarVM;
        }

        public static BarDTO MapBarVMToDTO(this CreateBarVM bar)
        {
            var newBarDTO = new BarDTO();
            newBarDTO.Id = bar.Id;
            newBarDTO.Name = bar.Name;
            newBarDTO.City = bar.City;
            newBarDTO.Country = bar.Country;
            newBarDTO.Details = bar.Details;
            newBarDTO.BarImage = bar.BarImage;
            newBarDTO.Website = bar.Website;
            newBarDTO.Cocktails = bar.AllCocktailsIDs
               .Select(i => new CocktailDto()
               {
                   Id = i.ToString()

               })
               .ToList();

            return newBarDTO;
        }
    }
}
