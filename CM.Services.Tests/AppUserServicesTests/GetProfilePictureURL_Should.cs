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
    public class GetProfilePictureURL_Should
    {
        [TestMethod]
        public async Task Return_AppUserImageUrl_WhenValidValuePassed()
        {
            var options = TestUtils.GetOptions(nameof(Return_AppUserImageUrl_WhenValidValuePassed));

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
                        ImageURL = "myImage"
                    });

                //2
                arrangeContext.Users.Add(
                    new AppUser
                    {
                        Id = "2",
                        UserName = "user2",
                        Email = "bb@yahoo.com",
                        ImageURL = "user2Img"
                    });

                //3
                var user = new AppUser
                {
                    Id = "3",
                    UserName = "user3",
                    Email = "cc@google.com",
                    ImageURL = "targetImg"
                };
                arrangeContext.Users.Add(user);

                await arrangeContext.SaveChangesAsync();
            }
            using (var assertContext = new CMContext(options))
            {
                var sut = new AppUserServices(assertContext, userManagerMocked.Object);

                var result = await sut.GetProfilePictureURL("3");

                Assert.AreEqual("targetImg", result);
                Assert.IsInstanceOfType(result, typeof(String));
            }
        }

        [TestMethod]
        public async Task Return_Null_IfAppUserImageUrl_DoesNotExist()
        {
            var options = TestUtils.GetOptions(nameof(Return_Null_IfAppUserImageUrl_DoesNotExist));

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
                        ImageURL = "myImage"
                    });

                //2
                arrangeContext.Users.Add(
                    new AppUser
                    {
                        Id = "2",
                        UserName = "user2",
                        Email = "bb@yahoo.com",
                        ImageURL = "user2Img"
                    });

                //3
                var user = new AppUser
                {
                    Id = "3",
                    UserName = "user3",
                    Email = "cc@google.com",
                };
                arrangeContext.Users.Add(user);

                await arrangeContext.SaveChangesAsync();
            }
            using (var assertContext = new CMContext(options))
            {
                var sut = new AppUserServices(assertContext, userManagerMocked.Object);

                var result = await sut.GetProfilePictureURL("3");

                Assert.AreEqual(null, result);
            }
        }

        [TestMethod]
        public async Task Throw_MagicExeption_WhenNullValueId_Passed()
        {
            var options = TestUtils.GetOptions(nameof(Throw_MagicExeption_WhenNullValueId_Passed));

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
                        ImageURL = "myImage"
                    });

                //2
                arrangeContext.Users.Add(
                    new AppUser
                    {
                        Id = "2",
                        UserName = "user2",
                        Email = "bb@yahoo.com",
                        ImageURL = "user2Img"
                    });

                //3
                var user = new AppUser
                {
                    Id = "3",
                    UserName = "user3",
                    Email = "cc@google.com",
                    ImageURL = "targetImg"
                };
                arrangeContext.Users.Add(user);

                await arrangeContext.SaveChangesAsync();
            }
            using (var assertContext = new CMContext(options))
            {
                var sut = new AppUserServices(assertContext, userManagerMocked.Object);

                var result = await Assert.ThrowsExceptionAsync<MagicExeption>(
                    async () => await sut.GetProfilePictureURL(null));
            }
        }
        [TestMethod]
        public async Task Throw_MagicCorrectExeption_WhenNullValueId_Passed()
        {
            var options = TestUtils.GetOptions(nameof(Throw_MagicCorrectExeption_WhenNullValueId_Passed));

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
                        ImageURL = "myImage"
                    });

                //2
                arrangeContext.Users.Add(
                    new AppUser
                    {
                        Id = "2",
                        UserName = "user2",
                        Email = "bb@yahoo.com",
                        ImageURL = "user2Img"
                    });

                //3
                var user = new AppUser
                {
                    Id = "3",
                    UserName = "user3",
                    Email = "cc@google.com",
                    ImageURL = "targetImg"
                };
                arrangeContext.Users.Add(user);

                await arrangeContext.SaveChangesAsync();
            }
            using (var assertContext = new CMContext(options))
            {
                var sut = new AppUserServices(assertContext, userManagerMocked.Object);

                var result = await Assert.ThrowsExceptionAsync<MagicExeption>(
                    async () => await sut.GetProfilePictureURL(null));
                Assert.AreEqual("ID cannot be null!", result.Message);
            }
        }
    }
}
