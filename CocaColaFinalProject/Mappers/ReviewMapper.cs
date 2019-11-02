using CM.DTOs;
using CM.Web.Areas.Reviews.Models;
using CM.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CM.Web.Mappers
{
    public static class ReviewMapper
    {
        public static BarReviewViewModel MapReviewDTOToVM(this BarReviewDTO reviewDTO)
        {
            var newReviewVM = new BarReviewViewModel();
            newReviewVM.BarId = reviewDTO.BarId;
            newReviewVM.Rating = reviewDTO.Rating;
            newReviewVM.Description = reviewDTO.Description;
            newReviewVM.ReviewDate = reviewDTO.ReviewDate;
            newReviewVM.UserName = reviewDTO.UserName;
            newReviewVM.UserID = reviewDTO.UserID;
            return newReviewVM;
        }

        public static BarReviewDTO MapVMToReviewDTO(this BarReviewViewModel reviewVM)
        {
            var newReview = new BarReviewDTO();
            newReview.BarId = reviewVM.BarId;
            newReview.Rating = reviewVM.Rating;
            newReview.Description = reviewVM.Description;
            newReview.ReviewDate = reviewVM.ReviewDate;
            newReview.UserName = reviewVM.UserName;
            newReview.UserID = reviewVM.UserID;
            return newReview;
        }
    }
}
