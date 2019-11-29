using CM.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace CM.Data.Configuratons
{
    public class CocktailComponentConfiguration : IEntityTypeConfiguration<CocktailComponent>
    {

        public void Configure(EntityTypeBuilder<CocktailComponent> builder)
        {
            builder
                 .Property(c => c.Unit)
                 .IsRequired()
                 .HasConversion(u => u.ToString(), u => (Unit)Enum.Parse(typeof(Unit), u));
        }

    }
}
