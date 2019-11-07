using System.Threading.Tasks;
using CM.Services.Contracts;
using CM.Web.Mappers;
using Microsoft.AspNetCore.Mvc;

namespace CM.Web.Areas.Search.Controllers
{
    [Area("Search")]
    public class SearchController : Controller
    {
        private readonly ISearchServices _searchServices;
        public SearchController(ISearchServices searchServices)
        {
            _searchServices = searchServices;
        }

        
        public async Task<IActionResult> SearchResults(string searchString)
        {
            var searchDto = await _searchServices.GetResultsFromSearch(searchString);

            var searchVm = searchDto.MapToSearchVM(searchString);

            return PartialView("_SearchResultsPartial",searchVm);
        }
    }
}