using CM.DTOs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CM.Services.Contracts
{
    public interface ISearchServices
    {
        //Task<SearchResultDTO> GetResultsFromSearch(string searchString);
        Task<ICollection<BarSearchResultDTO>> GetResultsFromBars(string searchString);
        Task<ICollection<CocktailSearchResultDTO>> GetResultsFromCocktails(string searchString);
        Task<ICollection<BarSearchResultDTO>> GetThreeBarResultsSortedByName(string searchString, int? currPage);
        Task<ICollection<CocktailSearchResultDTO>> GetThreeResultsFromCocktails(string searchString, int? currPage);
    }
}
