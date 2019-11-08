using CM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CM.DTOs.Mappers
{
    public static class CocktailReviewMapperDTO
    {
        public static CocktailReviewDTO BarMapReviewToDTO(this CocktailReview review)
        {
            var newReviewDTO = new CocktailReviewDTO();
            newReviewDTO.Id = review.Id;
            newReviewDTO.BarId = review.CocktailId;
            newReviewDTO.Description = review.Description;
            newReviewDTO.UserName = review.User.UserName;
            newReviewDTO.UserID = review.UserId;
            newReviewDTO.Rating = review.Rating;
            newReviewDTO.LikeCount = review.CocktailReviewLikes.Count;
            newReviewDTO.LikedByUsers = review.CocktailReviewLikes.Select(b => b.AppUserID).ToList();
            return newReviewDTO;
        }

        //public static CocktailReview MapDTOToReview(this CocktailReviewDTO reviewDTO)
        //{
        //    var newReview = new BarReview();
        //    newReview.BarId = reviewDTO.BarId;
        //    newReview.Description = reviewDTO.Description;
        //    newReview.Rating = reviewDTO.Rating;
        //    return newReview;
        //}
    }
}
