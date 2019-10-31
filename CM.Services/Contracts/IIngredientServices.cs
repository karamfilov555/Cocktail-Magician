using CM.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CM.Services.Contracts
{
    public interface IIngredientServices
    {
        Task<string> GetIngredientNameById(string id);
        Task<ICollection<Ingredient>> GetAllIngredients();
    }
}
