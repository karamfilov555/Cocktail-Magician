using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CM.Models.Abstractions
{
   public abstract class AbstractReview
    {
        [Key]
        public string Id { get; set; }
        [Range(0, 500)]
        public string Description { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        [Range(1, 5)]
        public decimal Rating { get; set; }
    }
}
