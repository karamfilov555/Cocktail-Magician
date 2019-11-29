using CM.Data;
using CM.Models;
using CM.Services.Contracts;
using CM.Services.CustomExceptions;
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
    public class BarCreateNotificationToAdminAsync_Should
    {
        Mock<IAppUserServices> _userServices;
        Mock<INotificationManager> _iNotificationManager;
        public BarCreateNotificationToAdminAsync_Should()
        {
            _userServices = new Mock<IAppUserServices>();
            _iNotificationManager = new Mock<INotificationManager>();
        }

        [TestMethod]
        public async Task CallUserManagerForCreateBar_WithCorrectParameters()
        {
            var options = TestUtils.GetOptions(nameof(CallUserManagerForCreateBar_WithCorrectParameters));

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
            _iNotificationManager.Setup(x => x.BarAddedDescription(adminName, barName))
                      .Returns($"New Bar notification: User: {adminName}, just added new Bar with name: {barName}");

            using (var arrangeContext = new CMContext(options))
            {
                arrangeContext.Add(admin);
                await arrangeContext.SaveChangesAsync();
                var sut = new NotificationServices(arrangeContext, _userServices.Object,
                   _iNotificationManager.Object);
                await sut.BarCreateNotificationToAdminAsync(id, barName);
                _userServices.Verify(u => u.GetUsernameById(id), Times.Once());
            }
        }

        [TestMethod]
        public async Task CallNotificationManager_WithCorrectParameters()
        {
            var options = TestUtils.GetOptions(nameof(CallNotificationManager_WithCorrectParameters));

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
            _iNotificationManager.Setup(x => x.BarAddedDescription(adminName, barName))
                      .Returns($"New Bar notification: User: {adminName}, just added new Bar with name: {barName}");

            using (var arrangeContext = new CMContext(options))
            {
                arrangeContext.Add(admin);
                await arrangeContext.SaveChangesAsync();
                var sut = new NotificationServices(arrangeContext, _userServices.Object,
                   _iNotificationManager.Object);
                await sut.BarCreateNotificationToAdminAsync(id, barName);
                _iNotificationManager.Verify(x => x.BarAddedDescription(adminName, barName), Times.Once());
            }
        }

        [TestMethod]
        public async Task AddNotificationToDB_WithCorrectParameters()
        {
            var options = TestUtils.GetOptions(nameof(AddNotificationToDB_WithCorrectParameters));

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
            _iNotificationManager.Setup(x => x.BarAddedDescription(adminName, barName))
                      .Returns($"New Bar notification: User: {adminName}, just added new Bar with name: {barName}");

            using (var arrangeContext = new CMContext(options))
            {
                arrangeContext.Add(admin);
                await arrangeContext.SaveChangesAsync();
                Assert.AreEqual(0, arrangeContext.Notifications.Count());
                var sut = new NotificationServices(arrangeContext, _userServices.Object,
                   _iNotificationManager.Object);
                await sut.BarCreateNotificationToAdminAsync(id, barName);
                Assert.AreEqual(1, arrangeContext.Notifications.Count());
            }
        }

        [TestMethod]
        public async Task ThrowException_WhenPassedBarNameIsNull()
        {
            var options = TestUtils.GetOptions(nameof(ThrowException_WhenPassedBarNameIsNull));

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
            _iNotificationManager.Setup(x => x.BarAddedDescription(adminName, barName))
                      .Returns($"New Bar notification: User: {adminName}, just added new Bar with name: {barName}");
            using (var assertContext = new CMContext(options))
            {
                var sut = new NotificationServices(assertContext, _userServices.Object,
                   _iNotificationManager.Object);
                var ex = await Assert.ThrowsExceptionAsync<MagicException>(
                  async () => await sut.BarCreateNotificationToAdminAsync(id, null));
                
            }
        }

        [TestMethod]
        public async Task ThrowCorrectMessage_WhenPassedBarNameIsNull()
        {
            var options = TestUtils.GetOptions(nameof(ThrowCorrectMessage_WhenPassedBarNameIsNull));

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
            _iNotificationManager.Setup(x => x.BarAddedDescription(adminName, barName))
                      .Returns($"New Bar notification: User: {adminName}, just added new Bar with name: {barName}");
            using (var assertContext = new CMContext(options))
            {
                var sut = new NotificationServices(assertContext, _userServices.Object,
                   _iNotificationManager.Object);
                var ex = await Assert.ThrowsExceptionAsync<MagicException>(
                  async () => await sut.BarCreateNotificationToAdminAsync(id, null));
                Assert.AreEqual(ExceptionMessages.BarNameNull, ex.Message);
            }
        }
    }
}
