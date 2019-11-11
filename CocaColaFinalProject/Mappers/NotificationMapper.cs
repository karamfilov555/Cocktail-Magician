using CM.DTOs;
using CM.Web.Areas.Notifications.Models;
using System.Linq;

namespace CM.Web.Mappers
{
    public static class NotificationMapper
    {
        public static NotificationViewModel MapToNotificationVM(this NotificationDTO notificationDto)
        {
            var notficationVm = new NotificationViewModel();
            notficationVm.Id = notificationDto.Id;
            notficationVm.Username = notificationDto.Username;
            notficationVm.Description = notificationDto.Description;
            notficationVm.UserId = notificationDto.UserId;
            notficationVm.IsSeen = notificationDto.IsSeen;
            notficationVm.EventDate = notificationDto.EventDate;

            return notficationVm;
        }
    }
}
