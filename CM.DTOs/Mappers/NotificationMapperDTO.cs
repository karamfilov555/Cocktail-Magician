using CM.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CM.DTOs.Mappers
{
    public static class NotificationMapperDTO
    {
        public static NotificationDTO MapNotificationToDTO(this Notification ctxNotification)
        {
            var notificationDTO = new NotificationDTO();
            notificationDTO.Id = ctxNotification.Id;
            notificationDTO.IsSeen = ctxNotification.IsSeen;
            notificationDTO.EventDate = ctxNotification.EventDate;
            notificationDTO.Description = ctxNotification.Description;
            //notificationDTO.User = ctxNotification.User;
            notificationDTO.UserId = ctxNotification.UserId;
            notificationDTO.Username = ctxNotification.Username;
           
            return notificationDTO;
        }
    }
}
