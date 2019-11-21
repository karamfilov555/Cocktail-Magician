using CM.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace CM.Services.Common
{
    public class NotificationManager : INotificationManager
    {
        private const string cocktailAddedMsg = "New Cocktail notification: User: {0}, just added new Cocktail with name: \"{1}\"!";
        private const string cocktailEditMsg = "Cocktail Edit notification: User: {0}, just edited Cocktail with name: \"{1}\" to - new Cocktail's name: \"{2}\"!";
        private const string cocktailEditSameNameMsg = "Cocktail Edit notification: User: {0}, just edited Cocktail with name: \"{1}\"!";
        private const string barAddedMsg = "New Bar notification: User: {0}, just added new Bar with name: \"{1}\"!";
        private const string cocktailDeletedMsg = "Deleted Cocktail notification: User: {0}, just delete a Cocktail with name: \"{1}\"!";
        private const string barDeletedMsg = "Deleted Bar notification: User: {0}, just delete a Bar with name: \"{1}\"!";
        private const string quickMessage = "Quick Message: From: {0}, Descrption:" +
            "\" {1} \", E-mail to replay: {2} ";

        public string CocktailAddedDescription(string username, string cocktailName)
            => string.Format(cocktailAddedMsg, username, cocktailName);
        public string CocktailEditedDescription(string username, string oldCocktailName,string newCocktailName)
           => string.Format(cocktailEditMsg, username, oldCocktailName,newCocktailName);
        public string CocktailEditedSameNameDescription(string username, string oldCocktailName)
          => string.Format(cocktailEditSameNameMsg, username, oldCocktailName);
        public string BarAddedDescription(string username, string barName)
            => string.Format(barAddedMsg, username, barName);
        public string CocktailDeletedDescription(string username, string barName)
           => string.Format(cocktailDeletedMsg, username, barName);
        public string BarDeletedDescription(string username, string barName)
           => string.Format(barDeletedMsg, username, barName);
        public string QuickMessageDescription(string name, string mail, string msg)
            => string.Format(quickMessage, name, msg, mail);
    }
}
