using CM.DTOs;
using CM.Web.Areas.Reviews.Models;
using CM.Web.Areas.Cocktails.Models;
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
            newReviewVM.Id = reviewDTO.Id;
            newReviewVM.BarId = reviewDTO.BarId;
            newReviewVM.Rating = reviewDTO.Rating;
            newReviewVM.Description = reviewDTO.Description;
            newReviewVM.ReviewDate = reviewDTO.ReviewDate;
            newReviewVM.UserName = reviewDTO.UserName;
            newReviewVM.BarName = reviewDTO.BarName;
            newReviewVM.UserID = reviewDTO.UserID;
            newReviewVM.LikeCount = reviewDTO.LikeCount;
            newReviewVM.LikedByUsers = reviewDTO.LikedByUsers;
            return newReviewVM;
        }

        public static BarReviewDTO MapVMToReviewDTO(this BarReviewViewModel reviewVM)
        {
            var newReview = new BarReviewDTO();
            newReview.BarId = reviewVM.BarId;
            newReview.Rating = reviewVM.Rating;
            newReview.Description = reviewVM.Description;
            //newReview.ReviewDate = reviewVM.ReviewDate;
            newReview.UserName = reviewVM.UserName;
            newReview.UserID = reviewVM.UserID;
            return newReview;
        }

        public static CocktailReviewViewModel MapToViewModel(this CocktailReviewDTO reviewDTO)
        {
            var reviewVm = new CocktailReviewViewModel();
            reviewVm.Id= reviewDTO.Id;
            reviewVm.CocktailID = reviewDTO.CocktailID;
            reviewVm.Description = reviewDTO.Description;
            reviewVm.LikeCount = reviewDTO.LikeCount;
            reviewVm.LikedByUsers = reviewDTO.LikedByUsers;
            reviewVm.Rating = reviewDTO.Rating;
            reviewVm.UserName = reviewDTO.UserName;
            reviewVm.ReviewDate = reviewDTO.ReviewDate;
            reviewVm.UserId = reviewDTO.UserID;
            reviewVm.ReviewDate = reviewDTO.ReviewDate;
            return reviewVm;
        }


    }
}
