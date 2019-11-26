using CM.Services.Contracts;
using CM.Web.Mappers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NToastNotify;
using System;
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
        [Authorize(Roles = "Manager, Administrator")]
        public async Task<IActionResult> Index()
        {
            try
            {
                string id = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
                var notifications = await _notificationServices.GetNotificationsForUserAsync(id);
                var notificationsVm = notifications.Select(n => n.MapToNotificationVM());
                var notificationsVmSortedByDate = notificationsVm.OrderByDescending(n => n.EventDate);
                return View(notificationsVmSortedByDate);
            }
            catch (Exception ex)
            {
                _toast.AddErrorToastMessage(ex.Message);
                ViewBag.ErrorTitle = "";
                return View("Error");
            }
        }
        [HttpPost]
        [Authorize(Roles = "Manager, Administrator")]
        public async Task<IActionResult> MarkAsSeen(string Id)
        {
            if (Id == null)
            {
                return NotFound();
            }
            try
            {
                var notification = await _notificationServices.MarkAsSeenAsync(Id);
                _toast.AddInfoToastMessage("Message marked as seen.");
                return PartialView("_NotificationSeenPartial", notification.MapToNotificationVM());
                //return ok
            }
            catch (Exception ex)
            {
                _toast.AddErrorToastMessage(ex.Message);
                ViewBag.ErrorTitle = "";
                return View("Error");
            }

        }
        [Authorize(Roles = "Manager, Administrator")]
        public async Task<int> GetNotificationsCount()
        {
            try
            {
            string id = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var notificatonsCount = await _notificationServices.GetUnseenNotificationsCountForUserAsync(id);

            return notificatonsCount;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        [HttpPost]
        public async Task<IActionResult> SendQuickMessage(string name, string email, string message)
        {
            try
            {
            await _notificationServices.CreateNewMessageAsync(name, email, message);
            _toast.AddSuccessToastMessage("You successfully contact our support!");
            return RedirectToAction("Index", "Home");

            }
            catch (Exception ex)
            {
                _toast.AddErrorToastMessage(ex.Message);
                ViewBag.ErrorTitle = "";
                return View("Error");
            }
        }

    }
}