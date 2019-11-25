using CM.Data;
using CM.DTOs;
using CM.Models;
using CM.Services.Contracts;
using CM.Services.CustomExeptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CM.Services.Tests.NotificationServiceTests
{
    [TestClass]
    public class MarkAsSeenAsync_Should
    {
        Mock<IAppUserServices> _userServices;
        Mock<INotificationManager> _iNotificationManager;
        public MarkAsSeenAsync_Should()
        {
            _userServices = new Mock<IAppUserServices>();
            _iNotificationManager = new Mock<INotificationManager>();
        }

        [TestMethod]
        public async Task ThrowException_WhenPassedNotificationIdIsNull()
        {
            var options = TestUtils.GetOptions(nameof(ThrowException_WhenPassedNotificationIdIsNull));

            using (var assertContext = new CMContext(options))
            {
                var sut = new NotificationServices(assertContext, _userServices.Object,
                   _iNotificationManager.Object);
                var ex = await Assert.ThrowsExceptionAsync<MagicExeption>(
                  async () => await sut.MarkAsSeenAsync(null));
            }
        }

        [TestMethod]
        public async Task ThrowCorrectMessage_WhenPassedNotificationIdIsNull()
        {
            var options = TestUtils.GetOptions(nameof(ThrowCorrectMessage_WhenPassedNotificationIdIsNull));

            using (var assertContext = new CMContext(options))
            {
                var sut = new NotificationServices(assertContext, _userServices.Object,
                   _iNotificationManager.Object);
                var ex = await Assert.ThrowsExceptionAsync<MagicExeption>(
                  async () => await sut.MarkAsSeenAsync(null));
                Assert.AreEqual(ExeptionMessages.NotificationIdNull, ex.Message);
            }
        }

        [TestMethod]
        public async Task ThrowException_WhenNotificationIsNull()
        {
            var options = TestUtils.GetOptions(nameof(ThrowException_WhenNotificationIsNull));

            using (var assertContext = new CMContext(options))
            {
                var sut = new NotificationServices(assertContext, _userServices.Object,
                   _iNotificationManager.Object);
                var ex = await Assert.ThrowsExceptionAsync<MagicExeption>(
                  async () => await sut.MarkAsSeenAsync("15"));
            }
        }

        [TestMethod]
        public async Task ThrowCorrectMessage_WhenNotificationIsNull()
        {
            var options = TestUtils.GetOptions(nameof(ThrowCorrectMessage_WhenNotificationIsNull));

            using (var assertContext = new CMContext(options))
            {
                var sut = new NotificationServices(assertContext, _userServices.Object,
                   _iNotificationManager.Object);
                var ex = await Assert.ThrowsExceptionAsync<MagicExeption>(
                  async () => await sut.MarkAsSeenAsync("15"));
                Assert.AreEqual(ExeptionMessages.NotificationNull, ex.Message);
            }
        }

        [TestMethod]
        public async Task ReturnCorrectObjectType_WhenCalledWithCorrectId()
        {
            var options = TestUtils.GetOptions(nameof(ReturnCorrectObjectType_WhenCalledWithCorrectId));

            var adminName = "pesho";
            var id = "1";
            var description = "new";
            var userName = "gosho";
            var notificationID = "15";
            var notification = new Notification
            {
                Id= notificationID,
                Description = description,
                Username = userName,
                UserId = "1"
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
                await arrangeContext.SaveChangesAsync();
                var sut = new NotificationServices(arrangeContext, _userServices.Object,
                   _iNotificationManager.Object);
                var result = await sut.MarkAsSeenAsync(notificationID);
                Assert.IsInstanceOfType(result, typeof(NotificationDTO));
                
            }
        }

        [TestMethod]
        public async Task MarkNotificationASeen_WhenCalledWithCorrectId()
        {
            var options = TestUtils.GetOptions(nameof(MarkNotificationASeen_WhenCalledWithCorrectId));
            var notificationID = "15";
            var adminName = "pesho";
            var id = "1";
            var description = "new";
            var userName = "gosho";
            var notification = new Notification
            {
                Id=notificationID,
                Description = description,
                Username = userName,
                UserId = "1", 
                IsSeen=false
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
                await arrangeContext.SaveChangesAsync();
                Assert.AreEqual(false, arrangeContext.Notifications.First().IsSeen);
                var sut = new NotificationServices(arrangeContext, _userServices.Object,
                   _iNotificationManager.Object);
                var result = await sut.MarkAsSeenAsync(notificationID);
                Assert.AreEqual(true, result.IsSeen);
            }
        }
    }
}
