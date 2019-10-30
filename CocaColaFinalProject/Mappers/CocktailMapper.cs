using CM.DTOs;
using CM.Web.Models;

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
            return cocktailVm;
        }
        public static CocktailDto MapToCocktailDto(this CocktailViewModel cocktailVm)
        {
            var cocktailDto = new CocktailDto();
            cocktailDto.Id = cocktailVm.Id;
            cocktailDto.Name = cocktailVm.Name;
            cocktailDto.Image = cocktailVm.Image;
            cocktailDto.Rating = cocktailVm.Rating;
            // colleciton bars , kudeto se predlaga kokteila ? 
            return cocktailDto;
        }
    }
}
