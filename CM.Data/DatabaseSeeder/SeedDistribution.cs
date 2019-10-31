using CM.Data.JsonManager;
using CM.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CM.Data.DatabaseSeeder
{
    public static class SeedDistribuion
    {
        private const string distributionDirectory = @"..\CM.Data\JsonRaw\BarCocktails.json";

        public static void SeedDatabaseDistribution(this IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                var _context = serviceScope.ServiceProvider.GetService<CMContext>();
                var _jsonManager = serviceScope.ServiceProvider.GetService<IJsonManager>();
                var count = _context.BarCocktails.Count();
                if (count == 0)
                {
                    _context.Database.Migrate();

                    var distribution = _jsonManager.ExtractTypesFromJson<BarCocktail>(distributionDirectory);
                   
                    _context.BarCocktails.AddRange(distribution);
                    _context.SaveChanges();
                }
            }
        }
    }
}
