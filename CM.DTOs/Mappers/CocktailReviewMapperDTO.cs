using CM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CM.DTOs.Mappers
{
    public static class CocktailReviewMapperDTO
    {
        public static CocktailReviewDTO MapReviewToDTO(this CocktailReview review)
        {
            var newReviewDTO = new CocktailReviewDTO();
            newReviewDTO.Id = review.Id;
            newReviewDTO.BarId = review.CocktailId; //todo
            newReviewDTO.Description = review.Description;
            newReviewDTO.UserName = review.User.UserName;
            newReviewDTO.UserID = review.UserId;
            newReviewDTO.Rating = review.Rating;
            newReviewDTO.LikeCount = review.CocktailReviewLikes.Count;
            newReviewDTO.LikedByUsers = review.CocktailReviewLikes.Select(b => b.AppUserID).ToList();
            return newReviewDTO;
        }
        //public static CocktailReview MapReviewDtoToCtx(this CocktailReviewDTO reviewDto)
        //{
        //    var review = new CocktailReview();
        //    review.Id = reviewDto.Id;
        //    //review.BarId = reviewDto.CocktailId; //todo
        //    review.Description = reviewDto.Description;
        //    review.UserId = reviewDto.UserID;
        //    review.Rating = reviewDto.Rating;
        //    review.CocktailId = reviewDto.CocktailId;
        //    review.ReviewDate = reviewDto.ReviewDate;
        //    review.Rating = reviewDto.Rating;
        //    review.CocktailId = reviewDto.CocktailId;
            
        //    review.CocktailReviewLikes = reviewDto.l;
        //    review.LikedByUsers = reviewDto.CocktailReviewLikes.Select(b => b.AppUserID).ToList();
        //    return newReviewDTO;
        //}
    }
}
