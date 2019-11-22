using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CM.Web.Areas.Ingredients.Models;
using CM.Services.Contracts;
using CM.Web.Mappers;
using NToastNotify;
using System.Linq;

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
        public IActionResult Create()
        {
            var ingredientVm = new IngredientViewModel();
            return View(ingredientVm);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
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

        //// GET: Ingredients/Ingredients/Edit/5
        //public async Task<IActionResult> Edit(string id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var ingredient = await _context.Ingredients.FindAsync(id);
        //    if (ingredient == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(ingredient);
        //}

        //// POST: Ingredients/Ingredients/Edit/5
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(string id, [Bind("Id,Name,DateDeleted")] Ingredient ingredient)
        //{
        //    if (id != ingredient.Id)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            _context.Update(ingredient);
        //            await _context.SaveChangesAsync();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!IngredientExists(ingredient.Id))
        //            {
        //                return NotFound();
        //            }
        //            else
        //            {
        //                throw;
        //            }
        //        }
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(ingredient);
        //}

        //// GET: Ingredients/Ingredients/Delete/5
        //public async Task<IActionResult> Delete(string id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var ingredient = await _context.Ingredients
        //        .FirstOrDefaultAsync(m => m.Id == id);
        //    if (ingredient == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(ingredient);
        //}

        //// POST: Ingredients/Ingredients/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(string id)
        //{
        //    var ingredient = await _context.Ingredients.FindAsync(id);
        //    _context.Ingredients.Remove(ingredient);
        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}

        //private bool IngredientExists(string id)
        //{
        //    return _context.Ingredients.Any(e => e.Id == id);
        //}
    }
}
