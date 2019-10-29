using CM.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CM.Services.Contracts
{
    public interface ICocktailServices
    {
        Task<IList<Cocktail>> GetCocktailsForHomePage();
    }
}
