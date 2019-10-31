using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using CM.Models;
using CM.Services.Contracts;
using CM.Web.Mappers;
using CM.Web.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NToastNotify;

namespace CM.Web.Areas.Cocktails.Controllers
{
    [Area("Cocktails")]
    public class CocktailsController : Controller
    {
        private readonly ICocktailServices _cocktailServices;
        private readonly IReviewServices _reviewServices;
        private readonly IIngredientServices _ingredientServices;
        private readonly IToastNotification _toast;

        //ID!!!! string id = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

        public CocktailsController(ICocktailServices cocktailServices, 
                                   IIngredientServices ingredientServices,
                                   IReviewServices reviewServices,
                                   IToastNotification toast)
        {
            _cocktailServices = cocktailServices;
            _ingredientServices = ingredientServices;
            _reviewServices = reviewServices;
            _toast = toast;
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
            //expouse-vame ctx model
            var ingr =await _ingredientServices.GetAllIngredients();
            var cocktailVM = new CocktailViewModel();
            cocktailVM.Ingredients.AddRange(ingr.Select(i => new SelectListItem(i.Name, i.Id)));
            return View(cocktailVM);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CocktailViewModel cocktailVm)
        {
            var cocktailDto = cocktailVm.MapToCocktailDto();
            await _cocktailServices.AddCocktail(cocktailDto);

            return RedirectToAction("ListCocktails");
        }
        [HttpGet]
        public async Task<IActionResult> ListCocktails(string sortOrder)
        {
            ViewData["CurrentSort"] = sortOrder; //care
            ViewData["NameSortCriteria"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["RatingSortCriteria"] = sortOrder == "rating" ? "rating_desc" : "rating";
            //ViewData["IngredientsSortCriteria"] = sortOrder == "ingredients" ? "ingredients_desc" : "ingredients";
            

            var allCocktailsDtos = await _cocktailServices.GetAllCocktails();
            var allCocktailsVms = allCocktailsDtos.Select(c => c.MapToCocktailViewModel());

            switch (sortOrder)
            {
                case "name_desc":
                    allCocktailsVms = allCocktailsVms.OrderByDescending(b => b.Name);
                    break;
                case "rating":
                    allCocktailsVms = allCocktailsVms.OrderBy(b => b.Rating);
                    break;
                case "rating_desc":
                    allCocktailsVms = allCocktailsVms.OrderByDescending(s => s.Rating);
                    break;

                default:
                    allCocktailsVms = allCocktailsVms.OrderBy(s => s.Name);
                    break;
            }

            return View(allCocktailsVms.ToList());
        }

        [HttpGet]
        public async Task<IActionResult> RateCocktail(string Id)
        {
            var cocktail = await _cocktailServices.FindCocktailById(Id);
            var reviewVm = cocktail.MapToCocktailReviewViewModel();
            string userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (await _reviewServices.CheckIfUserCanReview(userId, cocktail))
            {
                return BadRequest("You cannot rate cocktail you have already rated!");
            }

            return View(reviewVm);
        }
        [HttpPost]
        public async Task<IActionResult> RateCocktail(CocktailReviewViewModel cocktailVm)
        {
            //validations
            var cocktailDto = cocktailVm.MapToCocktailDto();
            string userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            await _reviewServices.CreateCocktailReview(userId, cocktailDto);
            return RedirectToAction("ListCocktails", "Cocktails");
        }
        [HttpGet]
        public async Task<IActionResult> Delete(string Id)
        {
            var cocktail = await _cocktailServices.FindCocktailById(Id);
            var cocktailVm = cocktail.MapToCocktailViewModel();
            //validations

            return View(cocktailVm);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(CocktailViewModel cocktailVm)
        {
             var cocktailName = await _cocktailServices.DeleteCocktial(cocktailVm.Id);
            _toast.AddSuccessToastMessage($"You successfully delete \"{cocktailName}\" cocktail!");
            return RedirectToAction("ListCocktails", "Cocktails");
        }
    }
}