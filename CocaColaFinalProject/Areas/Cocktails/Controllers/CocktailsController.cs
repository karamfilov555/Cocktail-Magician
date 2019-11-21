using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
//using CM.Services;
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
        private readonly IReviewServices _reviewServices;
        private readonly IIngredientServices _ingredientServices;
        private readonly IToastNotification _toast;
        private readonly IStreamWriterServices _streamWriter;
        private readonly INotificationServices _notificationServices;

        //ID!!!! string id = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

        public CocktailsController(ICocktailServices cocktailServices,
                                   IIngredientServices ingredientServices,
                                   IReviewServices reviewServices,
                                   IToastNotification toast,
                                   IStreamWriterServices streamWriter,
                                   INotificationServices notificationServices)
        {
            _cocktailServices = cocktailServices;
            _ingredientServices = ingredientServices;
            _reviewServices = reviewServices;
            _toast = toast;
            _streamWriter = streamWriter;
            _notificationServices = notificationServices;
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
        [Authorize(Roles = "Administrator, Manager")]
        public async Task<IActionResult> Create()
        {
            //review this block
            var ingr = await _ingredientServices.GetAllIngredientsNames();
            var createCocktailVM = new CreateCocktailViewModel();
            createCocktailVM.IngredientsNames.Add(new SelectListItem("Choose an igredient", ""));
            createCocktailVM.IngredientsNames.AddRange(ingr.Select(i => new SelectListItem(i, i)));
            createCocktailVM.Ingredients = new List<CocktailComponentViewModel>();
            for (int i = 0; i < 10; i++)
            {
                createCocktailVM.Ingredients.Add(new CocktailComponentViewModel());
            }
            return View(createCocktailVM);
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
                var imageSizeInKb = cocktailVm.CocktailImage.Length / 1024;
                var type = cocktailVm.CocktailImage.ContentType;
                var cocktailDto = cocktailVm.MapToCocktailDTO();
                await _cocktailServices.AddCocktail(cocktailDto);
                //notification to admin
                string id = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
                await _notificationServices.CocktailCreateNotificationToAdminAsync(id, cocktailDto.Name);
                _toast.AddSuccessToastMessage($"You successfully added cocktail {cocktailDto.Name}!");
                return RedirectToAction("ListCocktails");
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
                return PartialView("_CocktailsGrid", litingViewModel);
            }

            return PartialView("_LoadMorePartial", litingViewModel);
            //return View(allCocktailsVms.ToList());
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> RateCocktail(string Id)
        {
            var cocktail = await _cocktailServices.FindCocktailById(Id);
            var reviewVm = cocktail.MapToCocktailReviewViewModel();
            reviewVm.Ingredients = cocktail.Ingredients.Select(i => i.MapToCocktailComponentVM()).ToList();
            string userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

            var canUserReview = await _reviewServices.CheckIfUserCanReview(userId, cocktail);
            var cocktailReviews = await _reviewServices.GetReviewsForCocktial(cocktail.Id);

            var cocktailReviewsVm = cocktailReviews.Select(r => r.MapToViewModel()).ToList();

            reviewVm.CanReview = !canUserReview;
            reviewVm.Reviews = cocktailReviewsVm;

            return View(reviewVm);
        }
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> RateCocktail(CocktailReviewViewModel cocktailVm)
        {
            //validations
            var cocktailDto = cocktailVm.MapToCocktailDto();
            string userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var cocktailName = await _cocktailServices.GetCocktailNameById(cocktailDto.Id);

            await _reviewServices.CreateCocktailReview(userId, cocktailDto);
            _toast.AddSuccessToastMessage($"You successfully rate \"{cocktailName}\" cocktail");
            return RedirectToAction("ListCocktails", "Cocktails");
        }
        [HttpGet]
        [Authorize(Roles = "Administrator, Manager")]
        public async Task<IActionResult> Delete(string Id)
        {
            var cocktail = await _cocktailServices.FindCocktailById(Id);
            var cocktailVm = cocktail.MapToCocktailViewModel();
            //validations

            return View(cocktailVm);
        }

        [HttpPost]
        [Authorize(Roles = "Administrator, Manager")]
        public async Task<IActionResult> Delete(CocktailViewModel cocktailVm)
        {
            var cocktailName = await _cocktailServices.DeleteCocktial(cocktailVm.Id);

            var id = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            await _notificationServices.CocktailDeletedNotificationToAdminAsync(id, cocktailName);

            _toast.AddSuccessToastMessage($"You successfully delete \"{cocktailName}\" cocktail!");
            return RedirectToAction("ListCocktails", "Cocktails");
        }

        [HttpGet]
        public async Task<IActionResult> DownloadRecipe(string Id)
        {
            if (Id == null)
            {
                _toast.AddErrorToastMessage("Cocktail's Id cannot be null!");
                return RedirectToAction("ListCocktails", "Cocktails");
            }
            if (!await _cocktailServices.CheckIfCocktailExist(Id))
            {
                _toast.AddErrorToastMessage($"Cocktail with Id: {Id} does not exist!");
                return RedirectToAction("ListCocktails", "Cocktails");
            }
            // if id doesnt exist....
            //validations

            var cocktailName = await _cocktailServices.GetCocktailNameById(Id);
            var cocktailRecepie = await _cocktailServices.GetCocktailRecepie(Id);
            try
            {
                var content = new System.IO.MemoryStream(Encoding.UTF8.GetBytes(cocktailRecepie));
                var contentType = "APPLICATION/octet-stream";
                var fileName = $"{cocktailName}.txt";
                return File(content, contentType, fileName);
            }
            catch (Exception)
            {
                _toast.AddInfoToastMessage("This cocktail's recepie is a secret!");
                return RedirectToAction("ListCocktails", "Cocktails");
            }
        }

        [Authorize(Roles = "Manager, Administrator")]
        public async Task<IActionResult> Edit(string id)
        {
            //TODO refactor
            if (id == null)
            {
                return NotFound();
            }
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
        [HttpPost]
        [Authorize(Roles = "Manager, Administrator")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(CocktailViewModel cocktailVm)
        {
            if (ModelState.IsValid)
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
            return View(cocktailVm);
        }
    }
}