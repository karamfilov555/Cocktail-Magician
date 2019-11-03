using CM.Data;
using CM.DTOs;
using CM.DTOs.Mappers;
using CM.Models;
using CM.Services.Common;
using CM.Services.Contracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CM.Services
{
    public class BarServices : IBarServices
    {
        private readonly CMContext _context;
        public BarServices(CMContext context)
        {
            _context = context;
        }

        public async Task<ICollection<BarDTO>> GetHomePageBars()
        {
            //TODO reduce number and get only required fields
            var bars = await _context.Bars
                .Where(b=>b.DateDeleted==null)
                .ToListAsync()
                .ConfigureAwait(false);
            var barDTOs = bars.Select(b => b.MapBarToDTO()).ToList();
            return barDTOs;
        }

        public async Task<BarDTO> GetBarByID(string id)
        {
            var bar = await _context.Bars
                .Where(b=>b.Id==id)
                .Include(b => b.BarCocktails)
                .ThenInclude(b => b.Cocktail)
                .FirstOrDefaultAsync()
                .ConfigureAwait(false);
            bar.ValidateIfNull();
            var barDTO = bar.MapBarToDTO();
            return barDTO;
        }


        public async Task<ICollection<BarDTO>> GetAllBars()
        {
            var bars = await _context.Bars
                .Where(b => b.DateDeleted == null)
                .Include(b=>b.BarCocktails)
                .ThenInclude(b=>b.Cocktail)
                .ToListAsync()
                .ConfigureAwait(false);
            var barDTOs = bars.Select(b => b.MapBarToDTO()).ToList();
            return barDTOs;
        }
        public async Task<Bar> GetBar(string id)
        {
            var bar = await _context.Bars.FindAsync(id);
            bar.ValidateIfNull();
            return bar;
        }

        public async Task<string> AddBar(BarDTO barDTO)
        {
            var newBar = barDTO.MapBarDTOToBar();
            await _context.Bars.AddAsync(newBar).ConfigureAwait(false);
            await _context.SaveChangesAsync();
            var coctailsInBar = barDTO.Cocktails.Select(c => c.MapToCocktailModel()).ToList();
            foreach (var cocktail in coctailsInBar)
            {
               await AddCocktailToBar(cocktail, newBar);
            }
            await _context.SaveChangesAsync();
            return newBar.Name;
        }

        public async Task AddCocktailToBar(Cocktail cocktail, Bar bar)
        {
           bar.BarCocktails.Add(new BarCocktail() { BarId = bar.Id, CocktailId = cocktail.Id });
        }
        public async Task<string> Delete(string id)
        {
            var barToDelete = await this.GetBarByID(id);
            barToDelete.DateDeleted = DateTime.Now.Date;
            await _context.SaveChangesAsync();
            return barToDelete.Name;
        }

        public async Task<string> Update(BarDTO barDto)
        {
            var barToEdit =await this.GetBar(barDto.Id);
            var bar = barDto.MapBarDTOToBar();
            bar.BarRating = barToEdit.BarRating;
            var coctailsInBar = barDto.Cocktails.Select(c => c.MapToCocktailModel()).ToList();
            foreach (var cocktail in coctailsInBar)
            {
                await AddCocktailToBar(cocktail, bar);
            }

            _context.Entry(barToEdit).CurrentValues.SetValues(bar);
            await _context.SaveChangesAsync();
            return barToEdit.Name;
        }

    }

}
