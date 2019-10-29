namespace CM.Models
{
    public class BarCocktail
    {
        public BarCocktail()
        {

        }
        public string BarId { get; set; }
        public Bar Bar { get; set; }
        public string CocktailId { get; set; }
        public Cocktail Cocktail { get; set; }
    }
}
