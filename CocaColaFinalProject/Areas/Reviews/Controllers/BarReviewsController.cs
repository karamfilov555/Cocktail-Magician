﻿using CM.DTOs;
using CM.Services.Contracts;
using CM.Web.Areas.Bars.Models;
using CM.Web.Areas.Reviews.Models;
using CM.Web.Mappers;
using CM.Web.Models;
using Microsoft.AspNetCore.Mvc;
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

        public BarReviewsController(IBarServices barServices,
             IReviewServices reviewServices)
        {
            _barServices = barServices;
            _reviewServices = reviewServices;
        }


        [HttpGet]
        public async Task<IActionResult> BarReviews(string id, string name)
        {
            List<BarReviewDTO> allReviewsDTOs =await _reviewServices.GetAllReviewsForBar(id);
            var allReviewsVM = allReviewsDTOs.Select(r => r.MapReviewDTOToVM()).ToList();
            var barReviewVM = new AllBarReviewsViewModel();
            barReviewVM.Reviews = allReviewsVM;
            barReviewVM.BarId = id;
            barReviewVM.BarName = name;
            return View(barReviewVM);

        }

        [HttpPost]
        public async Task<IActionResult> CreateBarReview(BarReviewViewModel barVM)
        {
            var barReviewDTO = barVM.MapVMToReviewDTO();
            await _reviewServices.CreateBarReview(barReviewDTO);

            return RedirectToAction("BarReviews", "BarReviews", new { id = barVM.BarId, name = barVM.BarName });
        }

        [HttpPost]
        public async Task<IActionResult> LikeBarReview(string barReviewID, string barId, string name)
        {
            
            await _reviewServices.LikeBarReview(barReviewID, User.FindFirstValue(ClaimTypes.NameIdentifier));

            return RedirectToAction("BarReviews", "BarReviews", new { id = barId, name = name });
        }

        [HttpPost]
        public async Task<IActionResult> RemoveLikeBarReview(string barReviewID, string barId, string name)
        {

            await _reviewServices.RemoveBarReviewLike(barReviewID, User.FindFirstValue(ClaimTypes.NameIdentifier));

            return RedirectToAction("BarReviews", "BarReviews", new { id = barId, name = name });
        }
    }
}
