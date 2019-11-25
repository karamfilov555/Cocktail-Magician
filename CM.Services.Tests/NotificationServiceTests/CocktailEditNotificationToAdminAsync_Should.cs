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
    public class CocktailEditNotificationToAdminAsync_Should
    {
        Mock<IAppUserServices> _userServices;
        Mock<INotificationManager> _iNotificationManager;
        public CocktailEditNotificationToAdminAsync_Should()
        {
            _userServices = new Mock<IAppUserServices>();
            _iNotificationManager = new Mock<INotificationManager>();
        }

        [TestMethod]
        public async Task CallUserManagerForEditCocktail_WithCorrectParameters()
        {
            var options = TestUtils.GetOptions(nameof(CallUserManagerForEditCocktail_WithCorrectParameters));

            var adminName = "pesho";
            var id = "1";
            var oldName = "oldCocktail";
            var cocktailName = "newCocktail";

            var admin = new AppUser
            {
                Id = id,
                UserName = adminName
            };
            _userServices.Setup(x => x.GetUsernameById(id))
                      .ReturnsAsync(admin.UserName);
            _userServices.Setup(x => x.GetAdmin())
                     .ReturnsAsync(admin);
            _iNotificationManager.Setup(x => x.CocktailEditedDescription(adminName,oldName, cocktailName))
                      .Returns("LALLALALLA");

            using (var arrangeContext = new CMContext(options))
            {
                arrangeContext.Add(admin);
                await arrangeContext.SaveChangesAsync();
                var sut = new NotificationServices(arrangeContext, _userServices.Object,
                   _iNotificationManager.Object);
                await sut.CocktailEditNotificationToAdminAsync(id, oldName, cocktailName);
                _userServices.Verify(u => u.GetUsernameById(id), Times.Once());
            }
        }

        [TestMethod]
        public async Task CallNotificationManagerForOldCocktail_WithCorrectParameters()
        {
            var options = TestUtils.GetOptions(nameof(CallNotificationManagerForOldCocktail_WithCorrectParameters));

            var adminName = "pesho";
            var id = "1";
            var oldName = "oldCocktail";
            var cocktailName = "oldCocktail";

            var admin = new AppUser
            {
                Id = id,
                UserName = adminName
            };
            _userServices.Setup(x => x.GetUsernameById(id))
                      .ReturnsAsync(admin.UserName);
            _userServices.Setup(x => x.GetAdmin())
                     .ReturnsAsync(admin);
            _iNotificationManager.Setup(x => x.CocktailEditedSameNameDescription(adminName,oldName))
                      .Returns($"LALALALA");

            using (var arrangeContext = new CMContext(options))
            {
                arrangeContext.Add(admin);
                await arrangeContext.SaveChangesAsync();
                var sut = new NotificationServices(arrangeContext, _userServices.Object,
                   _iNotificationManager.Object);
                await sut.CocktailEditNotificationToAdminAsync(id, oldName, cocktailName);
                _iNotificationManager.Verify(x => x.CocktailEditedSameNameDescription(adminName, oldName), Times.Once());
            }
        }

        [TestMethod]
        public async Task CallNotificationManagerForEditNewCocktail_WithCorrectParameters()
        {
            var options = TestUtils.GetOptions(nameof(CallNotificationManagerForEditNewCocktail_WithCorrectParameters));

            var adminName = "pesho";
            var id = "1";
            var oldName = "oldCocktail";
            var cocktailName = "newCocktail";

            var admin = new AppUser
            {
                Id = id,
                UserName = adminName
            };
            _userServices.Setup(x => x.GetUsernameById(id))
                      .ReturnsAsync(admin.UserName);
            _userServices.Setup(x => x.GetAdmin())
                     .ReturnsAsync(admin);
            _iNotificationManager.Setup(x => x.CocktailEditedDescription(adminName, oldName, cocktailName))
                      .Returns($"LALALALA");

            using (var arrangeContext = new CMContext(options))
            {
                arrangeContext.Add(admin);
                await arrangeContext.SaveChangesAsync();
                var sut = new NotificationServices(arrangeContext, _userServices.Object,
                   _iNotificationManager.Object);
                await sut.CocktailEditNotificationToAdminAsync(id, oldName, cocktailName);
                _iNotificationManager.Verify(x => x.CocktailEditedDescription(adminName, oldName, cocktailName), Times.Once());
            }
        }

        [TestMethod]
        public async Task AddNotificationToDBForEditCocktail_WithCorrectParameters()
        {
            var options = TestUtils.GetOptions(nameof(AddNotificationToDBForEditCocktail_WithCorrectParameters));

            var adminName = "pesho";
            var id = "1";
            var oldName = "oldCocktail";
            var cocktailName = "newCocktail";
            var admin = new AppUser
            {
                Id = id,
                UserName = adminName
            };
            _userServices.Setup(x => x.GetUsernameById(id))
                      .ReturnsAsync(admin.UserName);
            _userServices.Setup(x => x.GetAdmin())
                     .ReturnsAsync(admin);
            _iNotificationManager.Setup(x => x.CocktailEditedDescription(adminName,oldName, cocktailName))
                      .Returns("LALALALALA");

            using (var arrangeContext = new CMContext(options))
            {
                arrangeContext.Add(admin);
                await arrangeContext.SaveChangesAsync();
                Assert.AreEqual(0, arrangeContext.Notifications.Count());
                var sut = new NotificationServices(arrangeContext, _userServices.Object,
                   _iNotificationManager.Object);
                await sut.CocktailEditNotificationToAdminAsync(id,oldName, cocktailName);
                Assert.AreEqual(1, arrangeContext.Notifications.Count());
            }
        }

        [TestMethod]
        public async Task AddNotificationToDBForEditCocktailWithCorrectDecription_WithCorrectParameters()
        {
            var options = TestUtils.GetOptions(nameof(AddNotificationToDBForEditCocktailWithCorrectDecription_WithCorrectParameters));

            var adminName = "pesho";
            var id = "1";
            var oldName = "oldCocktail";
            var cocktailName = "newCocktail";
            var admin = new AppUser
            {
                Id = id,
                UserName = adminName
            };
            _userServices.Setup(x => x.GetUsernameById(id))
                      .ReturnsAsync(admin.UserName);
            _userServices.Setup(x => x.GetAdmin())
                     .ReturnsAsync(admin);
            _iNotificationManager.Setup(x => x.CocktailEditedDescription(adminName, oldName, cocktailName))
                      .Returns("LALALALALA");

            using (var arrangeContext = new CMContext(options))
            {
                arrangeContext.Add(admin);
                await arrangeContext.SaveChangesAsync();
                Assert.AreEqual(0, arrangeContext.Notifications.Count());
                var sut = new NotificationServices(arrangeContext, _userServices.Object,
                   _iNotificationManager.Object);
                await sut.CocktailEditNotificationToAdminAsync(id, oldName, cocktailName);
                Assert.AreEqual("LALALALALA", arrangeContext.Notifications.First().Description);
                Assert.AreEqual(adminName, arrangeContext.Notifications.First().Username);
            }
        }

        [TestMethod]
        public async Task ThrowExceptionForEdit_WhenPassedOldNameIsNull()
        {
            var options = TestUtils.GetOptions(nameof(ThrowExceptionForEdit_WhenPassedOldNameIsNull));

            var adminName = "pesho";
            var id = "1";
            var cocktailName = "new";
            string oldName = null;

            var admin = new AppUser
            {
                Id = id,
                UserName = adminName
            };
            _userServices.Setup(x => x.GetUsernameById(id))
                      .ReturnsAsync(admin.UserName);
            _userServices.Setup(x => x.GetAdmin())
                     .ReturnsAsync(admin);
            _iNotificationManager.Setup(x => x.CocktailEditedDescription(adminName, oldName, cocktailName))
                      .Returns("LLALALALALA");
            using (var assertContext = new CMContext(options))
            {
                var sut = new NotificationServices(assertContext, _userServices.Object,
                   _iNotificationManager.Object);
                var ex = await Assert.ThrowsExceptionAsync<MagicExeption>(
                  async () => await sut.CocktailEditNotificationToAdminAsync(id, oldName, cocktailName));

            }
        }

        [TestMethod]
        public async Task ThrowCorrectMessage_WhenPassedOldNameIsNull()
        {
            var options = TestUtils.GetOptions(nameof(ThrowCorrectMessage_WhenPassedOldNameIsNull));

            var adminName = "pesho";
            var id = "1";
            var cocktailName = "new";
            string oldName = null;

            var admin = new AppUser
            {
                Id = id,
                UserName = adminName
            };
            _userServices.Setup(x => x.GetUsernameById(id))
                      .ReturnsAsync(admin.UserName);
            _userServices.Setup(x => x.GetAdmin())
                     .ReturnsAsync(admin);
            _iNotificationManager.Setup(x => x.CocktailEditedDescription(adminName, oldName, cocktailName))
                      .Returns($"LALALALA");
            using (var assertContext = new CMContext(options))
            {
                var sut = new NotificationServices(assertContext, _userServices.Object,
                   _iNotificationManager.Object);
                var ex = await Assert.ThrowsExceptionAsync<MagicExeption>(
                  async () => await sut.CocktailEditNotificationToAdminAsync(id, oldName, cocktailName));
                Assert.AreEqual(ExeptionMessages.CocktailNameNull, ex.Message);
            }
        }

        [TestMethod]
        public async Task ThrowExceptionForEdit_WhenPassedNewNameIsNull()
        {
            var options = TestUtils.GetOptions(nameof(ThrowExceptionForEdit_WhenPassedNewNameIsNull));

            var adminName = "pesho";
            var id = "1";
            string cocktailName = null;
            string oldName = "old";

            var admin = new AppUser
            {
                Id = id,
                UserName = adminName
            };
            _userServices.Setup(x => x.GetUsernameById(id))
                      .ReturnsAsync(admin.UserName);
            _userServices.Setup(x => x.GetAdmin())
                     .ReturnsAsync(admin);
            _iNotificationManager.Setup(x => x.CocktailEditedDescription(adminName, oldName, cocktailName))
                      .Returns("LLALALALALA");
            using (var assertContext = new CMContext(options))
            {
                var sut = new NotificationServices(assertContext, _userServices.Object,
                   _iNotificationManager.Object);
                var ex = await Assert.ThrowsExceptionAsync<MagicExeption>(
                  async () => await sut.CocktailEditNotificationToAdminAsync(id, oldName, cocktailName));

            }
        }

        [TestMethod]
        public async Task ThrowCorrectMessage_WhenPassedNewNameIsNull()
        {
            var options = TestUtils.GetOptions(nameof(ThrowCorrectMessage_WhenPassedNewNameIsNull));

            var adminName = "pesho";
            var id = "1";
            string cocktailName =null;
            string oldName = "old";

            var admin = new AppUser
            {
                Id = id,
                UserName = adminName
            };
            _userServices.Setup(x => x.GetUsernameById(id))
                      .ReturnsAsync(admin.UserName);
            _userServices.Setup(x => x.GetAdmin())
                     .ReturnsAsync(admin);
            _iNotificationManager.Setup(x => x.CocktailEditedDescription(adminName, oldName, cocktailName))
                      .Returns($"LALALALA");
            using (var assertContext = new CMContext(options))
            {
                var sut = new NotificationServices(assertContext, _userServices.Object,
                   _iNotificationManager.Object);
                var ex = await Assert.ThrowsExceptionAsync<MagicExeption>(
                  async () => await sut.CocktailEditNotificationToAdminAsync(id, oldName, cocktailName));
                Assert.AreEqual(ExeptionMessages.CocktailNameNull, ex.Message);
            }
        }
    }
}
