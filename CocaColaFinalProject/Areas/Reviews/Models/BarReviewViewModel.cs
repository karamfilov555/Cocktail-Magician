using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CM.Web.Areas.Reviews.Models
{
    public class BarReviewViewModel
    {
        public String Id { get; set; }
        public String BarId { get; set; }
        public String BarName { get; set; }
        [Required(ErrorMessage = "Description is required!")]
        [MinLength(5, ErrorMessage = "Description be between 5 and 500 symbols"),
            MaxLength(500, ErrorMessage = "Description must be between 5 and 500 symbols")]
        public String Description { get; set; }
        public int LikeCount { get; set; }
        public List<string> LikedByUsers { get; set; }
        [Required(ErrorMessage = "Rating is required!")]
        public double? Rating { get; set; }
        public string UserName { get; set; }
        public string UserID { get; set; }
        public DateTime ReviewDate { get; set; }
        public BarReviewViewModel()
        {

        }
    }
}
