using CM.Models.Abstractions;

namespace CM.Models
{
    public class BarReview : AbstractReview
    {
        public BarReview()
        {

        }
        public string BarId { get; set; }
        public Bar Bar { get; set; }
    }
}
