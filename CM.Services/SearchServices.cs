﻿using CM.Data;
using CM.DTOs;
using CM.DTOs.Mappers;
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
    public class SearchServices : ISearchServices
    {
        private readonly CMContext _context;
        private readonly IBarServices _barService;
        private readonly ICocktailServices _cocktailService;

        public SearchServices(CMContext context,
                              IBarServices barService,
                              ICocktailServices cocktailService)
        {
            _context = context?? throw new MagicException(ExceptionMessages.ContextNull);
            _barService = barService ?? throw new MagicException(ExceptionMessages.IBarServiceNull); ;
            _cocktailService = cocktailService ?? throw new MagicException(ExceptionMessages.ICocktailServiceNull); ;
        }

        //public async Task<SearchResultDTO> GetResultsFromSearch(string searchString)
        //{
        //    var searchDto = new SearchResultDTO();
        //    searchDto.Bars = await _barService.GetAllBarsByName(searchString);
        //    searchDto.Cocktails = await _cocktailService.GetAllCocktailsByName(searchString);
        //    return searchDto;
        //}

        public async Task<ICollection<BarSearchResultDTO>> GetResultsFromBars(string searchString)
        {
            var namesResult = _context.Bars
                 .Include(b => b.Address)
                .ThenInclude(a => a.Country)
                .Where(b => b.Name.Contains(searchString));
            var ratingResult = _context.Bars
                .Include(b => b.Address)
                .ThenInclude(a => a.Country)
                .Where(b => b.BarRating.ToString() == searchString);
            var addressResult = _context.Bars
                .Include(b => b.Address)
                .ThenInclude(a => a.Country)
                .Where(b => b.Address.Country.Name.Contains(searchString)
                || b.Address.City.Contains(searchString)
                || b.Address.Details.Contains(searchString));
            var finalResult = namesResult.Union(ratingResult);
            finalResult = finalResult.Union(addressResult);
            var resultList = await finalResult.Select(b => b.MapBarToSearchDTO()).ToListAsync();
            return resultList;
        }


        public async Task<ICollection<CocktailSearchResultDTO>> GetResultsFromCocktails(string searchString)
        {
            var namesResult = _context.Cocktails
                 .Include(c => c.CocktailComponents)
                 .ThenInclude(cc=>cc.Ingredient)
                .Where(c => c.Name.Contains(searchString));
            var ratingResult = _context.Cocktails
                 .Include(c => c.CocktailComponents)
                .ThenInclude(cc => cc.Ingredient)
                .Where(c => c.Rating.ToString() == searchString);
            var ingredientResult = _context.Cocktails
                .Include(c => c.CocktailComponents)
                .ThenInclude(cc => cc.Ingredient)
               .Where(c => c.CocktailComponents.Select(ccc=>ccc.Ingredient.Name).Any(ccc => ccc.Contains(searchString)));
            var finalResult = namesResult.Union(ratingResult);
            finalResult = finalResult.Union(ingredientResult);
            var resultList = await finalResult.Select(c => c.MapCocktailToCocktailSearchResult()).ToListAsync();
            return resultList;
        }

        public async Task<ICollection<BarSearchResultDTO>> GetThreeBarResultsSortedByName(string searchString, int? currPage=1 )
        {
            
            var namesResult = _context.Bars
                 .Include(b => b.Address)
                .ThenInclude(a => a.Country)
                .Where(b => b.Name.Contains(searchString));
            var ratingResult = _context.Bars
                .Include(b => b.Address)
                .ThenInclude(a => a.Country)
                .Where(b => b.BarRating.ToString() == searchString);
            var addressResult = _context.Bars
                .Include(b => b.Address)
                .ThenInclude(a => a.Country)
                .Where(b => b.Address.Country.Name.Contains(searchString)
                || b.Address.City.Contains(searchString)
                || b.Address.Details.Contains(searchString));
            var finalResult = namesResult.Union(ratingResult);
            finalResult = finalResult.Union(addressResult);

            var results = await finalResult
                                        .OrderBy(b => b.Name)
                                        .Skip(((currPage??1) - 1) * 3)
                                        .Take(3)
                                        .Select(b => b.MapBarToSearchDTO())
                                        .ToListAsync();
            return results;
        }

        public async Task<ICollection<CocktailSearchResultDTO>> GetThreeResultsFromCocktails(string searchString, int? currPage )
        {
            var namesResult = _context.Cocktails
                 .Include(c => c.CocktailComponents)
                 .ThenInclude(cc => cc.Ingredient)
                .Where(c => c.Name.Contains(searchString));
            var ratingResult = _context.Cocktails
                 .Include(c => c.CocktailComponents)
                .ThenInclude(cc => cc.Ingredient)
                .Where(c => c.Rating.ToString() == searchString);
            var ingredientResult = _context.Cocktails
                .Include(c => c.CocktailComponents)
                .ThenInclude(cc => cc.Ingredient)
               .Where(c => c.CocktailComponents.Select(ccc => ccc.Ingredient.Name).Any(ccc => ccc.Contains(searchString)));
            var finalResult = namesResult.Union(ratingResult);
            finalResult = finalResult.Union(ingredientResult);

            var results = await finalResult
                                        .OrderBy(c => c.Name)
                                        .Skip((currPage??1 - 1) * 3)
                                        .Take(3)
                                        .Select(c => c.MapCocktailToCocktailSearchResult())
                                        .ToListAsync();
            return results;
        }
    }
}
