using CM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CM.DTOs.Mappers
{
    public static class BarReviewMapperDTO
    {
        public static BarReviewDTO BarMapReviewToDTO(this BarReview review)
        {
            var newReviewDTO = new BarReviewDTO();
            newReviewDTO.Id = review.Id;
            newReviewDTO.BarId = review.BarId;
            newReviewDTO.Description = review.Description;
            newReviewDTO.UserName = review.User.UserName;
            newReviewDTO.BarName = review.Bar.Name;
            newReviewDTO.UserID = review.UserId;
            newReviewDTO.Rating = review.Rating;
            newReviewDTO.ReviewDate = review.ReviewDate;
            newReviewDTO.LikeCount = review.BarReviewLikes.Count;
            newReviewDTO.LikedByUsers = review.BarReviewLikes.Select(b => b.AppUserID).ToList();
            return newReviewDTO;
        }

        public static BarReview MapDTOToReview(this BarReviewDTO reviewDTO)
        {
            var newReview = new BarReview();
            newReview.BarId = reviewDTO.BarId;
            newReview.Description = reviewDTO.Description;
            newReview.Rating = reviewDTO.Rating;
            newReview.UserId = reviewDTO.UserID;
            newReview.ReviewDate = DateTime.Now;
            return newReview;
        }

    }
}
