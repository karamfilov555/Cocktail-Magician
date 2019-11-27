using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using CM.Services.Contracts;
using CM.Web.Areas.Cocktails.Models;
using CM.Web.Mappers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NToastNotify;

namespace CM.Web.Areas.Cocktails.Controllers
{
    [Area("Cocktails")]
    public class CocktailsController : Controller
    {
        private readonly ICocktailServices _cocktailServices;
        private readonly IIngredientServices _ingredientServices;
        private readonly IToastNotification _toast;
        private readonly INotificationServices _notificationServices;


        public CocktailsController(ICocktailServices cocktailServices,
                                   IIngredientServices ingredientServices,
                                   IToastNotification toast,
                                   INotificationServices notificationServices)
        {
            _cocktailServices = cocktailServices;
            _ingredientServices = ingredientServices;
            _toast = toast;
            _notificationServices = notificationServices;
        }

        [Route("cocktails/details/{id}")]
        public async Task<IActionResult> Details(string Id)
        {
            try
            {
                var cocktail = await _cocktailServices.FindCocktailById(Id);
                var vm = cocktail.MapToCocktailViewModel();
                if (vm == null)
                {
                    _toast.AddErrorToastMessage($"You are tring to see Details of a cocktail with invalid model state");
                    return StatusCode(404);
                }
                return View(vm);
            }
            catch (Exception ex)
            {
                _toast.AddErrorToastMessage(ex.Message);
                ViewBag.ErrorTitle = "";
                return View("Error");
            }
        }


        [HttpGet]
        [Authorize(Roles = "Administrator, Manager")]
        public async Task<IActionResult> Create()
        {
            try
            {
                //review this block
                var ingr = await _ingredientServices.GetAllIngredientsNames();
                var createCocktailVM = new CreateCocktailViewModel();
                createCocktailVM.IngredientsNames.Add(new SelectListItem("Choose an igredient", ""));
                createCocktailVM.IngredientsNames.AddRange(ingr.Select(i => new SelectListItem(i, i)));
                return View(createCocktailVM);
            }
            catch (Exception ex)
            {
                _toast.AddErrorToastMessage(ex.Message);
                ViewBag.ErrorTitle = "";
                return View("Error");
            }
        }

        [HttpPost]
        [Authorize(Roles = "Administrator, Manager")]
        public async Task<IActionResult> Create(CreateCocktailViewModel cocktailVm)
        {
            var ingr = await _ingredientServices.GetAllIngredientsNames();
            if (ModelState.IsValid)
            {
                //TODO -refactor!
                // validation if there is no Picture!
                try
                {
                    //var imageSizeInKb = cocktailVm.CocktailImage.Length / 1024;
                    //var type = cocktailVm.CocktailImage.ContentType;
                    var cocktailDto = cocktailVm.MapToCocktailDTO();
                    await _cocktailServices.AddCocktail(cocktailDto);
                    string id = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
                    await _notificationServices.CocktailCreateNotificationToAdminAsync(id, cocktailDto.Name);
                    _toast.AddSuccessToastMessage($"You successfully added cocktail {cocktailDto.Name}!");
                    return RedirectToAction("ListCocktails");
                }
                catch (Exception ex)
                {
                    _toast.AddErrorToastMessage(ex.Message);
                    ViewBag.ErrorTitle = "";
                    return View("Error");
                }
            }
            cocktailVm.IngredientsNames.Add(new SelectListItem("Choose an igredient", ""));
            cocktailVm.IngredientsNames.AddRange(ingr.Select(i => new SelectListItem(i, i)));
            return View(cocktailVm);
        }

        [HttpGet]
        public async Task<IActionResult> ListCocktails(string sortOrder, int? currPage, string orderByModel)
        {
            if (orderByModel == null)
            {
                ViewData["CurrentSort"] = sortOrder; //care
                ViewData["NameSortCriteria"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
                ViewData["RatingSortCriteria"] = sortOrder == "rating" ? "rating_desc" : "rating";
            }
            else
            {
                sortOrder = orderByModel;
            }
            //there is some logic for the traditional page-numbers pagination (DONT remove it for now)
            try
            {
                currPage = currPage ?? 1;
                var fiveSortedCocktailsDtos = await _cocktailServices
                                        .GetFiveSortedCocktailsAsync(sortOrder, (int)currPage);
                var totalPages = await _cocktailServices
                                        .GetPageCountForCocktials(5);
                var fiveSortedCocktailsVm = fiveSortedCocktailsDtos
                                        .Select(c => c.MapToCocktailViewModel()).ToList();
                var litingViewModel = new CocktailsListingViewModel()
                {
                    FiveCocktailsList = fiveSortedCocktailsVm,
                    CurrPage = (int)currPage,
                    TotalPages = totalPages,
                    SortOrder = sortOrder,
                    MoreToLoad = true
                };
                if (fiveSortedCocktailsDtos.Count == 0)
                {
                    _toast.AddInfoToastMessage("There are no more cocktails!");
                    litingViewModel.MoreToLoad = false;
                    //here i have to stop the request... return smthg..
                }
                if (totalPages > currPage)
                {
                    litingViewModel.NextPage = currPage + 1;
                }

                if (currPage > 1)
                {
                    litingViewModel.PrevPage = currPage - 1;
                }
                // To add timeSpan and animation ! 
                if (currPage == 1)
                {
                    return View("CocktailsGrid", litingViewModel);
                }
                return PartialView("_LoadMorePartial", litingViewModel);
                //return View(allCocktailsVms.ToList());
            }
            catch (Exception ex)
            {
                _toast.AddErrorToastMessage(ex.Message);
                ViewBag.ErrorTitle = "";
                return View("Error");
            }
        }

        [HttpGet]
        [Authorize(Roles = "Administrator, Manager")]
        public async Task<IActionResult> Delete(string Id)
        {
            try
            {
                var cocktail = await _cocktailServices.FindCocktailById(Id);
                var cocktailVm = cocktail.MapToCocktailViewModel();
                return View(cocktailVm);
            }
            catch (Exception ex)
            {
                _toast.AddErrorToastMessage(ex.Message);
                ViewBag.ErrorTitle = "";
                return View("Error");
            }
        }

        [HttpPost]
        [Authorize(Roles = "Administrator, Manager")]
        public async Task<IActionResult> Delete(CocktailViewModel cocktailVm)
        {
            try
            {
                var cocktailName = await _cocktailServices.DeleteCocktial(cocktailVm.Id);
                var id = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
                await _notificationServices.CocktailDeletedNotificationToAdminAsync(id, cocktailName);
                _toast.AddSuccessToastMessage($"You successfully delete \"{cocktailName}\" cocktail!");
                return RedirectToAction("ListCocktails", "Cocktails");

            }
            catch (Exception ex)
            {
                _toast.AddErrorToastMessage(ex.Message);
                ViewBag.ErrorTitle = "";
                return View("Error");
            }
        }

        [Authorize(Roles = "Manager, Administrator")]
        [HttpGet]
        public async Task<IActionResult> DownloadRecipe(string Id)
        {
            
            if (!await _cocktailServices.CheckIfCocktailExist(Id))
            {
                _toast.AddErrorToastMessage($"Cocktail with Id: {Id} does not exist!");
                return RedirectToAction("ListCocktails", "Cocktails");
            }
            try
            {
                var cocktailName = await _cocktailServices.GetCocktailNameById(Id);
                var cocktailRecepie = await _cocktailServices.GetCocktailRecipe(Id);
                var content = new System.IO.MemoryStream(Encoding.UTF8.GetBytes(cocktailRecepie));
                var contentType = "APPLICATION/octet-stream";
                var fileName = $"{cocktailName}.txt";
                return File(content, contentType, fileName);
            }
            catch (Exception ex)
            {
                _toast.AddErrorToastMessage(ex.Message);
                ViewBag.ErrorTitle = "";
                return View("Error");
            }
        }

        [Authorize(Roles = "Manager, Administrator")]
        public async Task<IActionResult> Edit(string id)
        {
            
            try
            {
                var allIngredients = await _ingredientServices.GetAllIngredients();
                var cocktailDto = await _cocktailServices.FindCocktailById(id);
                var cocktailVM = cocktailDto.MapToCocktailViewModel();
                var ingr = await _ingredientServices.GetAllIngredientsNames();
                cocktailVM.IngredientsNames.Add(new SelectListItem("Choose an igredient", ""));
                cocktailVM.IngredientsNames.AddRange(ingr.Select(i => new SelectListItem(i, i)));
                cocktailVM.Ingredients = new List<CocktailComponentViewModel>();
                for (int i = 0; i < 10; i++)
                {
                    cocktailVM.Ingredients.Add(new CocktailComponentViewModel());
                }
                cocktailVM.AllIngredients = allIngredients.Select(i => new SelectListItem(i.Name, i.Id)).ToList();

                return View(cocktailVM);
            }
            catch (Exception ex)
            {
                _toast.AddErrorToastMessage(ex.Message);
                ViewBag.ErrorTitle = "";
                return View("Error");
            }
        }
        [HttpPost]
        [Authorize(Roles = "Manager, Administrator")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(CocktailViewModel cocktailVm)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    //image check
                    var cocktailDto = cocktailVm.MapToCocktailDTO();
                    var oldName = await _cocktailServices.GetCocktailNameById(cocktailVm.Id);
                    var cocktailName = await _cocktailServices.Update(cocktailDto);
                    var id = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
                    await _notificationServices.CocktailEditNotificationToAdminAsync(id, oldName, cocktailName);
                    if (oldName != cocktailName)
                        _toast.AddSuccessToastMessage
                    ($"You successfully edited \"{oldName}\" cocktail - new name\"{cocktailName}\"!");

                    else
                        _toast.AddSuccessToastMessage
                            ($"You successfully edited \"{cocktailName}\" cocktail!");
                    return RedirectToAction(nameof(ListCocktails));
                }
                catch (Exception ex)
                {
                    _toast.AddErrorToastMessage(ex.Message);
                    ViewBag.ErrorTitle = "";
                    return View("Error");
                }
            }
            return View(cocktailVm);
        }
    }
}