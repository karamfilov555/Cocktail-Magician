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
    public class GetNotificationsForUserAsync_Should
    {
        Mock<IAppUserServices> _userServices;
        Mock<INotificationManager> _iNotificationManager;
        public GetNotificationsForUserAsync_Should()
        {
            _userServices = new Mock<IAppUserServices>();
            _iNotificationManager = new Mock<INotificationManager>();
        }

        [TestMethod]
        public async Task GetAllNotificationsForUser_WhenCalledWithCorrectId()
        {
            var options = TestUtils.GetOptions(nameof(GetAllNotificationsForUser_WhenCalledWithCorrectId));

            var adminName = "pesho";
            var id = "1";
            var description = "new";
            var userName = "gosho";
            var notification = new Notification
            {
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
                var result = await sut.GetNotificationsForUserAsync(id);
                Assert.AreEqual(userName, result.ToList()[0].Username);
                Assert.AreEqual(id, result.ToList()[0].UserId);
                Assert.AreEqual(description, result.ToList()[0].Description);
            }
        }

        [TestMethod]
        public async Task ReturnCorrectType_WhenCalledWithCorrectId()
        {
            var options = TestUtils.GetOptions(nameof(ReturnCorrectType_WhenCalledWithCorrectId));

            var adminName = "pesho";
            var id = "1";
            var description = "new";
            var userName = "gosho";
            var notification = new Notification
            {
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
                var result = await sut.GetNotificationsForUserAsync(id);
                Assert.IsInstanceOfType(result, typeof(List<NotificationDTO>));
               
            }
        }

        [TestMethod]
        public async Task ThrowException_WhenPassedIdIsNull()
        {
            var options = TestUtils.GetOptions(nameof(ThrowException_WhenPassedIdIsNull));

            using (var assertContext = new CMContext(options))
            {
                var sut = new NotificationServices(assertContext, _userServices.Object,
                   _iNotificationManager.Object);
                var ex = await Assert.ThrowsExceptionAsync<MagicExeption>(
                  async () => await sut.GetNotificationsForUserAsync(null));
            }
        }

        [TestMethod]
        public async Task ThrowCorrectMessage_WhenPassedIdIsNull()
        {
            var options = TestUtils.GetOptions(nameof(ThrowCorrectMessage_WhenPassedIdIsNull));

            using (var assertContext = new CMContext(options))
            {
                var sut = new NotificationServices(assertContext, _userServices.Object,
                   _iNotificationManager.Object);
                var ex = await Assert.ThrowsExceptionAsync<MagicExeption>(
                  async () => await sut.GetNotificationsForUserAsync(null));
                Assert.AreEqual(ExeptionMessages.IdNull, ex.Message);
            }
        }
    }
}
