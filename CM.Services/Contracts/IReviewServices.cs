using CM.DTOs;
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
        Task<List<BarReviewDTO>> GetAllReviewsForBar(string id);

        Task<IDictionary<string, Tuple<string, decimal, string>>> GetReviewsDetailsForCocktial(string cocktailId);
    }
}
