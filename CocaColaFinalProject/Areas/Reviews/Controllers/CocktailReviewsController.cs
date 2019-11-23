using CM.Services.Contracts;
using CM.Web.Areas.Cocktails.Models;
using CM.Web.Mappers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NToastNotify;
using CM.Web.Areas.Reviews.Models;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System;

namespace CM.Web.Areas.Reviews.Controllers
{
    [Area("Reviews")]
    public class CocktailReviewsController : Controller
    {
        private readonly ICocktailServices _cocktailServices;
        private readonly IReviewServices _reviewServices;
        private readonly IToastNotification _toast;

        //ID!!!! string id = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

        public CocktailReviewsController(ICocktailServices cocktailServices,
                                   IReviewServices reviewServices,
                                   IToastNotification toast)
        {
            _cocktailServices = cocktailServices;
            _reviewServices = reviewServices;
            _toast = toast;

        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> RateCocktail(string Id , int? currPage)
        {
            var cocktailDto = await _cocktailServices.FindCocktailById(Id);
            var reviewVm = cocktailDto.MapToCocktailReviewViewModel();
            reviewVm.Ingredients = cocktailDto.Ingredients.Select(i => i.MapToCocktailComponentVM()).ToList();
            string userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);


            currPage = currPage ?? 1;

            var twoDtoReviews = await _reviewServices
                                    .GetTwoReviewsAsync(cocktailDto.Id,(int)currPage);

            var canUserReview = await _reviewServices.CheckIfUserCanReview(userId, cocktailDto);
            var cocktailReviewsVm = twoDtoReviews.Select(r => r.MapToViewModel()).ToList();

            reviewVm.CanReview = !canUserReview;
            //reviewVm.Reviews = cocktailReviewsVm;

            var totalPages = await _reviewServices
                                    .GetPageCountForCocktailReviewsAsync(2, cocktailDto.Id);

            var litingReviewViewModel = new ListReviewViewModel()
            {
                Id = reviewVm.Id,
                Rating = reviewVm.Rating,
                Name = reviewVm.Name,
                Description = reviewVm.Description,
                Ingredients = reviewVm.Ingredients.ToList(),
                Image = reviewVm.Image,
                CanReview = !canUserReview,
                ReviewsPerPageForCocktail = cocktailReviewsVm,
                CurrPage = (int)currPage,
                TotalPages = totalPages,
                MoreToLoad = true,
                TotalReviewsForCocktail = await _reviewServices
                                    .GetTotalReviewsCountForCocktailAsync(cocktailDto.Id)
            };

            if (twoDtoReviews.Count() == 0 && litingReviewViewModel.TotalReviewsForCocktail != 0)
            {
                _toast.AddInfoToastMessage("There are no more reviews for this cocktail!");
                litingReviewViewModel.MoreToLoad = false;
            }
            if (currPage == 1)
            {
                return View(litingReviewViewModel);
            }

            return PartialView("_LoadMoreReviewsPartial", litingReviewViewModel);
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
            _toast.AddSuccessToastMessage($"You successfully rated \"{cocktailName}\" cocktail");
            return RedirectToAction("ListCocktails", "Cocktails", new { area = "Cocktails" });
        }

        //TODO bez redirect pri greshka
        [HttpPost]
        [Authorize(Roles = "Manager, Administrator, Member")]
        public async Task<int> LikeCocktailReview(string cocktailReviewID, string cocktailId)
        {
            try
            {
                int count = await _reviewServices.LikeCocktailReview(cocktailReviewID, User.FindFirstValue(ClaimTypes.NameIdentifier));

                return count;

            }
            catch (Exception ex)
            {

                throw new InvalidOperationException(ex.Message);
            }
        }

        //TODO bez rediredt pri greshka
        [HttpPost]
        [Authorize(Roles = "Manager, Administrator, Member")]
        public async Task<int> RemoveLikeCocktailReview(string cocktailReviewID, string cocktailId)
        {
            try
            {
                int count = await _reviewServices.RemoveCocktailReviewLike(cocktailReviewID, User.FindFirstValue(ClaimTypes.NameIdentifier));
                return count;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(ex.Message);
            }
        }
    }
}
