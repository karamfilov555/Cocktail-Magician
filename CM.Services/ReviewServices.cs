using CM.Data;
using CM.DTOs;
using CM.Models;
using CM.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CM.Services
{
    public class ReviewServices : IReviewServices
    {
        private readonly CMContext _context;
        private readonly ICocktailServices _cocktailServices;

        public ReviewServices(CMContext context,
                              ICocktailServices cocktailServices)
        {
            // this dependencies can be removed ... till now
            _context = context;
            _cocktailServices = cocktailServices;
        }

        public async Task<bool> CheckIfUserCanReview(string userId, CocktailDto cocktailDto)
        => cocktailDto.CocktailReviews.Any(c => c.UserId == userId && c.CocktailId == cocktailDto.Id);

        public async Task CreateCocktailReview(string userId, CocktailDto cocktailDto)
        {
            //validations

            var cocktailReview = new CocktailReview
            {
                UserId = userId,
                CocktailId = cocktailDto.Id,
                Description = cocktailDto.Description,
                Rating = cocktailDto.Rating
            };
            _context.CocktailReviews.Add(cocktailReview);
            await _context.SaveChangesAsync().ConfigureAwait(false);

        }
    }
}
