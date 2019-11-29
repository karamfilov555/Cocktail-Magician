﻿using CM.Data;
using CM.DTOs;
using CM.DTOs.Mappers;
using CM.Models;
using CM.Services.Common;
using CM.Services.Contracts;
using CM.Services.CustomExceptions;
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


        public ReviewServices(CMContext context)
        {
            _context = context ?? throw new MagicException(ExceptionMessages.ContextNull);
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
        public async Task<ICollection<CocktailReviewDTO>> GetReviewsForCocktail(string cocktailId)
        => await _context.CocktailReviews
                                    .Where(r => r.CocktailId == cocktailId)
                                    .Include(r => r.User)
                                    .Include(r => r.CocktailReviewLikes)
                                    .Include(r => r.Cocktail)
                                    .Select(r => r.MapReviewToDTO())
                                    .OrderByDescending(r => r.ReviewDate)
                                    .ToListAsync();

        public async Task<ICollection<CocktailReviewDTO>> GetTwoReviewsAsync(string cocktailId, int currPage)
        => await _context.CocktailReviews
                          .Where(r => r.CocktailId == cocktailId
                           && r.DateDeleted == null)
                          .OrderByDescending(r => r.ReviewDate)
                          .Skip((currPage - 1) * 2)
                          .Take(2)
                          .Include(r => r.User)
                          .Include(r => r.CocktailReviewLikes)
                          .Include(r => r.Cocktail)
                          .Select(r => r.MapReviewToDTO())
                          .ToListAsync()
                          .ConfigureAwait(false);


        public async Task<int> GetPageCountForCocktailReviewsAsync(int reviewsPerPage, string Id)
        {
            var allReviewsPerCocktailCount = await _context
                                       .CocktailReviews
                                       .Where(c => c.DateDeleted == null && c.Id == Id)
                                       .CountAsync();

            int pageCount = (allReviewsPerCocktailCount - 1) / reviewsPerPage + 1;

            return pageCount;
        }



        public async Task<List<BarReviewDTO>> GetAllReviewsForBar(string id, int? pageNumber)
        {
            var currentPage = pageNumber ?? 0;
            var reviewsPerPage = 2;
            List<BarReview> reviews = new List<BarReview>();
            if (currentPage == 0)
            {
                reviews = await _context
                                .BarReviews
                                .Include(r => r.User)
                                .Include(r => r.BarReviewLikes)
                                .Include(r => r.Bar)
                                .Where(r => r.BarId == id)
                                .OrderByDescending(r => r.ReviewDate)
                                .Skip(0)
                                .Take(reviewsPerPage)
                                .ToListAsync();
            }
            else
            {
                reviews = await _context
                     .BarReviews
                     .Include(r => r.User)
                     .Include(r => r.BarReviewLikes)
                     .Include(r => r.Bar)
                     .Where(r => r.BarId == id)
                     .OrderByDescending(r=>r.ReviewDate)
                     .Skip((currentPage-1) * reviewsPerPage)
                     .Take(reviewsPerPage)
                     .ToListAsync();
            }
            var reviewDTOs = reviews.Select(r => r.BarMapReviewToDTO()).ToList();
            return reviewDTOs;
        }

        public async Task<List<string>>GetUsersWhoReviewedBar(string barID)
        {
            return await _context.BarReviews.Where(br => br.BarId == barID)
                .Select(br=>br.UserId)
                .ToListAsync();
        }

        /// <summary>
        /// Ot tuk nadolu si ti :)
        /// </summary>
        /// <param name="barReviewDTO"></param>
        /// <returns></returns>
        //Tested 
        public async Task<double?> CreateBarReview(BarReviewDTO barReviewDTO)
        {
            var user = await _context.Users
                .FindAsync(barReviewDTO.UserID);
            user.ValidateIfNull(ExceptionMessages.AppUserNull);
            var bar = await _context.Bars
                .Include(b => b.Reviews)
                .Where(b => b.Id == barReviewDTO.BarId)
                .FirstOrDefaultAsync();
            bar.ValidateIfNull(ExceptionMessages.BarNull);
            var barReview = barReviewDTO.MapDTOToReview();
            if (bar.Reviews.Select(r => r.UserId).ToList().Any(id => id == user.Id))
            {
                throw new MagicException("You have already reviewed this bar!");
            }
            _context.BarReviews.Add(barReview);
            await _context.SaveChangesAsync().ConfigureAwait(false);
            await this.SetAverrageRatingForBar(barReviewDTO.BarId);
            return bar.BarRating;
        }
        //Tested
        public async Task SetAverrageRatingForBar(string barId)
        {
            barId.ValidateIfNull(ExceptionMessages.BarNull);
            var bar = await _context.Bars.FirstOrDefaultAsync(b => b.Id == barId);
            bar.ValidateIfNull(ExceptionMessages.BarNull);
            var barRatings = await _context.BarReviews
                                        .Where(r => r.BarId == barId)
                                        .Select(r => r.Rating)
                                        .ToListAsync();
            var avg = barRatings.Average();
            bar.BarRating = avg;
            await _context.SaveChangesAsync();
        }
        //Tested
        public async Task<int> LikeBarReview(string barReviewID, string userId)
        {
            barReviewID.ValidateIfNull(ExceptionMessages.IdNull);
            userId.ValidateIfNull(ExceptionMessages.IdNull);
            if (await _context.BarReviewLikes.AnyAsync(l => l.AppUserID == userId && l.BarReviewID == barReviewID))
            {
                throw new MagicException(ExceptionMessages.ReviewIsLiked);
            }
            var like = new BarReviewLike()
            {
                AppUserID = userId,
                BarReviewID = barReviewID,
                Date = DateTime.Now.Date
            };
            _context.BarReviewLikes.Add(like);
            await _context.SaveChangesAsync();
            int count = await _context.BarReviewLikes.Where(r => r.BarReviewID == barReviewID).CountAsync();
            return count;
        }
        //Tested
        public async Task<int> RemoveBarReviewLike(string barReviewID, string userId)
        {
            
            barReviewID.ValidateIfNull(ExceptionMessages.IdNull);
            userId.ValidateIfNull(ExceptionMessages.IdNull);
            var like = await _context.BarReviewLikes.FirstOrDefaultAsync(l => l.AppUserID == userId && l.BarReviewID == barReviewID);
            if (like == null)
            {
                throw new MagicException(ExceptionMessages.LikeNull);
            }
            _context.BarReviewLikes.Remove(like);
            await _context.SaveChangesAsync();
            return await _context.BarReviewLikes.Where(r => r.BarReviewID == barReviewID).CountAsync();
        }
        //Tested
        public async Task<int> LikeCocktailReview(string cocktailReviewID, string userId)
        {
            cocktailReviewID.ValidateIfNull(ExceptionMessages.IdNull);
            userId.ValidateIfNull(ExceptionMessages.IdNull);
            if (await _context.CocktailReviewLikes.AnyAsync(l => l.AppUserID == userId && l.CocktailReviewID == cocktailReviewID))
            {
                throw new MagicException(ExceptionMessages.ReviewIsLiked);
            }
            var like = new CocktailReviewLike()
            {
                AppUserID = userId,
                CocktailReviewID = cocktailReviewID,
                Date = DateTime.Now.Date
            };
            _context.CocktailReviewLikes.Add(like);
            await _context.SaveChangesAsync();
            int count = await _context.CocktailReviewLikes
                .Where(r => r.CocktailReviewID == cocktailReviewID).CountAsync();
            return count;
        }

        public async Task<int> RemoveCocktailReviewLike(string cocktailReviewID, string userId)
        {
            cocktailReviewID.ValidateIfNull(ExceptionMessages.IdNull);
            userId.ValidateIfNull(ExceptionMessages.IdNull);
            var like = await _context.CocktailReviewLikes.FirstOrDefaultAsync(l => l.AppUserID == userId && l.CocktailReviewID == cocktailReviewID);
            if (like == null)
            {
                throw new InvalidOperationException("You have not liked this review!");
            }
            _context.CocktailReviewLikes.Remove(like);
            await _context.SaveChangesAsync();
            return await _context.CocktailReviewLikes.Where(r => r.CocktailReviewID == cocktailReviewID).CountAsync();
        }


        public async Task<int> GetTotalReviewsCountForCocktailAsync(string id)
        {
            id.ValidateIfNull(ExceptionMessages.IdNull);
            return await _context
                          .CocktailReviews
                          .Where(c => c.DateDeleted == null && c.CocktailId == id)
                          .CountAsync();
        }

    }
}


