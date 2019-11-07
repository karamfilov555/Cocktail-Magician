using CM.DTOs;
using CM.Web.Areas.Bars.Models;
using CM.Web.Areas.Cocktails.Models;
using CM.Web.Areas.Search.Models;
using CM.Web.Models;
using System.Collections.Generic;
using System.Linq;

namespace CM.Web.Mappers
{
    public static class SearchMapper
    {

        public static SearchViewModel MapToSearchVM(this SearchResultDTO searchDto , string searchCriteria)
        {
            var searchVm = new SearchViewModel();
            ICollection<BarViewModel> barsVm = new List<BarViewModel>();
            ICollection<CocktailViewModel> cocktailVm = new List<CocktailViewModel>();

            if (searchDto.Bars.Count != 0)
            {
                 barsVm = searchDto.Bars.Select(b => b.MapBarToVM()).ToList();
            }
            if (searchDto.Cocktails.Count != 0)
            {
                 cocktailVm = searchDto.Cocktails.Select(b => b.MapToCocktailViewModel()).ToList();
            }

            searchVm.Bars = barsVm;
            searchVm.Cocktails = cocktailVm;
            searchVm.SearchCriteria = searchCriteria;
            return searchVm;
        }
       
    }
}
