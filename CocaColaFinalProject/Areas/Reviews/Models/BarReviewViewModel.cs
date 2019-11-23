using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CM.Web.Areas.Reviews.Models
{
    public class BarReviewViewModel
    {
        public String Id { get; set; }
        public String BarId { get; set; }
        public String BarName { get; set; }
        public String Description { get; set; }
        public int LikeCount { get; set; }
        public List<string> LikedByUsers { get; set; }
        public double? Rating { get; set; }
        public string UserName { get; set; }
        public string UserID { get; set; }
        public DateTime ReviewDate { get; set; }
        public BarReviewViewModel()
        {

        }
    }
}
