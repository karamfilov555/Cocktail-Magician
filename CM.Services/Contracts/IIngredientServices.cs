using CM.DTOs;
using CM.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CM.Services.Contracts
{
    public interface IIngredientServices
    {
        Task AddIngredient(IngredientDTO ingredientDto);
        Task<string> GetIngredientNameById(string id);
        Task<ICollection<String>> GetAllIngredientsNames();
        Task<List<String>> GetImagesForHpAsync();
        Task<IList<IngredientDTO>> GetTenIngredientsAsync(int currPage);
        Task<string> GetIngredientIdByName(string name);
        Task<int> GetPageCountForIngredientsAsync(int ingredientsPerPage);
        Task<IList<IngredientDTO>> GetAllIngredients();
    }
}
