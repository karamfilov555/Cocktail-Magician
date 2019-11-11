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
        Task<NotificationDTO> CreateNotificationAsync(string description, string username);
        Task<NotificationDTO> SendNotificationToUserAsync(string description, string username);
        Task<ICollection<NotificationDTO>> GetNotificationsForUserAsync(string userId);
        Task<int> GetNotificationsCountForUserAsync(string userId);
        Task<NotificationDTO> MarkAsSeenAsync(string notificationId);

    }
}
