using CM.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CM.DTOs.Mappers
{
    public static class ReviewMapperDTO
    {
        public static BarReviewDTO MapReviewToDTO(this Review review)
        {
            var newReviewDTO = new BarReviewDTO();
            newReviewDTO.Id = review.Id;
            newReviewDTO.Description = review.Description;
            newReviewDTO.UserName = review.User.UserName;
            newReviewDTO.Rating = review.Rating;
            return newReviewDTO;
        }
    }
}
