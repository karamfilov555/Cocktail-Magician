using CM.Data;
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
    public class CreateNewMessageAsync_Should
    {
        Mock<IAppUserServices> _userServices;
        Mock<INotificationManager> _iNotificationManager;
        public CreateNewMessageAsync_Should()
        {
            _userServices = new Mock<IAppUserServices>();
            _iNotificationManager = new Mock<INotificationManager>();
        }

        [TestMethod]
        public async Task CallNotificationManagerForMsg_WithCorrectParameters()
        {
            var options = TestUtils.GetOptions(nameof(CallNotificationManagerForMsg_WithCorrectParameters));

            var adminName = "pesho";
            var id = "1";
            var barName = "newBar";

            var admin = new AppUser
            {
                Id = id,
                UserName = adminName
            };
            
            _userServices.Setup(x => x.GetAdmin())
                     .ReturnsAsync(admin);
            _iNotificationManager.Setup(x => x.QuickMessageDescription("name", "email", "msg" ))
                      .Returns("msg");

            using (var arrangeContext = new CMContext(options))
            {
                arrangeContext.Add(admin);
                await arrangeContext.SaveChangesAsync();
                var sut = new NotificationServices(arrangeContext, _userServices.Object,
                   _iNotificationManager.Object);
                await sut.CreateNewMessageAsync("name", "email", "msg");
                _iNotificationManager.Verify(x => x.QuickMessageDescription("name", "email", "msg"), Times.Once());
            }
        }

        [TestMethod]
        public async Task AddNotificationMessageToDB_WithCorrectParameters()
        {
            var options = TestUtils.GetOptions(nameof(AddNotificationMessageToDB_WithCorrectParameters));

            var adminName = "pesho";
            var id = "1";
            var barName = "newBar";

            var admin = new AppUser
            {
                Id = id,
                UserName = adminName
            };

            _userServices.Setup(x => x.GetAdmin())
                     .ReturnsAsync(admin);
            _iNotificationManager.Setup(x => x.QuickMessageDescription(adminName, "email", "msg"))
                      .Returns("msg");

            using (var arrangeContext = new CMContext(options))
            {
                arrangeContext.Add(admin);
                await arrangeContext.SaveChangesAsync();
                var sut = new NotificationServices(arrangeContext, _userServices.Object,
                   _iNotificationManager.Object);
                Assert.AreEqual(0, arrangeContext.Notifications.Count());
                await sut.CreateNewMessageAsync(adminName, "email", "msg");
                Assert.AreEqual(1, arrangeContext.Notifications.Count());
                Assert.AreEqual("msg", arrangeContext.Notifications.First().Description);
                Assert.AreEqual(adminName, arrangeContext.Notifications.First().Username);
            }
        }
        [TestMethod]
        public async Task ThrowException_WhenPassedNameIsNull()
        {
            var options = TestUtils.GetOptions(nameof(ThrowException_WhenPassedNameIsNull));

            using (var assertContext = new CMContext(options))
            {
                var sut = new NotificationServices(assertContext, _userServices.Object,
                   _iNotificationManager.Object);
                var ex = await Assert.ThrowsExceptionAsync<MagicExeption>(
                  async () => await sut.CreateNewMessageAsync(null, "email", "msg"));
            }
        }

        [TestMethod]
        public async Task ThrowCorrectMessage_WhenPassedNameIsNull()
        {
            var options = TestUtils.GetOptions(nameof(ThrowCorrectMessage_WhenPassedNameIsNull));

            using (var assertContext = new CMContext(options))
            {
                var sut = new NotificationServices(assertContext, _userServices.Object,
                   _iNotificationManager.Object);
                var ex = await Assert.ThrowsExceptionAsync<MagicExeption>(
                  async () => await sut.CreateNewMessageAsync(null, "email", "msg"));
                Assert.AreEqual("Name cannot be null", ex.Message);
            }
        }

        [TestMethod]
        public async Task ThrowException_WhenPassedEmailIsNull()
        {
            var options = TestUtils.GetOptions(nameof(ThrowException_WhenPassedEmailIsNull));

            using (var assertContext = new CMContext(options))
            {
                var sut = new NotificationServices(assertContext, _userServices.Object,
                   _iNotificationManager.Object);
                var ex = await Assert.ThrowsExceptionAsync<MagicExeption>(
                  async () => await sut.CreateNewMessageAsync("name", null, "msg"));
            }
        }

        [TestMethod]
        public async Task ThrowCorrectMessage_WhenPassedEmailIsNull()
        {
            var options = TestUtils.GetOptions(nameof(ThrowCorrectMessage_WhenPassedEmailIsNull));

            using (var assertContext = new CMContext(options))
            {
                var sut = new NotificationServices(assertContext, _userServices.Object,
                   _iNotificationManager.Object);
                var ex = await Assert.ThrowsExceptionAsync<MagicExeption>(
                  async () => await sut.CreateNewMessageAsync("name", null, "msg"));
                Assert.AreEqual("Email cannot be null", ex.Message);
            }
        }

        [TestMethod]
        public async Task ThrowException_WhenPassedMsgIsNull()
        {
            var options = TestUtils.GetOptions(nameof(ThrowException_WhenPassedMsgIsNull));

            using (var assertContext = new CMContext(options))
            {
                var sut = new NotificationServices(assertContext, _userServices.Object,
                   _iNotificationManager.Object);
                var ex = await Assert.ThrowsExceptionAsync<MagicExeption>(
                  async () => await sut.CreateNewMessageAsync("name", "email", null));
            }
        }

        [TestMethod]
        public async Task ThrowCorrectMessage_WhenPassedMsgIsNull()
        {
            var options = TestUtils.GetOptions(nameof(ThrowCorrectMessage_WhenPassedMsgIsNull));

            using (var assertContext = new CMContext(options))
            {
                var sut = new NotificationServices(assertContext, _userServices.Object,
                   _iNotificationManager.Object);
                var ex = await Assert.ThrowsExceptionAsync<MagicExeption>(
                  async () => await sut.CreateNewMessageAsync("name", "email", null));
                Assert.AreEqual("Message cannot be null", ex.Message);
            }
        }

        
    }
}
