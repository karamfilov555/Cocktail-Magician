using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CM.Web.Areas.Bars.Models
{
    public class ListBarsViewModel
    {
        public string NameSortParm;

        public string RatingSortParm;

        public List<BarViewModel> AllBars { get; set; } = new List<BarViewModel>();

    }
}
