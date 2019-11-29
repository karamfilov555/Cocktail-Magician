﻿using CM.DTOs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CM.Services.Contracts
{
    public interface IReviewServices
    {
        Task<bool> CheckIfUserCanReview(string userId, CocktailDto cocktailDto);
        Task CreateCocktailReview(string userId, CocktailDto cocktailDto);
        Task SetAverrageRating(string cocktailId);
        Task<double?> CreateBarReview(BarReviewDTO barReviewDTO);
        Task<int> LikeBarReview(string barReviewID, string userId);
        Task<int> RemoveBarReviewLike(string barReviewID, string userId);
        Task<List<BarReviewDTO>> GetAllReviewsForBar(string id, int? pageNumber);
        Task<ICollection<CocktailReviewDTO>> GetReviewsForCocktail(string cocktailId);
        Task<int> LikeCocktailReview(string cocktailReviewID, string userId);
        Task<int> RemoveCocktailReviewLike(string cocktailReviewID, string userId);
        Task<ICollection<CocktailReviewDTO>> GetTwoReviewsAsync(string cocktailId, int currPage);

        Task<List<string>> GetUsersWhoReviewedBar(string barID);

        Task<int> GetPageCountForCocktailReviewsAsync(int reviewsPerPage, string Id);
        Task<int> GetTotalReviewsCountForCocktailAsync(string Id);
        Task SetAverrageRatingForBar(string barId);

            //In-memoriam:
            //Task<IDictionary<string, Tuple<string, decimal, DateTime>>> GetReviewsDetailsForCocktial(string cocktailId);
    }
}
