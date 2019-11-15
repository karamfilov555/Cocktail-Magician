using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CM.Data;
using CM.Models;
using CM.Services.Contracts;
using CM.Web.Mappers;
using CM.Web.Areas.Bars.Models;
using CM.Web.Areas.Reviews.Models;
using Microsoft.AspNetCore.Authorization;
using NToastNotify;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using CM.DTOs;
using CM.Services.Common;

namespace CM.Web.Areas.Bars.Controllers
{
    [Area("Bars")]
    public class BarsController : Controller
    {

        private readonly IBarServices _barServices;
        private readonly ICocktailServices _cocktailServices;
        private readonly IReviewServices _reviewServices;
        private readonly IToastNotification _toast;



        public BarsController(IBarServices barServices, ICocktailServices cocktailServices,
            IReviewServices reviewServices, IToastNotification toast)
        {
            _barServices = barServices;
            _cocktailServices = cocktailServices;
            _reviewServices = reviewServices;
            _toast = toast;
        }


        // GET: Bars/Bars/Details/5
        //[Route("bars/details/{id}")]
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var barDTO = await _barServices.GetBarByID(id);
            if (barDTO == null)
            {
                return NotFound();
            }
            var barVM = barDTO.MapBarToVM();
            var reviews = await _reviewServices.GetAllReviewsForBar(id);
            barVM.Reviews = reviews.Select(r => r.MapReviewDTOToVM()).ToList();

            return View(barVM);
        }

        // GET:
        [Route("bars/list")]
        public async Task<IActionResult> Index(string sortOrder, string currentFilter,
            int? pageNumber)
        {
            var listVM = new ListBarsViewModel();
            listVM.CurrentSortOrder = sortOrder;
            listVM.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            listVM.RatingSortParm = sortOrder == "Rating" ? "rating_asc" : "Rating";
            var bars = await _barServices.GetAllBars(pageNumber, sortOrder);
            var pagList = new PaginatedList<BarViewModel>()
            {
                PageIndex = bars.PageIndex,
                TotalPages = bars.TotalPages
            };
            foreach (var item in bars)
            {
                pagList.Add(item.MapBarToVM());
            }
            listVM.AllBars = pagList;
            return View(listVM);
        }
        // GET: Bars/Bars/Create
        [HttpGet]
        [Authorize(Roles = "Manager, Administrator")]
        public async Task<IActionResult> Create()
        {
            var allCocktails = await _cocktailServices.GetAllCocktails();
            var allCountries = await _barServices.GetAllCountries();
            var createBarVM = new CreateBarVM();
            createBarVM.AllCocktails = allCocktails
                .Select(c => new SelectListItem(c.Name, c.Id)).ToList();
            createBarVM.AllCountries = allCountries
                .Select(c => new SelectListItem(c.Name, c.Id)).ToList();
            return View(createBarVM);
        }

        //POST: Bars/Bars/Create
        [HttpPost]
        [Authorize(Roles = "Manager, Administrator")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateBarVM barVM)
        {
            if (ModelState.IsValid)
            {
                if (ImageIsValid(barVM.BarImage))
                {
                    var barDTO = barVM.MapBarVMToDTO();
                    var barName = await _barServices.AddBar(barDTO);
                    _toast.AddSuccessToastMessage($"You successfully added \"{barName}\" bar!");
                    return RedirectToAction(nameof(Index));
                }

            }
            var allCocktails = await _cocktailServices.GetAllCocktails();
            barVM.AllCocktails = allCocktails
                .Select(c => new SelectListItem(c.Name, c.Id)).ToList();
            return View(barVM);
        }

        private bool ImageIsValid(IFormFile barImage)
        {
            var imageSizeInKb = barImage.Length / 1024;
            var type = barImage.ContentType;

            if (type != "image/jpeg" && type != "image/jpg" && type != "image/png")
            {
                _toast.AddErrorToastMessage($"Allowed picture formats: \".jpg\", \".jpeg\" and \".png\"!");
                return false;
            }
            else if (imageSizeInKb > 100)
            {
                _toast.AddErrorToastMessage($"The picture size is too big! Maximum size: 100 kb");
                return false;
            }
            return true;
        }

        // GET: Bars/Bars/Edit/5
        [Authorize(Roles = "Manager, Administrator")]
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var allCocktails = await _cocktailServices.GetAllCocktails();
            var bar = await _barServices.GetBarByID(id);
            var barVM = bar.MapBarToCreateBarVM();
            barVM.AllCocktails = allCocktails
                .Select(c => new SelectListItem(c.Name, c.Id)).ToList();
            return View(barVM);
        }

        // POST: Bars/Bars/Edit/5
        [HttpPost]
        [Authorize(Roles = "Manager, Administrator")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(CreateBarVM barVM)
        {

            if (ModelState.IsValid)
            {
                if (ImageIsValid(barVM.BarImage))
                {
                    var barDTO = barVM.MapBarVMToDTO();
                    var barName = await _barServices.Update(barDTO);
                    _toast.AddSuccessToastMessage($"You successfully edited \"{barName}\" bar!");
                    return RedirectToAction(nameof(Index));
                }
            }
            var allCocktails = await _cocktailServices.GetAllCocktails();
            barVM.AllCocktails = allCocktails
                .Select(c => new SelectListItem(c.Name, c.Id)).ToList();
            return View(barVM);
        }

        // GET: Bars/Bars/Delete/5
        [Authorize(Roles = "Manager, Administrator")]
        [HttpGet]
        public async Task<IActionResult> Delete(string id)
        {
            var barDTO = await _barServices.GetBarByID(id);
            var barVM = barDTO.MapBarToVM();
            return View(barVM);
        }


        [Authorize(Roles = "Manager, Administrator")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var barName = await _barServices.Delete(id);
            _toast.AddSuccessToastMessage($"You successfully deleted \"{barName}\" bar!");
            return RedirectToAction(nameof(Index));
        }


    }
}
