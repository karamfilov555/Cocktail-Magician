using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CM.Services.Contracts;
using CM.Web.Mappers;
using CM.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace CM.Web.Areas.Cocktails.Controllers
{
    [Area("Cocktails")]
    public class CocktailsController : Controller
    {
        private readonly ICocktailServices _cocktailServices;
        public CocktailsController(ICocktailServices cocktailServices)
        {
            _cocktailServices = cocktailServices;
        }

        [Route("cocktails/details/{id}")]

        public async Task<IActionResult> Details(string Id)
        {
            if (Id == null)
            {
                ViewBag.ErrorTitle = $"You are tring to see Details of a cocktail with Id = null";
                ViewBag.ErrorMessage = $"Cocktail's Id cannot be null!";
                return View("Error");
            }

            var cocktail = await _cocktailServices.FindCocktailById(Id);
            if (cocktail == null)
            {
                ViewBag.ErrorTitle = $"You are tring to see Details of a cocktail with invalid model state";
                return View("Error");
            }

            var vm = cocktail.MapToCocktailViewModel();

            if (vm == null)
            {
                ViewBag.ErrorTitle = $"You are tring to see Details of a cocktail with invalid model state";
                return View("Error");
            }
            return View(vm);
        }
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CocktailViewModel cocktailVm)
        {
            var cocktailDto = cocktailVm.MapToCocktailDto();
            await _cocktailServices.AddCocktail(cocktailDto);

            return View();
        }
        [HttpGet]
        public async Task<IActionResult> ListCocktails()
        {
            var allCocktailsDtos = await _cocktailServices.GetAllCocktails();
            var allCocktailsVms = allCocktailsDtos.Select(c => c.MapToCocktailViewModel()).ToList();
            return View(allCocktailsVms);
        }
    }
}