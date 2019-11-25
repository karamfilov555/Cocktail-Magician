using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CM.Web.Areas.Ingredients.Models;
using CM.Services.Contracts;
using CM.Web.Mappers;
using NToastNotify;
using System.Linq;
using System;
using Microsoft.AspNetCore.Authorization;

namespace CM.Web.Areas.Ingredients.Controllers
{
    [Area("Ingredients")]
    public class IngredientsController : Controller
    {
        private readonly IIngredientServices _ingredientServices;
        private readonly IToastNotification _toast;

        public IngredientsController(IIngredientServices ingredientServices, IToastNotification toast)
        {
            _ingredientServices = ingredientServices;
            _toast = toast;
        }
        [Authorize(Roles = "Manager, Administrator")]
        public async Task<IActionResult> Index(int? currPage)
        {
            currPage = currPage ?? 1;

            var tenIngredientsDtos = await _ingredientServices
                                    .GetTenIngredientsAsync((int)currPage);

            var totalPages = await _ingredientServices
                                    .GetPageCountForIngredientsAsync(10);

            var tenIngredientsVm = tenIngredientsDtos
                                    .Select(c => c.MapToViewModel()).ToList();

            var litingViewModel = new IngredientsListViewModel()
            {
                TenIngredientsList = tenIngredientsVm,
                CurrPage = (int)currPage,
                TotalPages = totalPages,
                MoreToLoad = true
            };

            if (tenIngredientsDtos.Count == 0)
            {
                _toast.AddInfoToastMessage("There are no more ingredients!");
                litingViewModel.MoreToLoad = false;
            }
            if (currPage == 1)
            {
                return View(litingViewModel);
            }

            return PartialView("_LoadMorePartial", litingViewModel);
        }

        [HttpGet]
        [Authorize(Roles = "Manager, Administrator")]
        public IActionResult Create()
        {
            var ingredientVm = new IngredientViewModel();
            return View(ingredientVm);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Manager, Administrator")]
        public async Task<IActionResult> Create(IngredientViewModel ingredientVm)
        {
            if (ModelState.IsValid)
            {
                var ingredientDto = ingredientVm.MapToDto();

                await _ingredientServices.AddIngredient(ingredientDto);

                _toast.AddSuccessToastMessage($"You successfully added a new ingredient : {ingredientDto.Name} !");
                return RedirectToAction(nameof(Index));
            }
            _toast.AddErrorToastMessage($"Invalid Model state!");
            return View(ingredientVm);
        }

        // GET: Ingredients/Ingredients/Edit/5
        [Authorize(Roles = "Manager, Administrator")]
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ingredient = await _ingredientServices.GetIngredientById(id);
            var ingredientVM = ingredient.MapToViewModel();
            return View(ingredientVM);
        }

        // POST: Ingredients/Ingredients/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Manager, Administrator")]
        public async Task<IActionResult> Edit(IngredientViewModel ingredientVM)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var name =await _ingredientServices.EditIngredienAsync(ingredientVM.MapToDto());
                    _toast.AddErrorToastMessage($"Ingredient {name} was updated!");
                }
                catch (Exception ex)
                {
                    return StatusCode(500);
                }
            }
            return RedirectToAction(nameof(Index));
        }

        // GET: Ingredients/Ingredients/Delete/5
        [HttpGet]
        [Authorize(Roles = "Manager, Administrator")]
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var ingredient = await _ingredientServices.GetIngredientById(id);
            var ingredientVM = ingredient.MapToViewModel();
            return View(ingredientVM);
        }

        // POST: Ingredients/Ingredients/Delete/5
        [HttpPost]
        [Authorize(Roles = "Manager, Administrator")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var name = await _ingredientServices.DeleteIngredientAsync(id);
                    _toast.AddInfoToastMessage($"Ingredient {name} was deleted!");
                }
                catch (Exception ex)
                {
                    return StatusCode(500);
                }
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
