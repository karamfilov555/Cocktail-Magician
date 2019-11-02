using CM.Data;
using CM.DTOs;
using CM.DTOs.Mappers;
using CM.Models;
using CM.Services.Common;
using CM.Services.Contracts;
using Microsoft.EntityFrameworkCore;
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
        private readonly IAppUserServices _userServices;

        public ReviewServices(CMContext context,
                              ICocktailServices cocktailServices,
                              IAppUserServices userServices)
        {
            // this dependencies can be removed ... till now
            _context = context;
            _cocktailServices = cocktailServices;
            _userServices = userServices;
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
                Rating = cocktailDto.Rating,
                ReviewDate = DateTime.Now.Date
            };
            _context.CocktailReviews.Add(cocktailReview);
            await _context.SaveChangesAsync().ConfigureAwait(false);
            await this.SetAverrageRating(cocktailDto.Id);

        }
        public async Task SetAverrageRating(string cocktailId)
        {
            var gradesForCocktail = _context.CocktailReviews
                                        .Where(c => c.CocktailId == cocktailId).ToList();
            var avg = gradesForCocktail.Average(c => c.Rating);
            var cocktail = _context.Cocktails.First(c => c.Id == cocktailId);
            cocktail.Rating = avg;
            await _context.SaveChangesAsync();
        }
        public async Task<IDictionary<string, Tuple<string, decimal, DateTime>>> GetReviewsDetailsForCocktial(string cocktailId)
        {
            var reviews = new Dictionary<string, Tuple<string, decimal, DateTime>>();

            var reviewsForCocktail = await _context.CocktailReviews
                                    .Where(r => r.CocktailId == cocktailId)
                                    .ToListAsync();

            foreach (var item in reviewsForCocktail)
            {
                var username = await _userServices.GetUsernameById(item.UserId);

                if (item.Description == null)
                    item.Description = "No description";

                var descriptionWithRating = new Tuple<string, decimal, DateTime>(item.Description, item.Rating, item.ReviewDate);

                reviews.Add(username, descriptionWithRating);
            }

            return reviews;
        }

        public async Task<List<BarReviewDTO>> GetAllReviewsForBar(string id)
        {
            var reviews = await _context
                .BarReviews
                .Include(r => r.User)
                .Where(r => r.BarId == id).ToListAsync();
            var reviewDTOs = reviews.Select(r => r.BarMapReviewToDTO()).ToList();
            return reviewDTOs;
        }

        public async Task CreateBarReview(BarReviewDTO barReviewDTO)
        {
            //validations
            var user = await _context.Users
                .FindAsync(barReviewDTO.UserID);
            var bar = await _context.Bars
                .Include(b=>b.Reviews)
                .Where(b=>b.Id==barReviewDTO.BarId)
                .FirstOrDefaultAsync();
            user.ValidateIfNull();
            bar.ValidateIfNull();
            var barReview = barReviewDTO.MapDTOToReview();
            barReview.UserId = user.Id;
            if (bar.Reviews.Select(r=>r.UserId).ToList().Any(id=>id==user.Id))
            {
                throw new InvalidOperationException("You have already reviewed this bar!");      
            }
            _context.BarReviews.Add(barReview);
            await _context.SaveChangesAsync().ConfigureAwait(false);
            await this.SetAverrageRatingForBar(barReviewDTO.BarId);
        }
        private async Task SetAverrageRatingForBar(string barId)
        {
            var bar = await _context.Bars.FirstOrDefaultAsync(b => b.Id == barId);
            var barRatings = await _context.BarReviews
                                        .Where(r => r.BarId == barId)
                                        .Select(r => r.Rating)
                                        .ToListAsync();
            var avg = barRatings.Average();
            bar.BarRating = avg;
            await _context.SaveChangesAsync();
        }
    }
}
