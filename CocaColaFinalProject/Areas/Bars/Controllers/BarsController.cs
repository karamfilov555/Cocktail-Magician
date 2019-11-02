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

namespace CM.Web.Areas.Bars.Controllers
{
    [Area("Bars")]
    public class BarsController : Controller
    {

        private readonly IBarServices _barServices;
        private readonly ICocktailServices _cocktailServices;
        private readonly IReviewServices _reviewServices;

        public BarsController(IBarServices barServices, ICocktailServices cocktailServices,
            IReviewServices reviewServices)
        {
            _barServices = barServices;
            _cocktailServices = cocktailServices;
            _reviewServices = reviewServices;
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
        public async Task<IActionResult> Index()
        {
            var bars = await _barServices.GetAllBars();
            var barsVM = bars.Select(b => b.MapBarToVM()).ToList();
            return View(barsVM);
        }
        // GET: Bars/Bars/Create
        [HttpGet]
        [Authorize(Roles = "Manager, Administrator")]
        public async Task<IActionResult> Create()
        {
            var allCocktails = await _cocktailServices.GetAllCocktails();
            var createBarVM = new CreateBarVM();
            createBarVM.AllCocktails = allCocktails
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
                var barDTO = barVM.MapBarVMToDTO();
                var barName = await _barServices.AddBar(barDTO);

                return RedirectToAction(nameof(Index));
            }
            return View(barVM);
        }

        //// GET: Bars/Bars/Edit/5
        //public async Task<IActionResult> Edit(string id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var bar = await _context.Bars.FindAsync(id);
        //    if (bar == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(bar);
        //}

        //// POST: Bars/Bars/Edit/5
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(string id, [Bind("Id,Name,Image,Website,Address,BarRating,DateDeleted")] Bar bar)
        //{
        //    if (id != bar.Id)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            _context.Update(bar);
        //            await _context.SaveChangesAsync();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!BarExists(bar.Id))
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
        //    return View(bar);
        //}

        // GET: Bars/Bars/Delete/5
        [Authorize(Roles="Manager, Administrator")]
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
           var barName= await _barServices.Delete(id);
           return RedirectToAction(nameof(Index));
        }

    }
}
