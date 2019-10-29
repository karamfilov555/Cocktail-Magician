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
        public HomeController(ICocktailServices cocktailServices)
        {
            _cocktailServices = cocktailServices;
        }
        public async Task<IActionResult> Index()
        {
            var cocktailDtos =  await _cocktailServices.GetCocktailsForHomePage();
            var cocktailsVm = cocktailDtos.Select(b => b.MapToCocktailViewModel()).ToList();
            return View(cocktailsVm);
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
