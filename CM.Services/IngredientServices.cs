using CM.Data;
using CM.Services.Contracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
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
        public async Task<string> GetIngredientNameById(string id)
        {
            var ingredient = await _context.Ingredients.FirstOrDefaultAsync(i => i.Id == id);
            return ingredient.Name;
        }
    }
}
