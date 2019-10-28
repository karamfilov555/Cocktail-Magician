using CM.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;

namespace CM.Data
{
    public class CMContext : IdentityDbContext<AppUser, AppRole, string>
    {
        public CMContext(DbContextOptions<CMContext> options) : base(options)
        {

        }
        public DbSet<Bar> Bar { get; set; }
        public DbSet<BarReview> BarReviews { get; set; }
       
        public DbSet<BarCocktail> BarCocktails { get; set; }
        public DbSet<Cocktail> Cocktails { get; set; }
        public DbSet<CocktailReview> CocktailReviews { get; set; }
        public DbSet<CocktailIngredient> CocktailIngredients { get; set; }
        public DbSet<Ingredient> Ingredients { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<BarCocktail>().HasKey(hr => new { hr.BarId, hr.CocktailId });
            builder.Entity<CocktailIngredient>().HasKey(rb => new { rb.CocktailId, rb.IngredientId });
            //builder.Entity<Role>().HasKey(r => r.Id);
            //builder.Entity<User>().HasKey(r => r.Id);

            base.OnModelCreating(builder);
        }

    }
}
