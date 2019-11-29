using CM.Data;
using CM.DTOs;
using CM.Models;
using CM.Services.CustomExceptions;
using Microsoft.AspNetCore.Identity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CM.Services.Tests.AppUserServicesTests
{
    [TestClass]
    public class Delete_Should
    {
        [TestMethod]
        public async Task SetDateDeleted_OfAppUser_ToDateTimeNow_WhenValidValuesPassed()
        {
            var options = TestUtils.GetOptions(nameof(SetDateDeleted_OfAppUser_ToDateTimeNow_WhenValidValuesPassed));

            var userStoreMocked = new Mock<IUserStore<AppUser>>();
            var userManagerMocked =
                new Mock<UserManager<AppUser>>
                (userStoreMocked.Object, null, null, null, null, null, null, null, null);

            using (var assertContext = new CMContext(options))
            {
                var user = new AppUser
                {
                    Id = "1",
                    UserName = "user1",
                    ImageURL = "user1Img",
                    Email = "user1@mail",
                };
                assertContext.Users.Add(user);
                await assertContext.SaveChangesAsync();

                var sut = new AppUserServices(assertContext, userManagerMocked.Object);

                Assert.AreEqual(null, assertContext.Users.First().DateDeleted);

                await sut.Delete("1");

                Assert.AreNotEqual(null, assertContext.Users.First().DateDeleted);
                Assert.AreEqual(1, assertContext.Users.Count());

            }
        }

        [TestMethod]
        public async Task ThrowMagicExeption_IfNullValueId_Passed()
        {
            var options = TestUtils.GetOptions(nameof(ThrowMagicExeption_IfNullValueId_Passed));

            var userStoreMocked = new Mock<IUserStore<AppUser>>();
            var userManagerMocked =
                new Mock<UserManager<AppUser>>
                (userStoreMocked.Object, null, null, null, null, null, null, null, null);

            using (var assertContext = new CMContext(options))
            {
                var sut = new AppUserServices(assertContext, userManagerMocked.Object);
                var ex = await Assert.ThrowsExceptionAsync<MagicException>
                    (async () => await sut.Delete(null));
            }
        }
        [TestMethod]
        public async Task ThrowCorrectMagicExeption_IfUserWithIdPassed_DoesnotExist()
        {
            var options = TestUtils.GetOptions(nameof(ThrowCorrectMagicExeption_IfUserWithIdPassed_DoesnotExist));

            var userStoreMocked = new Mock<IUserStore<AppUser>>();
            var userManagerMocked =
                new Mock<UserManager<AppUser>>
                (userStoreMocked.Object, null, null, null, null, null, null, null, null);

            using (var assertContext = new CMContext(options))
            {
                var sut = new AppUserServices(assertContext, userManagerMocked.Object);
                var ex = await Assert.ThrowsExceptionAsync<MagicException>
                    (async () => await sut.Delete("dasda"));
                Assert.AreEqual("AppUser cannot be null!", ex.Message);
            }
        }
        [TestMethod]
        public async Task ThrowMagicExeption_IfUserWithIdPassed_DoesnotExist()
        {
            var options = TestUtils.GetOptions(nameof(ThrowMagicExeption_IfUserWithIdPassed_DoesnotExist));

            var userStoreMocked = new Mock<IUserStore<AppUser>>();
            var userManagerMocked =
                new Mock<UserManager<AppUser>>
                (userStoreMocked.Object, null, null, null, null, null, null, null, null);

            using (var assertContext = new CMContext(options))
            {
                var sut = new AppUserServices(assertContext, userManagerMocked.Object);
                var ex = await Assert.ThrowsExceptionAsync<MagicException>
                    (async () => await sut.Delete("dasdads"));
            }
        }
        [TestMethod]
        public async Task ThrowCorrectMagicExeption_IfNullValueId_Passed()
        {
            var options = TestUtils.GetOptions(nameof(ThrowCorrectMagicExeption_IfNullValueId_Passed));

            var userStoreMocked = new Mock<IUserStore<AppUser>>();
            var userManagerMocked =
                new Mock<UserManager<AppUser>>
                (userStoreMocked.Object, null, null, null, null, null, null, null, null);

            using (var assertContext = new CMContext(options))
            {
                var sut = new AppUserServices(assertContext, userManagerMocked.Object);
                var ex = await Assert.ThrowsExceptionAsync<MagicException>
                    (async () => await sut.Delete(null));
                Assert.AreEqual("ID cannot be null!", ex.Message);
            }
        }

        

    }
}
