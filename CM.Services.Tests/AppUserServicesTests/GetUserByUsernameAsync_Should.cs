using CM.Data;
using CM.Models;
using CM.Services.CustomExceptions;
using Microsoft.AspNetCore.Identity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CM.Services.Tests.AppUserServicesTests
{
    [TestClass]
    public class GetUserByUsernameAsync_Should
    {
        [TestMethod]
        public async Task Throw_MagicExeption_IfNullValue_UserNamePassed()
        {
            var options = TestUtils.GetOptions(nameof(Throw_MagicExeption_IfNullValue_UserNamePassed));

            var userStoreMocked = new Mock<IUserStore<AppUser>>();
            var userManagerMocked =
                new Mock<UserManager<AppUser>>
                (userStoreMocked.Object, null, null, null, null, null, null, null, null);
           
            using (var assertContext = new CMContext(options))
            {
                var sut = new AppUserServices(assertContext, userManagerMocked.Object);

                var result = await Assert.ThrowsExceptionAsync<MagicException>
                    (async () => await sut.GetUserByUsernameAsync(null));
            }
        }

        [TestMethod]
        public async Task Throw_CorrectMagicExeption_IfNullValue_UserNamePassed()
        {
            var options = TestUtils.GetOptions(nameof(Throw_CorrectMagicExeption_IfNullValue_UserNamePassed));

            var userStoreMocked = new Mock<IUserStore<AppUser>>();
            var userManagerMocked =
                new Mock<UserManager<AppUser>>
                (userStoreMocked.Object, null, null, null, null, null, null, null, null);

            using (var assertContext = new CMContext(options))
            {
                var sut = new AppUserServices(assertContext, userManagerMocked.Object);

                var result = await Assert.ThrowsExceptionAsync<MagicException>
                    (async () => await sut.GetUserByUsernameAsync(null));
                Assert.AreEqual("UserName cannot be null!", result.Message);
            }
        }
        [TestMethod]
        public async Task Throw_CorrectMagicExeption_IfNullValue_AppUserOccurs()
        {
            var options = TestUtils.GetOptions(nameof(Throw_CorrectMagicExeption_IfNullValue_AppUserOccurs));

            var userStoreMocked = new Mock<IUserStore<AppUser>>();
            var userManagerMocked =
                new Mock<UserManager<AppUser>>
                (userStoreMocked.Object, null, null, null, null, null, null, null, null);

            using (var assertContext = new CMContext(options))
            {
                var sut = new AppUserServices(assertContext, userManagerMocked.Object);

                var result = await Assert.ThrowsExceptionAsync<MagicException>
                    (async () => await sut.GetUserByUsernameAsync("1333"));
                Assert.AreEqual("AppUser cannot be null!", result.Message);
            }
        }
        [TestMethod]
        public async Task Throw_MagicExeption_IfNullValue_AppUserOccurs()
        {
            var options = TestUtils.GetOptions(nameof(Throw_CorrectMagicExeption_IfNullValue_AppUserOccurs));

            var userStoreMocked = new Mock<IUserStore<AppUser>>();
            var userManagerMocked =
                new Mock<UserManager<AppUser>>
                (userStoreMocked.Object, null, null, null, null, null, null, null, null);

            using (var assertContext = new CMContext(options))
            {
                var sut = new AppUserServices(assertContext, userManagerMocked.Object);

                var result = await Assert.ThrowsExceptionAsync<MagicException>
                    (async () => await sut.GetUserByUsernameAsync("13333"));
            }
        }
        [TestMethod]
        public async Task Return_AppUserWithSameUsername_IfThereIsOne()
        {
            var options = TestUtils.GetOptions(nameof(Return_AppUserWithSameUsername_IfThereIsOne));

            var userStoreMocked = new Mock<IUserStore<AppUser>>();
            var userManagerMocked =
                new Mock<UserManager<AppUser>>
                (userStoreMocked.Object, null, null, null, null, null, null, null, null);

            using (var arrangeContext = new CMContext(options))
            {
                //1
                arrangeContext.Users.Add(new AppUser { Id = "1", UserName = "user1" });

                //2
                arrangeContext.Users.Add(new AppUser { Id = "2", UserName = "user2" });

                //3
                arrangeContext.Users.Add(new AppUser { Id = "3", UserName = "user3",Email="user3@abv.bg" });

                await arrangeContext.SaveChangesAsync();
            }
            using (var assertContext = new CMContext(options))
            {
                var sut = new AppUserServices(assertContext, userManagerMocked.Object);

                var result = await sut.GetUserByUsernameAsync("user3");

                Assert.AreEqual("user3", result.UserName);
                Assert.AreEqual("3", result.Id);
                Assert.AreEqual("user3@abv.bg", result.Email);
                Assert.IsInstanceOfType(result, typeof(AppUser));
            }
        }
    }
}
