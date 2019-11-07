using CM.DTOs;
using CM.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CM.Services.Contracts
{
    public interface ICocktailServices
    {
        Task<ICollection<CocktailDto>> GetCocktailsForHomePage();
        Task<CocktailDto> FindCocktailById(string id);
        Task AddCocktail(CocktailDto cocktailDto);
        Task<ICollection<CocktailDto>> GetAllCocktails();
        Task<string> DeleteCocktial(string id);
        Task<string> GetCocktailNameById(string id);
        Task<IList<CocktailDto>> GetFiveSortedCocktailsAsync(string sortOrder,int currPage = 1);
        Task<int> GetPageCountForCocktials(int cocktailsPerPage);
        Task<ICollection<CocktailDto>> GetAllCocktailsByName(string searchCriteria);
        Task<bool> CheckIfCocktailExist(string id);
    }
}
