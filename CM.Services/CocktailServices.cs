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
                ?? throw new MagicExeption(ExeptionMessages.ContextNull);
            _fileUploadService = fileUploadService
                ?? throw new MagicExeption(ExeptionMessages.IFileUploadServiceNull);
            _ingredientServices = ingredientServices
                ?? throw new MagicExeption(ExeptionMessages.IIngredientServiceNull);
            _recipeServices = recipeServices
                ?? throw new MagicExeption(ExeptionMessages.IRecipeServiceNull);
          
        }
        //Tested
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
        //Fully Tested  
        public async Task<CocktailDto> FindCocktailById(string id)
        {
            id.ValidateIfNull("Id cannot be null!");
            var cocktail = await _context.Cocktails
                                            .Include(c => c.Reviews)
                                            .ThenInclude(c => c.User)
                                            .ThenInclude(c => c.CocktailReviews)
                                            .Include(c => c.CocktailComponents)
                                            .ThenInclude(c => c.Ingredient)
                                            .Include(c => c.BarCocktails)
                                            .ThenInclude(c => c.Bar)
                                            .ThenInclude(c => c.Address)
                                            .ThenInclude(b => b.Country)
                                            .FirstOrDefaultAsync(c => c.Id == id
                                             && c.DateDeleted == null)
                                            .ConfigureAwait(false);
            cocktail.ValidateIfNull("Cocktail doesn't exist in DB!");
            var cocktailDto = cocktail.MapToCocktailDto();
            return cocktailDto;
        }
        //Tested
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
        //Tested
        public async Task<string> GetCocktailNameById(string id)
        {
            id.ValidateIfNull("ID cannot be null!");
            var cocktail = await _context.Cocktails.FirstOrDefaultAsync(c => c.Id == id&&c.DateDeleted==null);
            cocktail.ValidateIfNull("Cocktail doesn't exist in DB!");
            return cocktail.Name;
        }
        //Tested
        public async Task AddCocktail(CocktailDto cocktailDto)
        {
            cocktailDto.ValidateIfNull("Model is not valid!");
            if (cocktailDto.CocktailImage!=null)
            {
            var uniqueFileNamePath = _fileUploadService.UploadFile(cocktailDto.CocktailImage);
            cocktailDto.Image = uniqueFileNamePath;
            }
            var cocktail = cocktailDto.MapToCocktailModel();
            _context.Cocktails.Add(cocktail);
            await _context.SaveChangesAsync();

            foreach (var cocktailComponenetDTO in cocktailDto.Ingredients)
            {
                var ingridientId = await _ingredientServices.GetIngredientIdByNameAsync(cocktailComponenetDTO.Ingredient);

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
        //Tested
        public async Task<string> DeleteCocktial(string id)
        {
            id.ValidateIfNull("ID cannot be null!");
            var cocktailModel = await this.GetCocktail(id);
            cocktailModel.DateDeleted = DateTime.Now;
            await _context.SaveChangesAsync();
            return cocktailModel.Name;
        }
        //Tested
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
        //Tested
        public async Task<int> GetPageCountForCocktials(int cocktailsPerPage)
        {
            var allCocktailsCount = await _context
                                       .Cocktails
                                       .Where(c => c.DateDeleted == null)
                                       .CountAsync();

            int pageCount = (allCocktailsCount - 1) / cocktailsPerPage + 1;

            return pageCount;
        }   

        //Tested
        public async Task<bool> CheckIfCocktailExist(string id)
        {
            id.ValidateIfNull("ID cannot be null!");
            return await _context.Cocktails
                            .AnyAsync(c => c.Id == id);
        }
        //Tested
        public async Task<string> GetCocktailIdByName(string cocktailName)
        {
            cocktailName.ValidateIfNull("Name cannot be null!");
            var cocktail = await _context.Cocktails
                                         .FirstOrDefaultAsync(c => c.Name == cocktailName&&c.DateDeleted==null);
            cocktail.ValidateIfNull("Cocktail doesn't exist in DB!");
            return cocktail.Id;
        }
        //Tested
        public async Task<string> GetCocktailRecipe(string id)
        {
            id.ValidateIfNull("ID cannot be null!");
            var cocktail = await this.GetCocktail(id);

            // za seed neshtata recepti...
            if (cocktail.Recepie == null)
            {
                cocktail.Recepie = await _recipeServices.ExtractRecipe(cocktail);
            }
            return cocktail.Recepie;
        }
        //Tested
        public async Task<Cocktail> GetCocktail(string id)
        {
            id.ValidateIfNull("ID cannot be null!");
            var cocktail = await _context.Cocktails
                .Where(b => b.Id == id && b.DateDeleted == null)
                //.Include(b => b.BarCocktails)
                //.ThenInclude(b => b.Cocktail)
                .Include(b => b.CocktailComponents)
                .ThenInclude(cc=>cc.Ingredient)
                .FirstOrDefaultAsync()
                .ConfigureAwait(false);
            cocktail.ValidateIfNull("Cocktail doesn't exist in DB!");
            return cocktail;
        }
        //Tested
        public async Task<string> Update(CocktailDto cocktailDto)
        {
            cocktailDto.ValidateIfNull("Model is invalid!");
            var cocktailToEdit = await GetCocktail(cocktailDto.Id);
            
            var cocktail = cocktailDto.MapToEditModel();
            if (cocktailDto.CocktailImage!=null)
            {
            var uniqueFileNamePath = _fileUploadService.UploadFile(cocktailDto.CocktailImage);
            cocktail.Image = uniqueFileNamePath;
            }
            else
            {
                cocktail.Image = cocktailToEdit.Image;
            }
            cocktail.Rating = cocktailToEdit.Rating;
            var newCocktailComponents = new List<CocktailComponent>();

            foreach (var component in cocktail.CocktailComponents)
            {
                var ingridientId = await _ingredientServices
                                         .GetIngredientIdByNameAsync(component.Name);
                
                if (ingridientId==null)
                {
                    var newIngr = new Ingredient() { Name = component.Name };
                    _context.Add(newIngr);
                    await _context.SaveChangesAsync();
                    ingridientId = newIngr.Id;
                }
                newCocktailComponents.Add(
                    new CocktailComponent
                    {
                        CocktailId = cocktailDto.Id,
                        IngredientId = ingridientId
                    });
            }
            cocktail.CocktailComponents = newCocktailComponents;

            _context.Entry(cocktailToEdit).CurrentValues.SetValues(cocktail);
            _context.RemoveRange(cocktailToEdit.CocktailComponents);
            _context.AddRange(cocktail.CocktailComponents);
            await _context.SaveChangesAsync();
            cocktail.Recepie = await _recipeServices.ExtractRecipe(cocktailToEdit);
            await _context.SaveChangesAsync();
            return cocktailToEdit.Name;
        }
    }
}


        //public async Task<ICollection<CocktailDto>> GetAllCocktailsByName(string searchCriteria)
        //{
        //    var cocktails = await _context.Cocktails
        //                    .Include(c => c.Reviews)
        //                    .ThenInclude(c => c.User)
        //                    .Include(c => c.CocktailComponents)
        //                    .ThenInclude(c => c.Ingredient)
        //                    .Include(c => c.BarCocktails)
        //                    .ThenInclude(c => c.Bar)
        //                    .ThenInclude(b => b.Address)
        //                    .ThenInclude(a => a.Country)
        //                    .Where(c => c.Name.Contains(searchCriteria,
        //                     StringComparison.OrdinalIgnoreCase)
        //                     && c.DateDeleted == null)
        //                    .ToListAsync();
        //    var cocktailsDtos = cocktails.Select(c => c.MapToCocktailDto()).ToList();
        //    return cocktailsDtos;
        //}