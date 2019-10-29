using CM.Models.Abstractions;

namespace CM.Models
{
    public class CocktailReview:AbstractReview
    {
        public CocktailReview()
        {

        }
        public string UserId { get; set; }
        public AppUser User { get; set; }
        public string CocktailId { get; set; }
        public Cocktail Cocktail { get; set; }
    }
}