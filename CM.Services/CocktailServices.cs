using CM.Data;
using CM.DTOs;
using CM.DTOs.Mappers;
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

        public async Task<IEnumerable<CocktailDto>> GetCocktailsForHomePage()
        {
            // include ingredients ... posle ... da se vzima po rating 
            var cocktails = await _context.Cocktails
                                            .Where(c => c.Name.Contains("A") && c.DateDeleted == null)
                                            .ToListAsync()
                                            .ConfigureAwait(false);
            //map to dto before pass to fe
            var cocktailDtos = cocktails.Select(c => c.MapToCocktailDto()).ToList();

            return cocktailDtos;
        }
    }
}

