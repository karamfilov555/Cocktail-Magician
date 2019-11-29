using CM.Data;
using CM.DTOs;
using CM.Models;
using CM.Services.Contracts;
using CM.Services.CustomExceptions;
using Microsoft.AspNetCore.Identity;
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
    public class CreateNotificationAsyncShould
    {
        Mock<IAppUserServices> _userServices;
        Mock<INotificationManager> _iNotificationManager;

        public CreateNotificationAsyncShould()
        {
            _userServices = new Mock<IAppUserServices>();
            _iNotificationManager = new Mock<INotificationManager>();
        }

        [TestMethod]
        public async Task AddNotificationToAdmin_WhenValidParametersArePassed()
        {

            var userStoreMocked = new Mock<IUserStore<AppUser>>();
            var userManagerMock =
                new Mock<UserManager<AppUser>>
                (userStoreMocked.Object, null, null, null, null, null, null, null, null);
            var adminName = "pesho";
            var id = "1";
            var description = "new";
            var userName = "gosho";

            var options = TestUtils.GetOptions(nameof(AddNotificationToAdmin_WhenValidParametersArePassed));
            var admin = new AppUser
            {
                Id = id,
                UserName = adminName
            };
            var roles = new List<string> { "Admin" };
            _userServices.Setup(x => x.GetAdmin())
                      .ReturnsAsync(admin);

            using (var arrangeContext = new CMContext(options))
            {
                var sut = new NotificationServices(arrangeContext, _userServices.Object,
                   _iNotificationManager.Object);

                arrangeContext.Add(admin);
                //var managerRole = new AppRole { Id = "1", Name = "Manager", NormalizedName = "MANAGER" };
                var adminRole = new AppRole { Id = "2", Name = "Admin", NormalizedName = "ADMIN" };
                //arrangeContext.Roles.Add(managerRole);
                arrangeContext.Roles.Add(adminRole);
                await arrangeContext.SaveChangesAsync();
                var result = await sut.CreateNotificationAsync(description, userName);
                Assert.AreEqual(id, arrangeContext.Notifications.First().UserId);
                Assert.AreEqual(description, arrangeContext.Notifications.First().Description);
                Assert.AreEqual(userName, arrangeContext.Notifications.First().Username);
                Assert.AreNotEqual(null, arrangeContext.Notifications.First().EventDate);
            }
        }
        [TestMethod]
        public async Task ReturnNotificationDTO_WhenValidParametersArePassed()
        {

            var userStoreMocked = new Mock<IUserStore<AppUser>>();
            var userManagerMock =
                new Mock<UserManager<AppUser>>
                (userStoreMocked.Object, null, null, null, null, null, null, null, null);
            var adminName = "pesho";
            var id = "1";
            var description = "new";
            var userName = "gosho";

            var options = TestUtils.GetOptions(nameof(ReturnNotificationDTO_WhenValidParametersArePassed));
            var admin = new AppUser
            {
                Id = id,
                UserName = adminName
            };
            var roles = new List<string> { "Admin" };
            _userServices.Setup(x => x.GetAdmin())
                      .ReturnsAsync(admin);

            using (var arrangeContext = new CMContext(options))
            {
                var sut = new NotificationServices(arrangeContext, _userServices.Object,
                   _iNotificationManager.Object);

                arrangeContext.Add(admin);
                //var managerRole = new AppRole { Id = "1", Name = "Manager", NormalizedName = "MANAGER" };
                var adminRole = new AppRole { Id = "2", Name = "Admin", NormalizedName = "ADMIN" };
                //arrangeContext.Roles.Add(managerRole);
                arrangeContext.Roles.Add(adminRole);
                await arrangeContext.SaveChangesAsync();
                var result = await sut.CreateNotificationAsync(description, userName);
                Assert.IsInstanceOfType(result, typeof(NotificationDTO));
                
            }
        }

        [TestMethod]
        public async Task ThrowException_WhenPassedDescriptionIsNull()
        {
            var options = TestUtils.GetOptions(nameof(ThrowException_WhenPassedDescriptionIsNull));

            using (var assertContext = new CMContext(options))
            {
                var sut = new NotificationServices(assertContext, _userServices.Object,
                   _iNotificationManager.Object);
                var ex = await Assert.ThrowsExceptionAsync<MagicException>(
                  async () => await sut.CreateNotificationAsync(null, "5"));
            }
        }

        [TestMethod]
        public async Task ThrowCorrectMessage_WhenPassedDescriptionIsNull()
        {
            var options = TestUtils.GetOptions(nameof(ThrowCorrectMessage_WhenPassedDescriptionIsNull));

            using (var assertContext = new CMContext(options))
            {
                var sut = new NotificationServices(assertContext, _userServices.Object,
                   _iNotificationManager.Object);
                var ex = await Assert.ThrowsExceptionAsync<MagicException>(
                  async () => await sut.CreateNotificationAsync(null, "5"));
                Assert.AreEqual(ExceptionMessages.DescriptionNull, ex.Message);
            }
        }

        [TestMethod]
        public async Task ThrowException_WhenPassedUsernameIsNull()
        {
            var options = TestUtils.GetOptions(nameof(ThrowException_WhenPassedUsernameIsNull));

            using (var assertContext = new CMContext(options))
            {
                var sut = new NotificationServices(assertContext, _userServices.Object,
                   _iNotificationManager.Object);
                var ex = await Assert.ThrowsExceptionAsync<MagicException>(
                  async () => await sut.CreateNotificationAsync("23", null));
            }
        }

        [TestMethod]
        public async Task ThrowCorrectMessage_WhenPassedUsernameIsNull()
        {
            var options = TestUtils.GetOptions(nameof(ThrowCorrectMessage_WhenPassedUsernameIsNull));

            using (var assertContext = new CMContext(options))
            {
                var sut = new NotificationServices(assertContext, _userServices.Object,
                   _iNotificationManager.Object);
                var ex = await Assert.ThrowsExceptionAsync<MagicException>(
                  async () => await sut.CreateNotificationAsync("23", null));
                Assert.AreEqual(ExceptionMessages.UserNameNull, ex.Message);
            }
        }
    }
}
