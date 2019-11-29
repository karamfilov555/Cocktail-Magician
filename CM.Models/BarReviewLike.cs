using CM.Models.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace CM.Models
{
    public class BarReviewLike
    {
        public string Id { get; set; }
        public DateTime Date { get; set; }
        public string AppUserID {get;set;}
        public AppUser User { get; set; }
        public string BarReviewID { get; set; }
        public BarReview BarReview { get; set; }
        

    }
}
