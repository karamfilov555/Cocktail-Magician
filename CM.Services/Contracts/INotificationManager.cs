using System;
using System.Collections.Generic;
using System.Text;

namespace CM.Services.Contracts
{
    public interface INotificationManager
    {
        string CocktailAddedDescription(string username, string cocktailName);
        string BarAddedDescription(string username, string barName);
        string CocktailEditedDescription(string username, string oldCocktailName, string newCocktailName);
        string CocktailEditedSameNameDescription(string username, string oldCocktailName);
        string CocktailDeletedDescription(string username, string barName);
        string BarDeletedDescription(string username, string barName);
    }
}
