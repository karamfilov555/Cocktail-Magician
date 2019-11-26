using CM.Data;
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
    public class SetProfilePictureURL_Should
    {

        [TestMethod]
        public async Task Save_ProfilePictureUrl_InDb_WhenValidValuesPassed()
        {
            var options = TestUtils.GetOptions(nameof(Save_ProfilePictureUrl_InDb_WhenValidValuesPassed));

            var userStoreMocked = new Mock<IUserStore<AppUser>>();
            var userManagerMocked =
                new Mock<UserManager<AppUser>>
                (userStoreMocked.Object, null, null, null, null, null, null, null, null);

            using (var arrangeContext = new CMContext(options))
            {
                //1
                arrangeContext.Users.Add(
                    new AppUser
                    {
                        Id = "1",
                        UserName = "user1",
                        Email = "aa@abv.bg",
                    });

                await arrangeContext.SaveChangesAsync();
            }
            using (var assertContext = new CMContext(options))
            {
                var sut = new AppUserServices(assertContext, userManagerMocked.Object);

                await sut.SetProfilePictureURL("1","image");

                Assert.AreEqual("image", assertContext.Users.First(x=>x.Id=="1").ImageURL);
            }
        }

        [TestMethod]
        public async Task Throw_MagicExeption_WhenIdPassed_IsOfDeletedAppUser()
        {
            var options = TestUtils.GetOptions(nameof(Throw_MagicExeption_WhenIdPassed_IsOfDeletedAppUser));

            var userStoreMocked = new Mock<IUserStore<AppUser>>();
            var userManagerMocked =
                new Mock<UserManager<AppUser>>
                (userStoreMocked.Object, null, null, null, null, null, null, null, null);

            using (var arrangeContext = new CMContext(options))
            {
                //1
                arrangeContext.Users.Add(
                    new AppUser
                    {
                        Id = "1",
                        UserName = "user1",
                        Email = "aa@abv.bg",
                        DateDeleted = DateTime.Now
                    });

                await arrangeContext.SaveChangesAsync();
            }


            using (var assertContext = new CMContext(options))
            {
                var sut = new AppUserServices(assertContext, userManagerMocked.Object);

                var result = await Assert.ThrowsExceptionAsync<MagicException>(
                    async () => await sut.SetProfilePictureURL("1", "url"));
            }
        }

        [TestMethod]
        public async Task Throw_CorrectMagicExeption_WhenIdPassed_IsOfDeletedAppUser()
        {
            var options = TestUtils.GetOptions(nameof(Throw_CorrectMagicExeption_WhenIdPassed_IsOfDeletedAppUser));

            var userStoreMocked = new Mock<IUserStore<AppUser>>();
            var userManagerMocked =
                new Mock<UserManager<AppUser>>
                (userStoreMocked.Object, null, null, null, null, null, null, null, null);

            using (var arrangeContext = new CMContext(options))
            {
                //1
                arrangeContext.Users.Add(
                    new AppUser
                    {
                        Id = "1",
                        UserName = "user1",
                        Email = "aa@abv.bg",
                        DateDeleted = DateTime.Now
                    });

                await arrangeContext.SaveChangesAsync();
            }


            using (var assertContext = new CMContext(options))
            {
                var sut = new AppUserServices(assertContext, userManagerMocked.Object);

                var result = await Assert.ThrowsExceptionAsync<MagicException>(
                    async () => await sut.SetProfilePictureURL("1", "url"));

                Assert.AreEqual("AppUser cannot be null!", result.Message);
            }
        }


        [TestMethod]
        public async Task Throw_MagicExeption_WhenNullAppUserId_Passed()
        {
            var options = TestUtils.GetOptions(nameof(Throw_MagicExeption_WhenNullAppUserId_Passed));

            var userStoreMocked = new Mock<IUserStore<AppUser>>();
            var userManagerMocked =
                new Mock<UserManager<AppUser>>
                (userStoreMocked.Object, null, null, null, null, null, null, null, null);
        
            using (var assertContext = new CMContext(options))
            {
                var sut = new AppUserServices(assertContext, userManagerMocked.Object);

                var result = await Assert.ThrowsExceptionAsync<MagicException>(
                    async () => await sut.SetProfilePictureURL(null,"url"));
            }
        }
        [TestMethod]
        public async Task Throw_CorrectMagicExeption_WhenNullAppUserId_Passed()
        {
            var options = TestUtils.GetOptions(nameof(Throw_CorrectMagicExeption_WhenNullAppUserId_Passed));

            var userStoreMocked = new Mock<IUserStore<AppUser>>();
            var userManagerMocked =
                new Mock<UserManager<AppUser>>
                (userStoreMocked.Object, null, null, null, null, null, null, null, null);

            using (var assertContext = new CMContext(options))
            {
                var sut = new AppUserServices(assertContext, userManagerMocked.Object);

                var result = await Assert.ThrowsExceptionAsync<MagicException>(
                    async () => await sut.SetProfilePictureURL(null, "url"));

                Assert.AreEqual("ID cannot be null!", result.Message);
            }
        }

        [TestMethod]
        public async Task Throw_CorrectMagicExeption_WhenAppUserWithThisId_DoesNotExist()
        {
            var options = TestUtils.GetOptions(nameof(Throw_CorrectMagicExeption_WhenAppUserWithThisId_DoesNotExist));

            var userStoreMocked = new Mock<IUserStore<AppUser>>();
            var userManagerMocked =
                new Mock<UserManager<AppUser>>
                (userStoreMocked.Object, null, null, null, null, null, null, null, null);

            using (var assertContext = new CMContext(options))
            {
                var sut = new AppUserServices(assertContext, userManagerMocked.Object);

                var result = await Assert.ThrowsExceptionAsync<MagicException>(
                    async () => await sut.SetProfilePictureURL("asdadad", "url"));

                Assert.AreEqual("AppUser cannot be null!", result.Message);
            }
        }
        [TestMethod]
        public async Task Throw_MagicExeption_WhenAppUserWithThisId_DoesNotExist()
        {
            var options = TestUtils.GetOptions(nameof(Throw_MagicExeption_WhenAppUserWithThisId_DoesNotExist));

            var userStoreMocked = new Mock<IUserStore<AppUser>>();
            var userManagerMocked =
                new Mock<UserManager<AppUser>>
                (userStoreMocked.Object, null, null, null, null, null, null, null, null);

            using (var assertContext = new CMContext(options))
            {
                var sut = new AppUserServices(assertContext, userManagerMocked.Object);

                var result = await Assert.ThrowsExceptionAsync<MagicException>(
                    async () => await sut.SetProfilePictureURL("asdadad", "url"));
            }
        }
    }
}
