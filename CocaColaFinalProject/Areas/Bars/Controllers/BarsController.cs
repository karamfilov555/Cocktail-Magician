﻿using System;
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

namespace CM.Web.Areas.Bars.Controllers
{
    [Area("Bars")]
    public class BarsController : Controller
    {
        
        private readonly IBarServices _barServices;

        public BarsController(IBarServices barServices)
        {
            _barServices = barServices;
        }


        // GET: Bars/Bars/Details/5
        [Route("bars/details/{id}")]
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var barDTO =await _barServices.GetBarByID(id);
            if (barDTO == null)
            {
                return NotFound();
            }
            var barVM = barDTO.MapBarToVM();

            return View(barVM);
        }

        //// GET: Bars/Bars
        //public async Task<IActionResult> Index()
        //{
        //    return View(await _context.Bars.ToListAsync());
        //}
        //// GET: Bars/Bars/Create
        //public IActionResult Create()
        //{
        //    return View();
        //}

        //// POST: Bars/Bars/Create
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create([Bind("Id,Name,Image,Website,Address,BarRating,DateDeleted")] Bar bar)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _context.Add(bar);
        //        await _context.SaveChangesAsync();
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(bar);
        //}

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

        //// GET: Bars/Bars/Delete/5
        //public async Task<IActionResult> Delete(string id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var bar = await _context.Bars
        //        .FirstOrDefaultAsync(m => m.Id == id);
        //    if (bar == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(bar);
        //}

        //// POST: Bars/Bars/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(string id)
        //{
        //    var bar = await _context.Bars.FindAsync(id);
        //    _context.Bars.Remove(bar);
        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}

        //private bool BarExists(string id)
        //{
        //    return _context.Bars.Any(e => e.Id == id);
        //}
    }
}