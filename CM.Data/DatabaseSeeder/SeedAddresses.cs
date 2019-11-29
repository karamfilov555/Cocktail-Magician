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
    public static class SeedAddresses
    {
        private const string barsDirectory = @"..\CM.Data\JsonRaw\Address.json";
        private const string countriesDirectory = @"..\CM.Data\JsonRaw\Countries.json";

        public static void SeedDatabaseAddresses(this IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                var _context = serviceScope.ServiceProvider.GetService<CMContext>();
                var _jsonManager = serviceScope.ServiceProvider.GetService<IJsonManager>();
                var countCountries = _context.Countries.Count();
                var count = _context.Addresses.Count();
                if (countCountries == 0)
                {
                    _context.Database.Migrate();

                    var countries = _jsonManager.ExtractTypesFromJson<Country>(countriesDirectory);
                    _context.Countries.AddRange(countries);
                    _context.SaveChanges();
                }
                if (count == 0)
                {
                    _context.Database.Migrate();

                    var addresses = _jsonManager.ExtractTypesFromJson<Address>(barsDirectory);
                    _context.Addresses.AddRange(addresses);
                    _context.SaveChanges();
                }
            }
        }
    }
}
