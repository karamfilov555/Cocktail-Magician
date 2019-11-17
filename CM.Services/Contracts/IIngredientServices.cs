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
        Task<IList<IngredientDTO>> GetAllIngredients();
        Task<string> GetIngredientIdByName(string name);
    }
}
