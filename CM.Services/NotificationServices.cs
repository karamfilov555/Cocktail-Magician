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
using CM.Services.CustomExeptions;
using CM.Services.Common;

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
            _context = context
                         ?? throw new MagicExeption(ExeptionMessages.ContextNull);
            _userService = userService ?? throw new MagicExeption(ExeptionMessages.IAppUserServiceNull);
            _notificationManager = notificationManager ?? throw new MagicExeption(ExeptionMessages.INotificationManagerNull);
        }
        //Tested
        public async Task<NotificationDTO> CreateNotificationAsync(string description, string username)
        {
            description.ValidateIfNull(ExeptionMessages.DescriptionNull);
            username.ValidateIfNull(ExeptionMessages.UserNameNull);
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
        //Tested
        public async Task<ICollection<NotificationDTO>> GetNotificationsForUserAsync(string userId)
        {
            userId.ValidateIfNull(ExeptionMessages.IdNull);
            var notification = await _context.Notifications
                                             .Where(n => n.UserId == userId)
                                             .ToListAsync().ConfigureAwait(false);
            return notification.Select(n => n.MapNotificationToDTO()).ToList();
        }
        //Tested
        public async Task<int> GetUnseenNotificationsCountForUserAsync(string userId)
        {
            userId.ValidateIfNull(ExeptionMessages.IdNull);
            var notificationsCount = await _context.Notifications
                                                   .Where(n => n.IsSeen == false && n.UserId == userId)
                                                   .CountAsync().ConfigureAwait(false);
            return notificationsCount;
        }
        //Tested
        public async Task<NotificationDTO> MarkAsSeenAsync(string notificationId)
        {
            notificationId.ValidateIfNull(ExeptionMessages.NotificationIdNull);
            var notificationToSee = await _context.Notifications
                                                   .FirstOrDefaultAsync(n => n.Id == notificationId).ConfigureAwait(false);
            notificationToSee.ValidateIfNull(ExeptionMessages.NotificationNull);
            notificationToSee.IsSeen = true;
            await _context.SaveChangesAsync().ConfigureAwait(false);

            return notificationToSee.MapNotificationToDTO();
        }
        //Tested
        public async Task BarCreateNotificationToAdminAsync(string id, string entityName)
        {
            entityName.ValidateIfNull(ExeptionMessages.BarNameNull);
            var username = await _userService.GetUsernameById(id);
            var notificationDescription = _notificationManager.BarAddedDescription(username, entityName);
            var notification = await CreateNotificationAsync(notificationDescription, username);
        }
        //Tested
        public async Task CocktailCreateNotificationToAdminAsync(string id, string entityName)
        {
            entityName.ValidateIfNull(ExeptionMessages.CocktailNameNull);
            var username = await _userService.GetUsernameById(id);
            var notificationDescription = _notificationManager.CocktailAddedDescription(username, entityName);
            var notification = await CreateNotificationAsync(notificationDescription, username);
        }
        //Tested
        public async Task CocktailEditNotificationToAdminAsync(string id, string entityName, string newName)
        {
            entityName.ValidateIfNull(ExeptionMessages.CocktailNameNull);
            newName.ValidateIfNull(ExeptionMessages.CocktailNameNull);
            var username = await _userService.GetUsernameById(id);
            string notificationDescription;

            if (entityName != newName)
                notificationDescription = _notificationManager.CocktailEditedDescription(username, entityName, newName);
            else
                notificationDescription = _notificationManager.CocktailEditedSameNameDescription(username, entityName);

            await CreateNotificationAsync(notificationDescription, username);
        }
        //Tested
        public async Task CocktailDeletedNotificationToAdminAsync(string id, string entityName)
        {
            entityName.ValidateIfNull(ExeptionMessages.CocktailNameNull);
            var username = await _userService.GetUsernameById(id);
            var notificationDescription = _notificationManager.CocktailDeletedDescription(username, entityName);

            await CreateNotificationAsync(notificationDescription, username);
        }
        public async Task BarDeletedNotificationToAdminAsync(string id, string entityName)
        {
            entityName.ValidateIfNull(ExeptionMessages.BarNameNull);
            var username = await _userService.GetUsernameById(id);
            var notificationDescription = _notificationManager.BarDeletedDescription(username, entityName);
            await CreateNotificationAsync(notificationDescription, username);
        }

        public async Task CreateNewMessageAsync(string name, string email, string msg)
        {
            name.ValidateIfNull("Name cannot be null");
            email.ValidateIfNull("Email cannot be null");
            msg.ValidateIfNull("Message cannot be null");

            var notificationDescription = _notificationManager.QuickMessageDescription(name, email, msg);

            await CreateNotificationAsync(notificationDescription, name);
        }
    }
}

//public async Task<NotificationDTO> SendNotificationToUserAsync(string description, string username)
//{
//    description.ValidateIfNull(ExeptionMessages.DescriptionNull);
//    username.ValidateIfNull(ExeptionMessages.UserNameNull);
//    var user = await _userService.GetUserByUsernameAsync(username).ConfigureAwait(false);

//    var notification = new Notification
//    {
//        UserId = user.Id,
//        Description = description,
//        EventDate = DateTime.Now,
//        Username = "System",
//    };

//    _context.Notifications.Add(notification);
//    await _context.SaveChangesAsync().ConfigureAwait(false);
//    return notification.MapNotificationToDTO();
//}