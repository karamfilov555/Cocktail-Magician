using CM.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace CM.Services.Common
{
    public class NotificationManager : INotificationManager
    {
        private const string cocktailAddedMsg = "New cocktail notification: User: {0}, just added new cocktail with name: \"{1}\"!";

        public string CocktailAddedDescription(string username, string cocktailName)
            => string.Format(cocktailAddedMsg, username, cocktailName);
    }
}
