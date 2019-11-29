using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CM.Web.Areas.Search.Models
{
    public class CocktailSearchRestultViewModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public string Ingredients { get; set; }
        public double? Rating { get; set; }
    }
}
