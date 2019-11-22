using CM.DTOs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CM.Services.Contracts
{
    public interface ISearchServices
    {
        Task<SearchResultDTO> GetResultsFromSearch(string searchString);
        Task<List<BarSearchResultDTO>> GetResultsFromBars(string searchString);
        Task<List<CocktailSearchResultDTO>> GetResultsFromCocktails(string searchString);
    }
}
