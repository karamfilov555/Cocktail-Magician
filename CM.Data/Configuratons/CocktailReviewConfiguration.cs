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
            builder.HasKey(r => r.Id);
            //builder.HasOne(r => r.User)
            //    .WithMany(user => user.CocktailReviews)
            //    .HasForeignKey(r => r.UserId);
            builder.HasOne(r => r.Cocktail)
               .WithMany(cocktail => cocktail.Reviews)
               .HasForeignKey(r => r.CocktailId);
        }

    }
}
