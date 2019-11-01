using CM.Services.Contracts;
using CM.Web.Mappers;
using CM.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CM.Web.Controllers
{
    public class ReviewsController:Controller
    {
        private readonly ICocktailServices _cocktailServices;
        private readonly IBarServices _barServices;
        private readonly IReviewServices _reviewServices;

        public ReviewsController(IBarServices barServices, 
            ICocktailServices cocktailServices, IReviewServices reviewServices)
        {
            _cocktailServices = cocktailServices;
            _barServices = barServices;
            _reviewServices = reviewServices;
        }

        public async Task<IActionResult> CreateBarReiew(BarReviewViewModel barVM)
        {
           var barReviewDTO = barVM.MapVMToReviewDTO();
           await _reviewServices.CreateBarReview(barReviewDTO);

            return RedirectToAction("Details", "Bars");
        }
    }
}
