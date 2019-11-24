using CM.Data;
using CM.DTOs;
using CM.DTOs.Mappers;
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
    public class IngredientServices : IIngredientServices
    {
        private readonly CMContext _context;
        public IngredientServices(CMContext context)
        {
            _context = context;
        }
        public async Task<IList<IngredientDTO>> GetAllIngredients()
        {
            return await _context.Ingredients.Include(i => i.CocktailComponents)
                                  .Select(i => i.MapToDtoModel())
                                  .ToListAsync();
        }
        public async Task<IList<IngredientDTO>> GetTenIngredientsAsync(int currPage)
        {
            return await _context.Ingredients
                .Include(i => i.CocktailComponents)
                                   .Where(c => c.DateDeleted == null)
                                   .OrderByDescending(c => c.Name)
                                   .Skip((currPage - 1) * 10)
                                   .Take(10)
                                   .Select(i => i.MapToDtoModel())
                                   .ToListAsync()
                                   .ConfigureAwait(false);
        }

        public async Task<int> GetPageCountForIngredientsAsync(int ingredientsPerPage)
        {
            var allIngredientsCount = await _context
                                       .Cocktails
                                       .Where(c => c.DateDeleted == null)
                                       .CountAsync();

            int pageCount = (allIngredientsCount - 1) / ingredientsPerPage + 1;

            return pageCount;
        }
        public async Task AddIngredient(IngredientDTO ingredientDto)
        {
            var ingredientCtx = ingredientDto.MapToCtxModel();
            _context.Add(ingredientCtx);
            await _context.SaveChangesAsync();
        }
        public async Task<string> GetIngredientNameById(string id)
        {
            var ingredient = await _context.Ingredients.FirstOrDefaultAsync(i => i.Id == id);
            return ingredient.Name;
        }
        public async Task<string> GetIngredientIdByNameAsync(string name)
        {
            var ingredient = await _context.Ingredients.FirstOrDefaultAsync(i => i.Name == name);
            return ingredient.Id;
        }

        public async Task<string> EditIngredienAsync(IngredientDTO ingredientVM)
        {
            ingredientVM.ValidateIfNull("This ingredient doesn't exist!");
            var ingredient = ingredientVM.MapToCtxModel();
            _context.Update(ingredient);
            await _context.SaveChangesAsync();
            return ingredient.Name;
        }

        public async Task<string> DeleteIngredientAsync(string id)
        {
            var ingredient =await _context.Ingredients.FirstOrDefaultAsync(i => i.Id == id && i.DateDeleted == null);
            ingredient.ValidateIfNull("This ingredient doesn't exist!");
            if (_context.CocktailComponent.Select(cc => cc.Ingredient.Id).Contains(id))
            {
                throw new MagicExeption("You cannot delete this ingredient");
            }
            _context.Remove(ingredient);
            await _context.SaveChangesAsync();
            return ingredient.Name;
        }

        public async Task<IngredientDTO> GetIngredientById(string id)
        {
            var ingredient = await _context.Ingredients.FirstOrDefaultAsync(i => i.Id == id && i.DateDeleted == null);
            ingredient.ValidateIfNull("This ingredient doesn't exist!");
            return ingredient.MapToDtoModel();
        }

        public async Task<ICollection<String>> GetAllIngredientsNames()
        {
            var ingredients = await _context.Ingredients
                .Where(i => i.DateDeleted == null)
                .Select(i => i.Name).ToListAsync();
            return ingredients;
        }
        public async Task<List<String>> GetImagesForHpAsync()
        {
            var ingredientImgsForHp = await _context.Ingredients
                                                    .Where(i => i.Id == "10"
                                                     || i.Id == "13" || i.Id == "14"
                                                     || i.Id == "15" || i.Id == "9")
                                                    .Select(i => i.ImageUrl)
                                                    .ToListAsync();
            return ingredientImgsForHp;
        }
    }
}
