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
        private readonly IFileUploadService _fileUploadService;

        public CocktailServices(CMContext context, IFileUploadService fileUploadService)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _fileUploadService = fileUploadService;
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
                                            .ThenInclude(c=>c.Address)
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
            var uniqueFileNamePath = _fileUploadService.UploadFile(cocktailDto.CocktailImage);
            cocktailDto.Image = uniqueFileNamePath;
            var cocktail = cocktailDto.MapToCocktailModel();
            _context.Cocktails.Add(cocktail);
            await _context.SaveChangesAsync();
            cocktailDto.CocktailIngredients.ForEach(ci => ci.CocktailId = cocktail.Id);
            cocktail.CocktailIngredients=cocktailDto.CocktailIngredients;
            await _context.SaveChangesAsync();

        }
        // to be deleted !  !
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

        public async Task<IList<CocktailDto>> GetFiveSortedCocktailsAsync(string sortOrder, int currPage = 1)
        {
            var sortedCocktails = _context.Cocktails
                                                .Include(c => c.Reviews)
                                                .ThenInclude(c => c.User)
                                                .Include(c => c.CocktailIngredients)
                                                .ThenInclude(c => c.Ingredient)
                                                .Include(c => c.BarCocktails)
                                                .ThenInclude(c => c.Bar)
                                                .Where(c=>c.DateDeleted==null);

            switch (sortOrder)
            {
                case "name_desc":
                    sortedCocktails = sortedCocktails
                                                .OrderByDescending(b => b.Name);
                    break;

                case "rating":
                    sortedCocktails = sortedCocktails
                                                .OrderBy(b => b.Rating);
                    break;

                case "rating_desc":
                    sortedCocktails = sortedCocktails
                                                .OrderByDescending(s => s.Rating);
                    break;

                default:
                    sortedCocktails = sortedCocktails
                                                .OrderBy(s => s.Name);
                    break;
            }

            var fiveSortedCocktails = await sortedCocktails
                                        .Skip((currPage - 1) * 5)
                                        .Take(5)
                                        .ToListAsync();

            var fiveSortedCocktailsDtos = fiveSortedCocktails
                                                    .Select(c => c.MapToCocktailDto())
                                                    .ToList();
            return fiveSortedCocktailsDtos;
        }
        public async Task<int> GetPageCountForCocktials(int cocktailsPerPage)
        {
            var allCocktailsCount = await _context
                                       .Cocktails
                                       .Where(c=>c.DateDeleted==null)
                                       .CountAsync();

            int pageCount = (allCocktailsCount - 1) / cocktailsPerPage + 1;

            return pageCount;
        }
    }
}

