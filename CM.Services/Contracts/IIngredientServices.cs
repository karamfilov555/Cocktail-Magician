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
        Task<string> GetIngredientIdByNameAsync(string name);
        Task<int> GetPageCountForIngredientsAsync(int ingredientsPerPage);
        Task<IList<IngredientDTO>> GetAllIngredients();
        Task<IngredientDTO> GetIngredientById(string id);
        Task<string> EditIngredienAsync(IngredientDTO ingredientVM);
        Task<string> DeleteIngredientAsync(string id);

    }
}
