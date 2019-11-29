using CM.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace CM.Data.Configuratons
{
    class BarCocktailConfiguration : IEntityTypeConfiguration<BarCocktail>
    {
        public void Configure(EntityTypeBuilder<BarCocktail> builder)
        {
            //many-to-many 
            builder
                .HasKey(bc => new { bc.BarId, bc.CocktailId });
           
        }
    }
}
 