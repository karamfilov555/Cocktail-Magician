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


        public async Task<IActionResult> SearchResults(string searchString, int pageIndex, string entity)
        {
            if (searchString == null)
            {
                _toast.AddInfoToastMessage("Please enter search criteria!");
                return RedirectToAction("Index", "Home");
            }
            try
            {
                var searchVM = new SearchViewModel();
                searchVM.SearchCriteria = searchString;
                if (entity == "bars")
                {
                    var barResults = await _searchServices.GetThreeBarResultsSortedByName(searchString, pageIndex);
                    searchVM.Bars = barResults.Select(b => b.MapSearchBarVMToDTO()).ToList();
                    if (barResults.Count==0)
                    {
                    _toast.AddInfoToastMessage("No more results");
                    }
                    return PartialView("_SearchBarsResult", searchVM);
                }
                else if (entity == "cocktails")
                {
                    var cocktailResults = await _searchServices.GetThreeResultsFromCocktails(searchString, pageIndex);
                    searchVM.Cocktails = cocktailResults.Select(c => c.MapCocktailSearchDTOToVM()).ToList();
                    if (cocktailResults.Count == 0)
                    {
                        _toast.AddInfoToastMessage("No more results");
                    }
                    return PartialView("_SearchCocktailsResults", searchVM);
                }
                else
                {
                    var barResults = await _searchServices.GetThreeBarResultsSortedByName(searchString, pageIndex);
                    var cocktailResults = await _searchServices.GetThreeResultsFromCocktails(searchString, pageIndex);
                    searchVM.Bars = barResults.Select(b => b.MapSearchBarVMToDTO()).ToList();
                    searchVM.Cocktails = cocktailResults.Select(c => c.MapCocktailSearchDTOToVM()).ToList();
                    return View(searchVM);
                }
            }
            catch (System.Exception ex)
            {
                _toast.AddErrorToastMessage(ex.Message);
                ViewBag.ErrorTitle = "";
                return View("Error");
            }
        }
    }
}