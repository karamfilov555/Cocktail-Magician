using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CM.Services.Contracts;
using CM.Web.Areas.Search.Models;
using CM.Web.Mappers;
using Microsoft.AspNetCore.Mvc;
using NToastNotify;

namespace CM.Web.Areas.Search.Controllers
{
    [Area("Search")]
    public class SearchController : Controller
    {
        private readonly ISearchServices _searchServices;
        private readonly IToastNotification _toast;
        public SearchController(ISearchServices searchServices,
            IToastNotification toast)
        {
            _searchServices = searchServices;
            _toast = toast;
        }

        
        public async Task<IActionResult> SearchResults(string searchString)
        {
            if (searchString == null)
            {
                _toast.AddInfoToastMessage("Please enter search criteria!");
                return RedirectToAction("Index","Home");
            }
            var barResults = await _searchServices.GetResultsFromBars(searchString);
            var cocktailResults =await _searchServices.GetResultsFromCocktails(searchString);

            var searchVM = new SearchViewModel();
            searchVM.Bars = barResults.Select(b => b.MapSearchBarVMToDTO()).ToList();
            searchVM.Cocktails = cocktailResults.Select(c => c.MapCocktailSearchDTOToVM()).ToList();
            searchVM.SearchCriteria = searchString;
            return PartialView("_SearchResultsPartial", searchVM);
        }


        public IActionResult BarResults(ICollection<BarSearchResultViewModel> barResults)
        {
            if (barResults is null)
            {
                throw new System.ArgumentNullException(nameof(barResults));
            }

            return PartialView("_BarResults", barResults);
        }

        public IActionResult CocktailResults(ICollection<CocktailSearchRestultViewModel> cocktailResults)
        {
            if (cocktailResults is null)
            {
                throw new System.ArgumentNullException(nameof(cocktailResults));
            }

            return PartialView("_CocktailResults", cocktailResults);
        }
    }
}