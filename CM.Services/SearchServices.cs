using CM.Data;
using CM.DTOs;
using CM.Services.Contracts;
using System;
using System.Collections.Generic;
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
            _context = context;
            _barService = barService;
            _cocktailService = cocktailService;
        }

        public async Task<SearchResultDTO> GetResultsFromSearch(string searchString)
        {
            var searchDto = new SearchResultDTO();
            searchDto.Bars = await _barService.GetAllBarsByName(searchString);
            searchDto.Cocktails = await _cocktailService.GetAllCocktailsByName(searchString);
            return searchDto;
        }
    }
}
