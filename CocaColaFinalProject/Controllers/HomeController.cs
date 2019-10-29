using CM.Services.Contracts;
using CM.Web.Mappers;
using CM.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace CocaColaFinalProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly ICocktailServices _cocktailServices;
        private readonly IBarServices _barServices;

        public HomeController(ICocktailServices cocktailServices, IBarServices barServices)
        {
            _cocktailServices = cocktailServices;
            _barServices = barServices;
        }
        public async Task<IActionResult> Index()
        {
            var cocktailDtos =  await _cocktailServices.GetCocktailsForHomePage();
            var cocktailsVM = cocktailDtos.Select(c => c.MapToCocktailViewModel()).ToList();
            var barDTOs = await _barServices.GetHomePageBars();
            var barsVM = barDTOs.Select(b => b.MapBarToVM()).ToList();
            var homePageVM = new HomePageViewModel(barsVM, cocktailsVM);
            return View(homePageVM);
        }

        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult About()
        {
            return View();
        }
        public IActionResult Cola()
        {
            return View();
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
