using CM.Data;
using CM.DTOs;
using CM.DTOs.Mappers;
using CM.Models;
using CM.Services.Common;
using CM.Services.Contracts;
using CM.Services.CustomExeptions;
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

        public BarServices(CMContext context, IFileUploadService fileUploadService)//tested
        {
            _context = context
                         ?? throw new MagicExeption(ExeptionMessages.ContextNull);
            _fileUploadService = fileUploadService
                         ?? throw new MagicExeption(ExeptionMessages.IFileUploadServiceNull);
        }

        public async Task<ICollection<HomePageBarDTO>> GetHomePageBars() //tested
        {
            var bars = await _context.Bars
                .Include(b => b.Address)
                .Where(b => b.DateDeleted == null)
                .OrderByDescending(b => b.BarRating)
                .Take(5)
                .Select(b => b.MapBarToHomePageBarDTO())
                .ToListAsync()
                .ConfigureAwait(false);

            return bars;
        }

        public async Task<BarDTO> GetBarByID(string id) // tested with ex
        {
            id.ValidateIfNull(ExeptionMessages.IdNull);

            var bar = await _context.Bars
                .Where(b => b.Id == id)
                .Include(b => b.Address)
                .ThenInclude(a => a.Country)
                .Include(b => b.BarCocktails)
                .ThenInclude(b => b.Cocktail)
                .FirstOrDefaultAsync()
                .ConfigureAwait(false);

            bar.ValidateIfNull(ExeptionMessages.BarNull);

            var barDTO = bar.MapBarToDTO();
            barDTO.CountryId = bar.Address.Country.Id;
            barDTO.Country = bar.Address.Country.Name;
            barDTO.City = bar.Address.City;
            barDTO.Details = bar.Address.Details;
            return barDTO;
        }


        public async Task<PaginatedList<BarDTO>> GetAllBars(int? pageNumber, string sortOrder)
        // tested
        {
            int pageSize = 2;

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
        public async Task<Bar> GetBar(string id) // tested
        {
            id.ValidateIfNull(ExeptionMessages.IdNull);

            var bar = await _context.Bars
                .Where(b => b.Id == id && b.DateDeleted == null)
                .Include(b => b.Address)
                .ThenInclude(a => a.Country)
                .Include(b => b.BarCocktails)
                .ThenInclude(b => b.Cocktail)
                .FirstOrDefaultAsync()
                .ConfigureAwait(false);

            bar.ValidateIfNull(ExeptionMessages.BarNull);

            return bar;
        }

        public async Task<string> AddBarAsync(BarDTO barDTO) //tested
        {
            barDTO.ValidateIfNull(ExeptionMessages.BarDtoNull);
            barDTO.ImageUrl =  _fileUploadService.SetUniqueImagePath(barDTO.BarImage);

            var newBar = barDTO.MapBarDTOToBar();                  // to be tested in MapperTests
            var newAddress = barDTO.MapBarDTOToAddress();          // to be tested in MapperTests
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

        public async Task AddCocktailToBar(Cocktail cocktail, Bar bar) //tested
        {
            cocktail.ValidateIfNull(ExeptionMessages.CocktailNull);
            bar.ValidateIfNull(ExeptionMessages.BarNull);
            if (!bar.BarCocktails.Any(bc => bc.CocktailId == cocktail.Id))
            {
                bar.BarCocktails.Add(new BarCocktail() { BarId = bar.Id, CocktailId = cocktail.Id });
            }
        }
        public async Task<string> Delete(string id) //tested
        {
            id.ValidateIfNull(ExeptionMessages.IdNull);
            var barToDelete = await this.GetBar(id);
            barToDelete.ValidateIfNull(ExeptionMessages.BarNull);
            barToDelete.DateDeleted = DateTime.Now.Date;
            await _context.SaveChangesAsync();
            return barToDelete.Name;
        }

        public async Task<string> Update(BarDTO barDto) //tested
        {
            barDto.ValidateIfNull(ExeptionMessages.BarDtoNull);
            var barToEdit = await this.GetBar(barDto.Id);
            barToEdit.ValidateIfNull(ExeptionMessages.BarNull);

            barDto.ImageUrl = _fileUploadService.SetUniqueImagePath(barDto.BarImage);

            barToEdit = barDto.EditBarDTOToBar(barToEdit);
            var coctailsInBar = barDto.Cocktails.Select(c => c.MapToCocktailModel()).ToList();
            foreach (var cocktail in coctailsInBar)
            {
                await AddCocktailToBar(cocktail, barToEdit);
            }

            await _context.SaveChangesAsync();
            return barToEdit.Name;
        }
        //care
        public async Task<ICollection<BarDTO>> GetAllBarsByName(string searchCriteria)//tested
        {
            var bars = await _context.Bars
                .Include(b => b.Address)
                .ThenInclude(a => a.Country)
                .Include(b => b.BarCocktails)
                .ThenInclude(b => b.Cocktail)
                .Where(b => b.Name.Contains(searchCriteria,
                 StringComparison.OrdinalIgnoreCase)
                 && b.DateDeleted == null) 
                .ToListAsync()
                .ConfigureAwait(false);
            //Change the method;s name ?
            var allBarsDtos = bars.Select(b => b.MapBarToDTOWithFullAdress()).ToList();
            return allBarsDtos;
        }

        public async Task<ICollection<CountryDTO>> GetAllCountries()//tested
        {
            var countries = _context.Countries;
            var countriesDTO = await countries.Select(c => new CountryDTO
            {
                Id = c.Id,
                Name = c.Name
            }).OrderBy(c => c.Name)
            .ToListAsync();
            return countriesDTO;
        }
    }
}
