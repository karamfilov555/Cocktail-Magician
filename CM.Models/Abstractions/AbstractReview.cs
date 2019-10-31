using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CM.Models.Abstractions
{
    public abstract class AbstractReview
    {
        [Range(0, 500)]
        public string Description { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        [Range(1, 5)]
        public decimal Rating { get; set; }
    }
}
