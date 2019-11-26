using CM.DTOs;
using CM.Services.Contracts;
using CM.Web.Areas.Reviews.Models;
using CM.Web.Mappers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NToastNotify;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CM.Web.Areas.Reviews.Controllers
{
    [Area("Reviews")]
    public class BarReviewsController : Controller
    {
        //private readonly ICocktailServices _cocktailServices;
        private readonly IBarServices _barServices;
        private readonly IReviewServices _reviewServices;
        private readonly IToastNotification _toast;

        public BarReviewsController(IBarServices barServices,
             IReviewServices reviewServices, IToastNotification toast)
        {
            _barServices = barServices;
            _reviewServices = reviewServices;
            _toast = toast;
        }


        [HttpGet]
        [Authorize(Roles = "Manager, Administrator, Member")]
        public async Task<IActionResult> BarReviews(string id, string name, double? rating, int? pageNumber )
        {
            try
            {
            List<BarReviewDTO> allReviewsDTOs = await _reviewServices.GetAllReviewsForBar(id, pageNumber);
            var allReviewsVM = allReviewsDTOs.Select(r => r.MapReviewDTOToVM()).ToList();
            if (allReviewsVM.Count == 0&&pageNumber!=null)
            {
                _toast.AddInfoToastMessage("There are no more comments!");
                //here i have to stop the request... return smthg..
            }
            if (pageNumber!=null)
            {
                return PartialView("_LoadMoreBarReviewsPartial", allReviewsVM);
            }
            var barReviewVM = new AllBarReviewsViewModel();
            barReviewVM.ReviewedByUsers = await _reviewServices.GetUsersWhoReviewedBar(id);
            barReviewVM.Reviews = allReviewsVM;
            barReviewVM.BarId = id;
            barReviewVM.BarName = name;
            barReviewVM.Rating = (double)Math.Round((decimal)(rating ?? 0), 2);
            return View(barReviewVM);

            }
            catch (Exception ex)
            {
                _toast.AddErrorToastMessage(ex.Message);
                ViewBag.ErrorTitle = "";
                return View("Error");
            }
        }


        [HttpPost]
        [Authorize(Roles = "Manager, Administrator, Member")]
        public async Task<IActionResult> CreateBarReview(BarReviewViewModel barVM)
        {
            try
            {
            var barReviewDTO = barVM.MapVMToReviewDTO();
            var newRating= (double)Math.Round((decimal)(await _reviewServices.CreateBarReview(barReviewDTO)??0), 2);

            return RedirectToAction("BarReviews", "BarReviews", new { id = barVM.BarId, name = barVM.BarName, rating=newRating});
            }
            catch (Exception ex)
            {

                _toast.AddErrorToastMessage(ex.Message);
                ViewBag.ErrorTitle = "";
                return View("Error");
            }
        }

        //TODO bez rediredt pri greshka
        [HttpPost]
        [Authorize(Roles = "Manager, Administrator, Member")]
        public async Task<int> LikeBarReview(string barReviewID, string barId)
        {
            try
            {
            int count = await _reviewServices.LikeBarReview(barReviewID, User.FindFirstValue(ClaimTypes.NameIdentifier));

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
        public async Task<int> RemoveLikeBarReview(string barReviewID, string barId)
        {
            try
            {
                int count = await _reviewServices.RemoveBarReviewLike(barReviewID, User.FindFirstValue(ClaimTypes.NameIdentifier));
                return count;

            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(ex.Message);
            }
        }
    }
}
