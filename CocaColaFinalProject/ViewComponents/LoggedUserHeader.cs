using CM.Services.Contracts;
using CM.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CM.Web.ViewComponents
{
    public class LoggedUserHeader : ViewComponent
    {
        private readonly INotificationServices _notificationServices;
        private readonly IAppUserServices _appUserServices;

        public string ImageURL { get; set; }
        public int NotificationsCount { get; set; }

        public LoggedUserHeader(INotificationServices notificationServices,
            IAppUserServices appUserServices)
        {
            _notificationServices = notificationServices;
            _appUserServices = appUserServices;
        }

        public async Task<IViewComponentResult> InvokeAsync(string userID)
        {
            var pictureURL = await _appUserServices.GetProfilePictureURL(userID);
            var notificationsCount = await _notificationServices.GetUnseenNotificationsCountForUserAsync(userID);
            var headerVM = new HeaderViewModel { ImageURL = pictureURL, NotificationsCount = notificationsCount };
            return View(headerVM);
        }
    }
}
