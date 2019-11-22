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
            _toast.AddSuccessToastMessage($"You successfully rated \"{cocktailName}\" cocktail");
            return RedirectToAction("ListCocktails", "Cocktails", new { area = "Cocktails" });
        }
    }
}
