using CM.Data.Configuratons;
using CM.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CM.Data
{
    public class CMContext : IdentityDbContext<AppUser, AppRole, string>
    {
        public CMContext(DbContextOptions<CMContext> options) : base(options)
        {

        }
        public DbSet<Bar> Bars { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<BarCocktail> BarCocktails { get; set; }
        public DbSet<Cocktail> Cocktails { get; set; }
        public DbSet<BarReview> BarReviews { get; set; }
        public DbSet<CocktailReview> CocktailReviews { get; set; }
        public DbSet<BarReviewLike> BarReviewLikes { get; set; }
        public DbSet<CocktailReviewLike> CocktailReviewLikes { get; set; }
        //public DbSet<CocktailIngredient> CocktailIngredients { get; set; }
        public DbSet<CocktailComponent> CocktailComponent { get; set; }
        public DbSet<Ingredient> Ingredients { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new BarCocktailConfiguration());
            builder.ApplyConfiguration(new CocktailIngredientConfiguration());
            builder.ApplyConfiguration(new CocktailReviewConfiguration());
            builder.ApplyConfiguration(new BarReviewConfiguration());
            builder.ApplyConfiguration(new CocktailComponentConfiguration());

            base.OnModelCreating(builder);
        }
    }
}