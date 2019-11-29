using CM.Data;
using CM.Models;
using CM.Services.Contracts;
using CM.Services.CustomExceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CM.Services.Tests.NotificationServiceTests
{
    [TestClass]
    public class GetUnseenNotificationsCountForUserAsync_Should
    {
        Mock<IAppUserServices> _userServices;
        Mock<INotificationManager> _iNotificationManager;
        public GetUnseenNotificationsCountForUserAsync_Should()
        {
            _userServices = new Mock<IAppUserServices>();
            _iNotificationManager = new Mock<INotificationManager>();
        }

        [TestMethod]
        public async Task GetNumberOfUnseenNotificationsForUser_WhenCalledWithCorrectId()
        {
            var options = TestUtils.GetOptions(nameof(GetNumberOfUnseenNotificationsForUser_WhenCalledWithCorrectId));

            var adminName = "pesho";
            var id = "1";
            var description = "new";
            var userName = "gosho";
            var notification = new Notification
            {
                Description = description,
                Username = userName,
                UserId = "1", 
                IsSeen=false 
            };
            var notification2 = new Notification
            {
                Description = "new",
                Username = userName,
                UserId = "1",
                IsSeen = false
            };
            var notification3 = new Notification
            {
                Description = "new",
                Username = userName,
                UserId = "1",
                IsSeen = true
            };

            var admin = new AppUser
            {
                Id = id,
                UserName = adminName
            };
            _userServices.Setup(x => x.GetAdmin())
                      .ReturnsAsync(admin);

            using (var arrangeContext = new CMContext(options))
            {

                arrangeContext.Add(admin);
                arrangeContext.Add(notification);
                arrangeContext.Add(notification2);
                arrangeContext.Add(notification3);
                await arrangeContext.SaveChangesAsync();
                var sut = new NotificationServices(arrangeContext, _userServices.Object,
                   _iNotificationManager.Object);
                var result = await sut.GetUnseenNotificationsCountForUserAsync(id);
                Assert.AreEqual(2, result);            }
        }

        [TestMethod]
        public async Task ThrowExceptionForCount_WhenPassedIdIsNull()
        {
            var options = TestUtils.GetOptions(nameof(ThrowExceptionForCount_WhenPassedIdIsNull));

            using (var assertContext = new CMContext(options))
            {
                var sut = new NotificationServices(assertContext, _userServices.Object,
                   _iNotificationManager.Object);
                var ex = await Assert.ThrowsExceptionAsync<MagicException>(
                  async () => await sut.GetUnseenNotificationsCountForUserAsync(null));
            }
        }

        [TestMethod]
        public async Task ThrowCorrectMessageForCount_WhenPassedIdIsNull()
        {
            var options = TestUtils.GetOptions(nameof(ThrowCorrectMessageForCount_WhenPassedIdIsNull));

            using (var assertContext = new CMContext(options))
            {
                var sut = new NotificationServices(assertContext, _userServices.Object,
                   _iNotificationManager.Object);
                var ex = await Assert.ThrowsExceptionAsync<MagicException>(
                  async () => await sut.GetNotificationsForUserAsync(null));
                Assert.AreEqual(ExceptionMessages.IdNull, ex.Message);
            }
        }
    }
}
