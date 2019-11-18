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
    public class IngredientServices : IIngredientServices
    {
        private readonly CMContext _context;
        public IngredientServices(CMContext context)
        {
            _context = context;
        }
        public async Task<IList<IngredientDTO>> GetAllIngredients()
        {
            return await _context.Ingredients
                                  .Select(i => i.MapToDtoModel())
                                  .ToListAsync();
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
        public async Task<string> GetIngredientIdByName(string name)
        {
            var ingredient = await _context.Ingredients.FirstOrDefaultAsync(i => i.Name == name);
            return ingredient.Id;
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
            var ingredientImgsForHp = await _context.Ingredients.Where(i =>  i.Id == "7" 
            || i.Id == "9" || i.Id == "13" || i.Id == "15" || i.Id == "17").Select(i=>i.ImageUrl).ToListAsync();
            return ingredientImgsForHp;
        }
    }
}
