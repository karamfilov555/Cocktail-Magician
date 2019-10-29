using CM.Data;
using CM.Models;
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

        public async Task<IList<Cocktail>> GetCocktailsForHomePage()
        {
            // include ingredients ... posle ... da se vzima po rating 
            var cocktails = await _context.Cocktails
                                            .Where(c => c.Name.Contains("A") && c.DateDeleted == null)
                                            .ToListAsync()
                                            .ConfigureAwait(false);
            return cocktails;
        }
    }
}

