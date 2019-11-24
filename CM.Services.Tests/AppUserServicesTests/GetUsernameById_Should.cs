using CM.Data;
using CM.Models;
using CM.Services.CustomExeptions;
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
    public class GetUsernameById_Should
    {
        [TestMethod]
        public async Task Return_UserNameAsString_WhenValidValues_Passed()
        {
            var options = TestUtils.GetOptions(nameof(Return_UserNameAsString_WhenValidValues_Passed));

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
                arrangeContext.Users.Add(new AppUser { Id = "3", UserName = "user3" });

                await arrangeContext.SaveChangesAsync();
            }
            using (var assertContext = new CMContext(options))
            {

                var sut = new AppUserServices(assertContext, userManagerMocked.Object);

                var result =  await sut.GetUsernameById("2");
                Assert.AreEqual("user2", result);
                Assert.IsInstanceOfType(result, typeof(String));
                var result2 = await sut.GetUsernameById("3");
                Assert.AreEqual("user3", result2);

            }
        }

        [TestMethod]
        public async Task Return_AnnonymousAsUserName_IfNullIdPassed()
        {
            var options = TestUtils.GetOptions(nameof(Return_AnnonymousAsUserName_IfNullIdPassed));

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
                arrangeContext.Users.Add(new AppUser { Id = "3", UserName = "user3" });

                await arrangeContext.SaveChangesAsync();
            }
            using (var assertContext = new CMContext(options))
            {

                var sut = new AppUserServices(assertContext, userManagerMocked.Object);

                var result = await sut.GetUsernameById(null);
                Assert.AreEqual("annonymous", result);
            }
        }

        [TestMethod]
        public async Task Throw_CorrectMagicExeption_IfUserWithSuchId_DoesNotExist()
        {
            var options = TestUtils.GetOptions(nameof(Throw_CorrectMagicExeption_IfUserWithSuchId_DoesNotExist));

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
                arrangeContext.Users.Add(new AppUser { Id = "3", UserName = "user3" });

                await arrangeContext.SaveChangesAsync();
            }
            using (var assertContext = new CMContext(options))
            {

                var sut = new AppUserServices(assertContext, userManagerMocked.Object);

                var result = await Assert.ThrowsExceptionAsync<MagicExeption>
                    (async () => await sut.GetUsernameById("a3331s"));
                Assert.AreEqual("AppUser cannot be null!", result.Message);
            }
        }
        [TestMethod]
        public async Task Throw_MagicExeption_IfUserWithSuchId_DoesNotExist()
        {
            var options = TestUtils.GetOptions(nameof(Throw_MagicExeption_IfUserWithSuchId_DoesNotExist));

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
                arrangeContext.Users.Add(new AppUser { Id = "3", UserName = "user3" });

                await arrangeContext.SaveChangesAsync();
            }
            using (var assertContext = new CMContext(options))
            {

                var sut = new AppUserServices(assertContext, userManagerMocked.Object);

                var result = await Assert.ThrowsExceptionAsync<MagicExeption>
                    (async () => await sut.GetUsernameById("a3331s"));
            }
        }
    }
}
