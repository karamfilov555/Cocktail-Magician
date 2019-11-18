using CM.Data;
using CM.DTOs;
using CM.Models;
using CM.Services.Contracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CM.DTOs.Mappers;

namespace CM.Services
{
    public class NotificationServices : INotificationServices
    {
        private readonly CMContext _context;
        private readonly IAppUserServices _userService;
        private readonly INotificationManager _notificationManager;

        public NotificationServices(CMContext context,
                                   IAppUserServices userService,
                                   INotificationManager notificationManager)
        {
            _context = context;
            _userService = userService;
            _notificationManager = notificationManager;
        }
        private async Task<NotificationDTO> CreateNotificationAsync(string description, string username)
        {
            //notifications for admin
            var admin = await _userService.GetAdmin().ConfigureAwait(false);

            var notification = new Notification
            {
                UserId = admin.Id,
                Description = description,
                EventDate = DateTime.Now,
                Username = username,
            };

            _context.Notifications.Add(notification);
            await _context.SaveChangesAsync().ConfigureAwait(false);
            return notification.MapNotificationToDTO();
        }
        public async Task<NotificationDTO> SendNotificationToUserAsync(string description, string username)
        {
            var user = await _userService.GetUserByUsernameAsync(username).ConfigureAwait(false);
            var notification = new Notification
            {
                UserId = user.Id,
                Description = description,
                EventDate = DateTime.Now,
                Username = "System",
            };

            _context.Notifications.Add(notification);
            await _context.SaveChangesAsync().ConfigureAwait(false);
            return notification.MapNotificationToDTO();
        }
        public async Task<ICollection<NotificationDTO>> GetNotificationsForUserAsync(string userId)
        {
            var notification = await _context.Notifications
                                             .Where(n => n.UserId == userId)
                                             .ToListAsync().ConfigureAwait(false);
            return notification.Select(n => n.MapNotificationToDTO()).ToList();
        }

        public async Task<int> GetUnseenNotificationsCountForUserAsync(string userId)
        {
            var notificationsCount = await _context.Notifications
                                                   .Where(n => n.IsSeen == false && n.UserId == userId)
                                                   .CountAsync().ConfigureAwait(false);
            return notificationsCount;
        }
        public async Task<NotificationDTO> MarkAsSeenAsync(string notificationId)
        {
            var notificationToSee = await _context.Notifications
                                                   .FirstAsync(n => n.Id == notificationId).ConfigureAwait(false);
            notificationToSee.IsSeen = true;
            await _context.SaveChangesAsync().ConfigureAwait(false);

            return notificationToSee.MapNotificationToDTO();
        }
        public async Task BarNotificationToAdminAsync(string id, string entityName)
        {
            var username = await _userService.GetUsernameById(id);
            var notificationDescription = _notificationManager.BarAddedDescription(username, entityName);
            var notification = await CreateNotificationAsync(notificationDescription, username);
        }
        public async Task CocktailNotificationToAdminAsync(string id, string entityName)
        {
            var username = await _userService.GetUsernameById(id);
            var notificationDescription = _notificationManager.CocktailAddedDescription(username, entityName);
            var notification = await CreateNotificationAsync(notificationDescription, username);
        }
        public async Task CocktailEditNotificationToAdminAsync(string id, string entityName, string newName)
        {
            var username = await _userService.GetUsernameById(id);
            string notificationDescription;

            if (entityName != newName)
                notificationDescription = _notificationManager.CocktailEditedDescription(username, entityName, newName);
            else
                notificationDescription = _notificationManager.CocktailEditedSameNameDescription(username, entityName);

                await CreateNotificationAsync(notificationDescription, username);
        }
        public async Task CocktailDeletedNotificationToAdminAsync(string id, string entityName)
        {
            var username = await _userService.GetUsernameById(id);
            var notificationDescription = _notificationManager.CocktailDeletedDescription(username, entityName);

            await CreateNotificationAsync(notificationDescription, username);
        }
        public async Task BarDeletedNotificationToAdminAsync(string id, string entityName)
        {
            var username = await _userService.GetUsernameById(id);
            var notificationDescription = _notificationManager.BarDeletedDescription(username, entityName);
            await CreateNotificationAsync(notificationDescription, username);
        }
    }
}
