using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CM.Web.Areas.Reviews.Models
{
    public class AllBarReviewsViewModel
    {
        public string BarId;
        public string BarName;
        public double? Rating;
        public List<BarReviewViewModel> Reviews;
        public List<string> ReviewedByUsers { get; set; }

        public AllBarReviewsViewModel()
        {

        }

    }
}
