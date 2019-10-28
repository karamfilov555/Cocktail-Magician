using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CM.Models
{
    public class BarRating
    {
        public BarRating()
        {

        }

        public string Id { get; set; }
        public ICollection<BarReview> BarReviews { get; set; }
        public string BarId { get; set; }
        public Bar Bar { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Rating { get; set; }
    }
}
