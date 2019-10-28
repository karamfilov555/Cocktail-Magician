using CM.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace CM.Data.Configuratons
{
    public class CocktailReviewConfiguration : IEntityTypeConfiguration<CocktailReview>
    {

        public void Configure(EntityTypeBuilder<CocktailReview> builder)
        {
            builder
                     .HasKey(cr => new {
                         cr.UserId,
                         cr.CocktailId
                     });

        }

    }
}
