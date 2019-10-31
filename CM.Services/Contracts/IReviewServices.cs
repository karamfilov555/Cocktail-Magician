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
    }
}
