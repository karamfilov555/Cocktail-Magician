using CM.Services.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CM.Web.Areas.Bars.Models
{
    public class ListBarsViewModel
    {
        public string CurrentSortOrder;
        public string NameSortParm;
        public string RatingSortParm;
        public PaginatedList<BarViewModel> AllBars { get; set; }
    }
}
