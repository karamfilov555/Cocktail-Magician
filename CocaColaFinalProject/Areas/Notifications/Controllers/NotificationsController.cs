using CM.Models;
using CM.Services.Contracts;
using CM.Web.Mappers;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NToastNotify;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CM.Web.Areas.Notifications.Controllers
{
    [Area("Notifications")]
    public class NotificationsController : Controller
    {
        private readonly INotificationServices _notificationServices;
        private readonly IToastNotification _toast;

        public NotificationsController(INotificationServices notificationService,
                                      IToastNotification toast)
        {
            _notificationServices = notificationService;
            _toast = toast;
        }

        public async Task<IActionResult> Index()
        {
            string id = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var notifications = await _notificationServices.GetNotificationsForUserAsync(id);
            var notificationsVm = notifications.Select(n => n.MapToNotificationVM());
            var notificationsVmSortedByDate = notificationsVm.OrderByDescending(n => n.EventDate);
            return View(notificationsVmSortedByDate);
        }
    }
}