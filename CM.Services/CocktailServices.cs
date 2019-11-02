using CM.Data;
using CM.DTOs;
using CM.DTOs.Mappers;
using CM.Services.Contracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CM.Services
{
    public class CocktailServices : ICocktailServices
    {
        private readonly CMContext _context;
        public CocktailServices(CMContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<ICollection<CocktailDto>> GetCocktailsForHomePage()
        {
            // include ingredients ... posle ... da se vzima po rating 
            var cocktails = await _context.Cocktails
                                            .Where(c=>c.DateDeleted == null)
                                            .Include(c=>c.CocktailIngredients)
                                            .ThenInclude(c => c.Ingredient)
                                            .Include(c => c.BarCocktails)
                                            .ThenInclude(c => c.Bar)
                                            .ToListAsync()
                                            .ConfigureAwait(false);
            //map to dto before pass to fe
            var cocktailDtos = cocktails.Select(c => c.MapToCocktailDto()).ToList();
            
            return cocktailDtos;
        }
        public async Task<CocktailDto> FindCocktailById(string id)
        {
            // INCLUDE !!
            var cocktail = await _context.Cocktails
                                            .Include(c=>c.Reviews)
                                            .ThenInclude(c=>c.User)
                                            .Include(c=>c.CocktailIngredients)
                                            .ThenInclude(c=>c.Ingredient)
                                            .Include(c=>c.BarCocktails)
                                            .ThenInclude(c=>c.Bar)
                                            .FirstOrDefaultAsync(c => c.Id == id 
                                             && c.DateDeleted == null)
                                            .ConfigureAwait(false);

            var cocktailDto = cocktail.MapToCocktailDto();

            return cocktailDto;
        }

        public async Task AddCocktail(CocktailDto cocktailDto)
        {
            if (cocktailDto == null)
            {
                // check , some validaitons
            }
            var cocktail = cocktailDto.MapToCocktailModel();
            _context.Cocktails.Add(cocktail);
            await _context.SaveChangesAsync();
            cocktailDto.CocktailIngredients.ForEach(ci => ci.CocktailId = cocktail.Id);
            cocktail.CocktailIngredients=cocktailDto.CocktailIngredients;
            await _context.SaveChangesAsync();

        }
        public async Task<ICollection<CocktailDto>> GetAllCocktails()
        {
            var allCocktailsModels = await _context.Cocktails
                                                .Include(c => c.Reviews)
                                                .ThenInclude(c => c.User)
                                                .Include(c => c.CocktailIngredients)
                                                .ThenInclude(c => c.Ingredient)
                                                .Include(c => c.BarCocktails)
                                                .ThenInclude(c => c.Bar)
                                                .Where(c=>c.DateDeleted == null)
                                                .ToListAsync();

            var allCocktailsDto = allCocktailsModels.Select(c => c.MapToCocktailDto()).ToList();
            return allCocktailsDto;
        }

        public async Task<string> DeleteCocktial(string id)
        {
            var cocktailModel = await _context.Cocktails
                                            .FirstAsync(c=>c.Id==id);
            cocktailModel.DateDeleted = DateTime.Now;
            await _context.SaveChangesAsync();
            return cocktailModel.Name;
        }
        public async Task<string> GetCocktailNameById(string id)
        {
            var cocktail = await _context.Cocktails.FirstAsync(c => c.Id == id);
            return cocktail.Name;
        }

        public async Task<IList<CocktailDto>> GetFiveCocktails(int currPage = 1)
        {
            var fiveCocktails = await _context.Cocktails
                                        .Skip((currPage - 1) * 5)
                                        .Take(5)
                                        .ToListAsync();

            var cocktailsDtos = fiveCocktails.Select(c => c.MapToCocktailDto()).ToList();
            return cocktailsDtos;
        }
        public async Task<int> GetPageCountForCocktials(int cocktailsPerPage)
        {
            var allCocktailsCount = await _context
                                       .Cocktails
                                       .CountAsync();

            int pageCount = (allCocktailsCount - 1) / cocktailsPerPage + 1;

            return pageCount;
        }
    }
}

