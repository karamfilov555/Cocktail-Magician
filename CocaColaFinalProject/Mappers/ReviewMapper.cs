using CM.DTOs;
using CM.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CM.Web.Mappers
{
    public static class ReviewMapper
    {
        public static ReviewViewModel MapReviewDTOToVM(this BarReviewDTO reviewDTO)
        {
            var newReviewVM = new ReviewViewModel();
            newReviewVM.Id = reviewDTO.Id;
            newReviewVM.Rating = reviewDTO.Rating;
            newReviewVM.Description = reviewDTO.Description;
            newReviewVM.ReviewDate = reviewDTO.ReviewDate;
            newReviewVM.UserName = reviewDTO.UserName;
            return newReviewVM;
        }
    }
}
