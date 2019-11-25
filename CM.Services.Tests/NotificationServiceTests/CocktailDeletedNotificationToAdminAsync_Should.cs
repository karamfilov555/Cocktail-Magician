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
    public class CocktailDeletedNotificationToAdminAsync_Should
    {
        Mock<IAppUserServices> _userServices;
        Mock<INotificationManager> _iNotificationManager;
        public CocktailDeletedNotificationToAdminAsync_Should()
        {
            _userServices = new Mock<IAppUserServices>();
            _iNotificationManager = new Mock<INotificationManager>();
        }

        [TestMethod]
        public async Task CallUserManagerForDeleteCocktail_WithCorrectParameters()
        {
            var options = TestUtils.GetOptions(nameof(CallUserManagerForDeleteCocktail_WithCorrectParameters));

            var adminName = "pesho";
            var id = "1";
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
            _iNotificationManager.Setup(x => x.CocktailDeletedDescription(adminName, cocktailName))
                      .Returns("LALLALALLA");

            using (var arrangeContext = new CMContext(options))
            {
                arrangeContext.Add(admin);
                await arrangeContext.SaveChangesAsync();
                var sut = new NotificationServices(arrangeContext, _userServices.Object,
                   _iNotificationManager.Object);
                await sut.CocktailDeletedNotificationToAdminAsync(id, cocktailName);
                _userServices.Verify(u => u.GetUsernameById(id), Times.Once());
            }
        }

        [TestMethod]
        public async Task CallNotificationManagerForDeleteCocktail_WithCorrectParameters()
        {
            var options = TestUtils.GetOptions(nameof(CallNotificationManagerForDeleteCocktail_WithCorrectParameters));

            var adminName = "pesho";
            var id = "1";
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
            _iNotificationManager.Setup(x => x.CocktailDeletedDescription(adminName, cocktailName))
                      .Returns($"LALALALA");

            using (var arrangeContext = new CMContext(options))
            {
                arrangeContext.Add(admin);
                await arrangeContext.SaveChangesAsync();
                var sut = new NotificationServices(arrangeContext, _userServices.Object,
                   _iNotificationManager.Object);
                await sut.CocktailDeletedNotificationToAdminAsync(id, cocktailName);
                _iNotificationManager.Verify(x => x.CocktailDeletedDescription(adminName, cocktailName), Times.Once());
            }
        }

        [TestMethod]
        public async Task AddNotificationToDBForDeleteCocktail_WithCorrectParameters()
        {
            var options = TestUtils.GetOptions(nameof(AddNotificationToDBForDeleteCocktail_WithCorrectParameters));

            var adminName = "pesho";
            var id = "1";
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
            _iNotificationManager.Setup(x => x.CocktailDeletedDescription(adminName, cocktailName))
                      .Returns("LALALALALA");

            using (var arrangeContext = new CMContext(options))
            {
                arrangeContext.Add(admin);
                await arrangeContext.SaveChangesAsync();
                Assert.AreEqual(0, arrangeContext.Notifications.Count());
                var sut = new NotificationServices(arrangeContext, _userServices.Object,
                   _iNotificationManager.Object);
                await sut.CocktailDeletedNotificationToAdminAsync(id, cocktailName);
                Assert.AreEqual(1, arrangeContext.Notifications.Count());
            }
        }

        [TestMethod]
        public async Task ThrowExceptionForDeleteCocktail_WhenPassedCocktailNameIsNull()
        {
            var options = TestUtils.GetOptions(nameof(ThrowExceptionForDeleteCocktail_WhenPassedCocktailNameIsNull));

            var adminName = "pesho";
            var id = "1";
            var cocktailName = "new";

            var admin = new AppUser
            {
                Id = id,
                UserName = adminName
            };
            _userServices.Setup(x => x.GetUsernameById(id))
                      .ReturnsAsync(admin.UserName);
            _userServices.Setup(x => x.GetAdmin())
                     .ReturnsAsync(admin);
            _iNotificationManager.Setup(x => x.CocktailDeletedDescription(adminName, cocktailName))
                      .Returns("LLALALALALA");
            using (var assertContext = new CMContext(options))
            {
                var sut = new NotificationServices(assertContext, _userServices.Object,
                   _iNotificationManager.Object);
                var ex = await Assert.ThrowsExceptionAsync<MagicExeption>(
                  async () => await sut.CocktailDeletedNotificationToAdminAsync(id, null));

            }
        }

        [TestMethod]
        public async Task ThrowCorrectMessageForDeleteCocktail_WhenPassedCocktailNameIsNull()
        {
            var options = TestUtils.GetOptions(nameof(ThrowCorrectMessageForDeleteCocktail_WhenPassedCocktailNameIsNull));

            var adminName = "pesho";
            var id = "1";
            var cocktailName = "new";

            var admin = new AppUser
            {
                Id = id,
                UserName = adminName
            };
            _userServices.Setup(x => x.GetUsernameById(id))
                      .ReturnsAsync(admin.UserName);
            _userServices.Setup(x => x.GetAdmin())
                     .ReturnsAsync(admin);
            _iNotificationManager.Setup(x => x.CocktailDeletedDescription(adminName, cocktailName))
                      .Returns($"LALALALA");
            using (var assertContext = new CMContext(options))
            {
                var sut = new NotificationServices(assertContext, _userServices.Object,
                   _iNotificationManager.Object);
                var ex = await Assert.ThrowsExceptionAsync<MagicExeption>(
                  async () => await sut.CocktailDeletedNotificationToAdminAsync(id, null));
                Assert.AreEqual(ExeptionMessages.CocktailNameNull, ex.Message);
            }
        }
    }
}
