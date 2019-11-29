﻿using CM.Web.Areas.Bars.Models;
using CM.Web.Areas.Cocktails.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CM.Web.Areas.Search.Models
{
    public class SearchViewModel
    {
        public ICollection<BarSearchResultViewModel> Bars { get; set; }
        public ICollection<CocktailSearchRestultViewModel> Cocktails { get; set; }
        public string SearchCriteria { get; set; }
    }
}
