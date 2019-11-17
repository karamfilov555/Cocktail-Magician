using CM.DTOs;
using CM.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CM.Services.Contracts
{
    public interface INotificationServices
    {
        //Task<NotificationDTO> CreateNotificationAsync(string description, string username);
        Task<NotificationDTO> SendNotificationToUserAsync(string description, string username);
        Task<ICollection<NotificationDTO>> GetNotificationsForUserAsync(string userId);
        Task<int> GetUnseenNotificationsCountForUserAsync(string userId);
        Task<NotificationDTO> MarkAsSeenAsync(string notificationId);
        Task BarNotificationToAdminAsync(string id, string entityName);
        Task CocktailNotificationToAdminAsync(string id, string entityName);
        Task CocktailEditNotificationToAdminAsync(string id, string entityName, string newName);
        Task CocktailDeletedNotificationToAdminAsync(string id, string entityName);
        Task BarDeletedNotificationToAdminAsync(string id, string entityName);
    }
}
