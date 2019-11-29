﻿using CM.Data.JsonManager;
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
    public static class SeedBars
    {
        private const string barsDirectory = @"..\CM.Data\JsonRaw\Bars.json";

        public static void SeedDatabaseBars(this IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                var _context = serviceScope.ServiceProvider.GetService<CMContext>();
                var _jsonManager = serviceScope.ServiceProvider.GetService<IJsonManager>();
                var count = _context.Bars.Count();
                if (count == 0)
                {
                    _context.Database.Migrate();

                    var bars = _jsonManager.ExtractTypesFromJson<Bar>(barsDirectory);

                    _context.Bars.AddRange(bars);
                    _context.SaveChanges();
                }
            }
        }
    }
}
