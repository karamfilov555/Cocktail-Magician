using CM.Models.Abstractions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CM.Models
{
    public class BarReview : AbstractReview
    {
        public BarReview()
        {

        }
        public string UserId { get; set; }
        public AppUser User { get; set; }
        public string BarId { get; set; }
        public Bar Bar { get; set; }
    }
}
