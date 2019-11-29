using CM.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CM.Services.Contracts
{
    public interface IRecipeServices
    {
        Task<string> ExtractRecipe(Cocktail cocktail);
    }
}
