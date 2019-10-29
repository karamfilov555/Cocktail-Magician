using CM.DTOs;
using CM.Web.Models;

namespace CM.Web.Mappers
{
    public static class MapToViewModel
    {
        public static CocktailViewModel MapToCocktailViewModel(this CocktailDto cocktailDto)
        {
            var cocktailVm = new CocktailViewModel();
            cocktailVm.Id = cocktailDto.Id;
            cocktailVm.Name = cocktailDto.Name;
            cocktailVm.Rating = cocktailDto.Rating;
            cocktailVm.Image = cocktailDto.Image;
            return cocktailVm;
        }
    }
}
