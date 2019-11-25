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
    public class BarDeletedNotificationToAdminAsync_Should
    {
        Mock<IAppUserServices> _userServices;
        Mock<INotificationManager> _iNotificationManager;
        public BarDeletedNotificationToAdminAsync_Should()
        {
            _userServices = new Mock<IAppUserServices>();
            _iNotificationManager = new Mock<INotificationManager>();
        }

        [TestMethod]
        public async Task CallUserManagerForDeleteBar_WithCorrectParameters()
        {
            var options = TestUtils.GetOptions(nameof(CallUserManagerForDeleteBar_WithCorrectParameters));

            var adminName = "pesho";
            var id = "1";
            var barName = "newBar";

            var admin = new AppUser
            {
                Id = id,
                UserName = adminName
            };
            _userServices.Setup(x => x.GetUsernameById(id))
                      .ReturnsAsync(admin.UserName);
            _userServices.Setup(x => x.GetAdmin())
                     .ReturnsAsync(admin);
            _iNotificationManager.Setup(x => x.BarDeletedDescription(adminName, barName))
                      .Returns($"New Bar notification: User: {adminName}, just added new Bar with name: {barName}");

            using (var arrangeContext = new CMContext(options))
            {
                arrangeContext.Add(admin);
                await arrangeContext.SaveChangesAsync();
                var sut = new NotificationServices(arrangeContext, _userServices.Object,
                   _iNotificationManager.Object);
                await sut.BarDeletedNotificationToAdminAsync(id, barName);
                _userServices.Verify(u => u.GetUsernameById(id), Times.Once());
            }
        }

        [TestMethod]
        public async Task CallNotificationManagerForDeleteBar_WithCorrectParameters()
        {
            var options = TestUtils.GetOptions(nameof(CallNotificationManagerForDeleteBar_WithCorrectParameters));

            var adminName = "pesho";
            var id = "1";
            var barName = "newBar";

            var admin = new AppUser
            {
                Id = id,
                UserName = adminName
            };
            _userServices.Setup(x => x.GetUsernameById(id))
                      .ReturnsAsync(admin.UserName);
            _userServices.Setup(x => x.GetAdmin())
                     .ReturnsAsync(admin);
            _iNotificationManager.Setup(x => x.BarDeletedDescription(adminName, barName))
                      .Returns($"New Bar notification: User: {adminName}, just added new Bar with name: {barName}");

            using (var arrangeContext = new CMContext(options))
            {
                arrangeContext.Add(admin);
                await arrangeContext.SaveChangesAsync();
                var sut = new NotificationServices(arrangeContext, _userServices.Object,
                   _iNotificationManager.Object);
                await sut.BarDeletedNotificationToAdminAsync(id, barName);
                _iNotificationManager.Verify(x => x.BarDeletedDescription(adminName, barName), Times.Once());
            }
        }

        [TestMethod]
        public async Task AddNotificationToDBForDeleteBar_WithCorrectParameters()
        {
            var options = TestUtils.GetOptions(nameof(AddNotificationToDBForDeleteBar_WithCorrectParameters));

            var adminName = "pesho";
            var id = "1";
            var barName = "newBar";

            var admin = new AppUser
            {
                Id = id,
                UserName = adminName
            };
            _userServices.Setup(x => x.GetUsernameById(id))
                      .ReturnsAsync(admin.UserName);
            _userServices.Setup(x => x.GetAdmin())
                     .ReturnsAsync(admin);
            _iNotificationManager.Setup(x => x.BarDeletedDescription(adminName, barName))
                      .Returns($"LALAA");

            using (var arrangeContext = new CMContext(options))
            {
                arrangeContext.Add(admin);
                await arrangeContext.SaveChangesAsync();
                Assert.AreEqual(0, arrangeContext.Notifications.Count());
                var sut = new NotificationServices(arrangeContext, _userServices.Object,
                   _iNotificationManager.Object);
                await sut.BarDeletedNotificationToAdminAsync(id, barName);
                Assert.AreEqual(1, arrangeContext.Notifications.Count());
                Assert.AreEqual("LALAA", arrangeContext.Notifications.First().Description);
                Assert.AreEqual(adminName, arrangeContext.Notifications.First().Username);
            }
        }

        [TestMethod]
        public async Task ThrowExceptionForDelete_WhenPassedBarNameIsNull()
        {
            var options = TestUtils.GetOptions(nameof(ThrowExceptionForDelete_WhenPassedBarNameIsNull));

            var adminName = "pesho";
            var id = "1";
            var barName = "newBar";

            var admin = new AppUser
            {
                Id = id,
                UserName = adminName
            };
            _userServices.Setup(x => x.GetUsernameById(id))
                      .ReturnsAsync(admin.UserName);
            _userServices.Setup(x => x.GetAdmin())
                     .ReturnsAsync(admin);
            _iNotificationManager.Setup(x => x.BarDeletedDescription(adminName, barName))
                      .Returns($"New Bar notification: User: {adminName}, just added new Bar with name: {barName}");
            using (var assertContext = new CMContext(options))
            {
                var sut = new NotificationServices(assertContext, _userServices.Object,
                   _iNotificationManager.Object);
                var ex = await Assert.ThrowsExceptionAsync<MagicExeption>(
                  async () => await sut.BarDeletedNotificationToAdminAsync(id, null));

            }
        }

        [TestMethod]
        public async Task ThrowCorrectMessageForDeleteBar_WhenPassedBarNameIsNull()
        {
            var options = TestUtils.GetOptions(nameof(ThrowCorrectMessageForDeleteBar_WhenPassedBarNameIsNull));

            var adminName = "pesho";
            var id = "1";
            var barName = "newBar";

            var admin = new AppUser
            {
                Id = id,
                UserName = adminName
            };
            _userServices.Setup(x => x.GetUsernameById(id))
                      .ReturnsAsync(admin.UserName);
            _userServices.Setup(x => x.GetAdmin())
                     .ReturnsAsync(admin);
            _iNotificationManager.Setup(x => x.BarDeletedDescription(adminName, barName))
                      .Returns($"New Bar notification: User: {adminName}, just added new Bar with name: {barName}");
            using (var assertContext = new CMContext(options))
            {
                var sut = new NotificationServices(assertContext, _userServices.Object,
                   _iNotificationManager.Object);
                var ex = await Assert.ThrowsExceptionAsync<MagicExeption>(
                  async () => await sut.BarDeletedNotificationToAdminAsync(id, null));
                Assert.AreEqual(ExeptionMessages.BarNameNull, ex.Message);
            }
        }
    }
}
