using CM.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CM.DTOs.Mappers
{
    public static class BarReviewMapperDTO
    {
        public static BarReviewDTO BarMapReviewToDTO(this BarReview review)
        {
            var newReviewDTO = new BarReviewDTO();
            newReviewDTO.BarId = review.Id;
            newReviewDTO.Description = review.Description;
            newReviewDTO.UserName = review.User.UserName;
            newReviewDTO.UserID = review.UserId;
            newReviewDTO.Rating = review.Rating;
            return newReviewDTO;
        }

        public static BarReview MapDTOToReview(this BarReviewDTO reviewDTO)
        {
            var newReview = new BarReview();
            newReview.Id = reviewDTO.BarId;
            newReview.Description = reviewDTO.Description;
            newReview.Rating = reviewDTO.Rating;
            return newReview;
        }

    }
}
