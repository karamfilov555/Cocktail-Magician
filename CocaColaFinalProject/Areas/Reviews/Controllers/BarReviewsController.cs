using CM.Services.Contracts;
using CM.Web.Areas.Bars.Models;
using CM.Web.Areas.Reviews.Models;
using CM.Web.Mappers;
using CM.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CM.Web.Areas.Reviews.Controllers
{
    [Area("Reviews")]
    public class BarReviewsController : Controller
    {
        private readonly ICocktailServices _cocktailServices;
        private readonly IBarServices _barServices;
        private readonly IReviewServices _reviewServices;

        public BarReviewsController(IBarServices barServices,
             IReviewServices reviewServices)
        {
            _barServices = barServices;
            _reviewServices = reviewServices;
        }

        [HttpPost]
        public async Task<IActionResult> CreateBarReview(BarReviewViewModel barVM)
        {
            var barReviewDTO = barVM.MapVMToReviewDTO();
            await _reviewServices.CreateBarReview(barReviewDTO);

            return RedirectToAction("Details", "Bars", new { id = barVM.BarId, area = "Bars" });
        }
    }
}
