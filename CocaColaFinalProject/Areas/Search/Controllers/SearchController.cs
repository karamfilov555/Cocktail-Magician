using System.Threading.Tasks;
using CM.Services.Contracts;
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
            var searchDto = await _searchServices.GetResultsFromSearch(searchString);

            var searchVm = searchDto.MapToSearchVM(searchString);

            return PartialView("_SearchResultsPartial",searchVm);
        }
    }
}