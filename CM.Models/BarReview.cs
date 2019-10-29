using CM.Models.Abstractions;

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
