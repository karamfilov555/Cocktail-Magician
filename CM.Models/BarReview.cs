using CM.Models.Abstractions;
using System.Collections.Generic;

namespace CM.Models
{
    public class BarReview : BaseReview
    {
        public BarReview()
        {

        }
        public string BarId { get; set; }
        public Bar Bar { get; set; }

        public ICollection<BarReviewLike> BarReviewLikes { get; set; }
    }
}   
