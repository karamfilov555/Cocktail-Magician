using CM.Data;
using CM.DTOs;
using CM.DTOs.Mappers;
using CM.Models;
using CM.Services.Contracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CM.Services
{
    public class CocktailServices : ICocktailServices
    {
        private readonly CMContext _context;
        private readonly IFileUploadService _fileUploadService;
        private readonly IIngredientServices _ingredientServices;
        private readonly IRecipeServices _recipeServices;

        public CocktailServices(CMContext context,
                                IFileUploadService fileUploadService,
                                IIngredientServices ingredientServices,
                                IRecipeServices recipeServices)
        {
            _context = context
                ?? throw new ArgumentNullException(nameof(context));
            _fileUploadService = fileUploadService
                ?? throw new ArgumentNullException(nameof(fileUploadService));
            _ingredientServices = ingredientServices
                ?? throw new ArgumentNullException(nameof(ingredientServices));
            _recipeServices = recipeServices
                ?? throw new ArgumentNullException(nameof(recipeServices));
        }

        public async Task<ICollection<CocktailDto>> GetCocktailsForHomePage()

                           => await _context.Cocktails
                                            .Include(c => c.CocktailComponents)
                                            .ThenInclude(c => c.Ingredient)
                                            .Include(c => c.BarCocktails)
                                            .ThenInclude(c => c.Bar)
                                            .Where(c => c.DateDeleted == null)
                                            .OrderByDescending(c=>c.Rating)
                                            .Take(5)
                                            .Select(c=>c.MapToCocktailDto())
                                            .ToListAsync()
                                            .ConfigureAwait(false);
          
        public async Task<CocktailDto> FindCocktailById(string id)
        {

            // INCLUDE !!
            var cocktail = await _context.Cocktails
                                            .Include(c => c.Reviews)
                                            .ThenInclude(c => c.User)
                                            .Include(c => c.CocktailComponents)
                                            .ThenInclude(c => c.Ingredient)
                                            .Include(c => c.BarCocktails)
                                            .ThenInclude(c => c.Bar)
                                            .ThenInclude(c => c.Address)
                                            .ThenInclude(b => b.Country)
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

            foreach (var cocktailComponenetDTO in cocktailDto.Ingredients)
            {
                var ingridientId = await _ingredientServices.GetIngredientIdByName(cocktailComponenetDTO.Ingredient);

                _context.CocktailComponent.Add(
                    new CocktailComponent
                    {
                        CocktailId = cocktail.Id,
                        IngredientId = ingridientId,
                        Quantity = cocktailComponenetDTO.Quantity,
                        Unit = cocktailComponenetDTO.Unit,
                        Name = cocktailComponenetDTO.Ingredient
                    });
            }
            cocktail.Recepie = await _recipeServices.ExtractRecipe(cocktail);
            await _context.SaveChangesAsync();
        }
        // to be deleted !  !
        public async Task<ICollection<CocktailDto>> GetAllCocktails()
        {
            var allCocktailsModels = await _context.Cocktails
                                                .Include(c => c.Reviews)
                                                .ThenInclude(c => c.User)
                                                .Include(c => c.CocktailComponents)
                                                .ThenInclude(c => c.Ingredient)
                                                .Include(c => c.BarCocktails)
                                                .ThenInclude(c => c.Bar)
                                                .Where(c => c.DateDeleted == null)
                                                .ToListAsync();

            var allCocktailsDto = allCocktailsModels.Select(c => c.MapToCocktailDto()).ToList();
            return allCocktailsDto;
        }

        public async Task<string> DeleteCocktial(string id)
        {
            var cocktailModel = await _context.Cocktails
                                            .FirstAsync(c => c.Id == id);
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
                                                .Include(c => c.CocktailComponents)
                                                .ThenInclude(c => c.Ingredient)
                                                .Include(c => c.BarCocktails)
                                                .ThenInclude(c => c.Bar)
                                                .Where(c => c.DateDeleted == null);

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
                                       .Where(c => c.DateDeleted == null)
                                       .CountAsync();

            int pageCount = (allCocktailsCount - 1) / cocktailsPerPage + 1;

            return pageCount;
        }

        public async Task<ICollection<CocktailDto>> GetAllCocktailsByName(string searchCriteria)
        {
            var cocktails = await _context.Cocktails
                            .Include(c => c.Reviews)
                            .ThenInclude(c => c.User)
                            .Include(c => c.CocktailComponents)
                            .ThenInclude(c => c.Ingredient)
                            .Include(c => c.BarCocktails)
                            .ThenInclude(c => c.Bar)
                            .ThenInclude(b => b.Address)
                            .ThenInclude(a => a.Country)
                            .Where(c => c.Name.Contains(searchCriteria,
                             StringComparison.OrdinalIgnoreCase)
                             && c.DateDeleted == null)
                            .ToListAsync();
            var cocktailsDtos = cocktails.Select(c => c.MapToCocktailDto()).ToList();
            return cocktailsDtos;
        }
        public async Task<bool> CheckIfCocktailExist(string id)
        => await _context.Cocktails
                         .AnyAsync(c => c.Id == id);

        public async Task<string> GetCocktailIdByName(string cocktailName)
        {
            var cocktail = await _context.Cocktails
                                         .FirstOrDefaultAsync(c => c.Name == cocktailName);
            return cocktail.Id;
        }

        public async Task<string> GetCocktailRecepie(string id)
        {
            var cocktail = await _context.Cocktails
                .Include(c => c.CocktailComponents)
                            .ThenInclude(c => c.Ingredient)
                            .FirstOrDefaultAsync(c => c.Id == id);

            // za seed neshtata recepti...
            if (cocktail.Recepie == null)
            {
                cocktail.Recepie = await _recipeServices.ExtractRecipe(cocktail);
            }
            return cocktail.Recepie;
        }
        private async Task<Cocktail> GetCocktail(string id)
        {
            var cocktail = await _context.Cocktails
                .Where(b => b.Id == id && b.DateDeleted == null)
                .Include(b => b.BarCocktails)
                .ThenInclude(b => b.Cocktail)
                .Include(b => b.CocktailComponents)
                .FirstOrDefaultAsync()
                .ConfigureAwait(false);

            return cocktail;
        }
        public async Task<string> Update(CocktailDto cocktailDto)
        {
            var cocktailToEdit = await GetCocktail(cocktailDto.Id);
            var uniqueFileNamePath = _fileUploadService.UploadFile(cocktailDto.CocktailImage);
            cocktailDto.Image = uniqueFileNamePath;
            var cocktail = cocktailDto.MapToEditModel();
            cocktail.Rating = cocktailToEdit.Rating;
            var newCocktailComponents = new List<CocktailComponent>();
            foreach (var component in cocktail.CocktailComponents)
            {
                var ingridientId = await _ingredientServices
                                            .GetIngredientIdByName(component.Name);

                newCocktailComponents.Add(
                    new CocktailComponent
                    {
                        CocktailId = cocktailDto.Id,
                        IngredientId = ingridientId
                    });
            }
            //var recipeSB = new StringBuilder();
            //foreach (var component in cocktail.CocktailComponents)
            //{
            //    recipeSB.AppendLine(component.Name + " " + component.Quantity + " " + component.Unit);
            //}
            //recipeSB.AppendLine(cocktail.Recepie);
            //var cocktail = cocktailDto.MapToCocktailModel();
            cocktail.Recepie = await _recipeServices.ExtractRecipe(cocktail);
            cocktail.CocktailComponents = newCocktailComponents;

            _context.Entry(cocktailToEdit).CurrentValues.SetValues(cocktail);
            _context.RemoveRange(cocktailToEdit.CocktailComponents);
            _context.AddRange(cocktail.CocktailComponents);
            await _context.SaveChangesAsync();
            return cocktailToEdit.Name;
        }

    }
}

