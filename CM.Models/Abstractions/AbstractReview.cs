using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CM.Models.Abstractions
{
    public abstract class AbstractReview
    {
       
        public string Id { get; set; }

        [Range(0, 500)]
        public string Description { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        [Range(1, 5)]
        public decimal Rating { get; set; }
        public string ReviewDate { get; set; }
        public DateTime? DateDeleted { get; set; }
        public string UserId { get; set; }
        public AppUser User { get; set; }

    }
}
