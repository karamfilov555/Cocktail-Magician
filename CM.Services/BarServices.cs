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
        private readonly IFileUploadService _fileUploadService;

        public BarServices(CMContext context, IFileUploadService fileUploadService)
        {
            _context = context;
           _fileUploadService = fileUploadService;
        }

        public async Task<ICollection<HomePageBarDTO>> GetHomePageBars()
        {
            var bars = await _context.Bars
                .Include(b=>b.Address)
                .Where(b => b.DateDeleted == null)
                .OrderByDescending(b=>b.BarRating)
                .Take(5)
                .Select(b=>b.MapBarToHomePageBarDTO())
                .ToListAsync()
                .ConfigureAwait(false);
            return bars;
        }

        public async Task<BarDTO> GetBarByID(string id)
        {
            var bar = await _context.Bars
                .Where(b => b.Id == id)
                .Include(b=>b.Address)
                .ThenInclude(a=>a.Country)
                .Include(b => b.BarCocktails)
                .ThenInclude(b => b.Cocktail)
                .FirstOrDefaultAsync()
                .ConfigureAwait(false);
            bar.ValidateIfNull();
            var barDTO = bar.MapBarToDTO();
            barDTO.Country = bar.Address.Country.Name;
            barDTO.City = bar.Address.City;
            barDTO.Details = bar.Address.Details;
            return barDTO;
        }


        public async Task<PaginatedList<BarDTO>> GetAllBars(int? pageNumber, string sortOrder)
        {
            int pageSize =5;
            IQueryable<Bar> bars = _context.Bars
                .Where(b => b.DateDeleted == null)
                .Include(b => b.BarCocktails)
                .ThenInclude(b => b.Cocktail);
            switch (sortOrder)
            {
                case "name_desc":
                    bars = bars.OrderByDescending(b => b.Name);
                    break;
                case "Rating":
                    bars = bars.OrderBy(b => b.BarRating);
                    break;
                case "rating_asc":
                    bars = bars.OrderByDescending(b => b.BarRating);
                    break;
                default:
                    bars = bars.OrderBy(b => b.Name);
                    break;
            }
            var barDTOs = bars.Select(b => b.MapBarToDTO());
            var currentPage = await PaginatedList<BarDTO>.CreateAsync(barDTOs, pageNumber ?? 1, pageSize);
            return currentPage;
        }
        private async Task<Bar> GetBar(string id)
        {
            var bar = await _context.Bars
                .Where(b => b.Id == id && b.DateDeleted == null)
                .Include(b => b.BarCocktails)
                .ThenInclude(b => b.Cocktail)
                .FirstOrDefaultAsync()
                .ConfigureAwait(false);
            bar.ValidateIfNull();
            return bar;
        }

        public async Task<string> AddBarAsync(BarDTO barDTO)
        {
            string uniqueFileNamePath;
            if (barDTO.ImageUrl!=null)
            {
            uniqueFileNamePath = _fileUploadService.UploadFile(barDTO.BarImage);

            }
            else
            {
                uniqueFileNamePath= "/images/defaultBarImage.jpg";
            }
            barDTO.ImageUrl = uniqueFileNamePath;
            var newBar = barDTO.MapBarDTOToBar();
            var newAddress = barDTO.MapBarDTOToAddress();
            newBar.Address = newAddress;
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
            var barToDelete = await this.GetBar(id);
            barToDelete.DateDeleted = DateTime.Now.Date;
            await _context.SaveChangesAsync();
            return barToDelete.Name;
        }

        public async Task<string> Update(BarDTO barDto)
        {
            var barToEdit = await this.GetBar(barDto.Id);
            var uniqueFileNamePath = _fileUploadService.UploadFile(barDto.BarImage);
            barDto.ImageUrl = uniqueFileNamePath;
            var bar = barDto.MapBarDTOToBar();
            bar.BarRating = barToEdit.BarRating;
            var coctailsInBar = barDto.Cocktails.Select(c => c.MapToCocktailModel()).ToList();
            foreach (var cocktail in coctailsInBar)
            {
                await AddCocktailToBar(cocktail, bar);
            }
            _context.Entry(barToEdit).CurrentValues.SetValues(bar);
            _context.RemoveRange(barToEdit.BarCocktails);
            _context.AddRange(bar.BarCocktails);
            await _context.SaveChangesAsync();
            return barToEdit.Name;
        }
        //care
        public async Task<ICollection<BarDTO>> GetAllBarsByName(string searchCriteria)
        {
            var bars = await _context.Bars
                .Include(b => b.Address)
                .ThenInclude(a=>a.Country)
                .Include(b => b.BarCocktails)
                .ThenInclude(b => b.Cocktail)
                .Where(b => b.Name.Contains(searchCriteria,
                 StringComparison.OrdinalIgnoreCase)
                 && b.DateDeleted==null)
                .ToListAsync()
                .ConfigureAwait(false);
                                                  //Change the method;s name ?
            var allBarsDtos = bars.Select(b => b.MapBarToDTOWithFullAdress()).ToList();
            return allBarsDtos;
        }

        public async Task<ICollection<CountryDTO>> GetAllCountries()
        {
            var countries = _context.Countries;
            var countriesDTO = await countries.Select(c => new CountryDTO
            { Id = c.Id,
                Name = c.Name
            }).OrderByDescending(c=>c.Name)
            .ToListAsync();
            return countriesDTO;
        }
    }
}
