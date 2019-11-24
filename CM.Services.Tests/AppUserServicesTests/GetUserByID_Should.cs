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
    public class GetUserByID_Should
    {
        [TestMethod]
        public async Task Return_AppUserWithCorrectAttributes_WhenValidIdPassed()
        {
            var options = TestUtils.GetOptions(nameof(Return_AppUserWithCorrectAttributes_WhenValidIdPassed));

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

                var result = await sut.GetUserByID("3");

                Assert.AreEqual("user3", result.UserName);
                Assert.AreEqual("3", result.Id);
                Assert.IsInstanceOfType(result,typeof(AppUser));
            }
        }

        [TestMethod]
        public async Task Throw_MagicExeption_NullValueIdPassed()
        {
            var options = TestUtils.GetOptions(nameof(Throw_MagicExeption_NullValueIdPassed));

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

                var result = Assert.ThrowsExceptionAsync<MagicExeption>
                    (async () => await sut.GetUserByID(null));
            }
        }
        [TestMethod]
        public async Task Throw_CorrectMagicExeption_NullValueIdPassed()
        {
            var options = TestUtils.GetOptions(nameof(Throw_CorrectMagicExeption_NullValueIdPassed));

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
                    (async () => await sut.GetUserByID(null));
                Assert.AreEqual("ID cannot be null!", result.Message);
            }
        }

        [TestMethod]
        public async Task Throw_MagicExeption_IfUserWithThisID_DoesNotExist()
        {
            var options = TestUtils.GetOptions(nameof(Throw_MagicExeption_IfUserWithThisID_DoesNotExist));

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
                    (async () => await sut.GetUserByID("dasda"));
            }
        }

        [TestMethod]
        public async Task Throw_CorrectMagicExeption_IfUserWithThisID_DoesNotExist()
        {
            var options = TestUtils.GetOptions(nameof(Throw_CorrectMagicExeption_IfUserWithThisID_DoesNotExist));

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
                    (async () => await sut.GetUserByID("dasda"));
                Assert.AreEqual("AppUser cannot be null!", result.Message);
            }
        }

        //    [TestMethod]
        //    public async Task Return_InstanceOfTypeDto_WhenValidIdPassed()
        //    {
        //        var options = TestUtils.GetOptions(nameof(Return_InstanceOfTypeDto_WhenValidIdPassed));

        //        var fileService = new Mock<IFileUploadService>();

        //        using (var arrangeContext = new CMContext(options))
        //        {

        //            //1
        //            arrangeContext.Cocktails.Add(new Cocktail { Id = "1", Name = "cocktail" });

        //            arrangeContext.Bars.Add(
        //               new Bar
        //               {
        //                   Id = "3",
        //                   Name = "Target",
        //                   Image = "Snimka",
        //                   Website = "abv.bg",
        //                   Address = new Address
        //                   {
        //                       Id = "3",
        //                       BarId = "3",
        //                       Country = new Country
        //                       {
        //                           Id = "3",
        //                           Name = "Bulgaria"
        //                       }
        //                   },

        //                   BarCocktails = new List<BarCocktail>
        //                    {
        //                        new BarCocktail
        //                        {
        //                            BarId = "3",
        //                            CocktailId = "1"
        //                        }
        //                    }
        //                   ,
        //                   BarRating = 3
        //               });

        //            await arrangeContext.SaveChangesAsync();
        //        }
        //        using (var assertContext = new CMContext(options))
        //        {
        //            var sut = new BarServices(assertContext, fileService.Object);

        //            var result = await sut.GetBarByID("3");

        //            Assert.IsInstanceOfType(result, typeof(BarDTO));
        //        }
        //    }

        //    [TestMethod]
        //    public async Task Throw_MagicExeption_WhenNullIdPassed()
        //    {
        //        var options = TestUtils.GetOptions(nameof(Throw_MagicExeption_WhenNullIdPassed));

        //        var fileService = new Mock<IFileUploadService>();

        //        using (var arrangeContext = new CMContext(options))
        //        {
        //            //1
        //            arrangeContext.Cocktails.Add(new Cocktail { Id = "1", Name = "cocktail" });

        //            arrangeContext.Bars.Add(
        //               new Bar
        //               {
        //                   Id = "3",
        //                   Name = "Target",
        //                   Image = "Snimka",
        //                   Website = "abv.bg",
        //                   Address = new Address
        //                   {
        //                       Id = "3",
        //                       BarId = "3",
        //                       Country = new Country
        //                       {
        //                           Id = "3",
        //                           Name = "Bulgaria"
        //                       }
        //                   },

        //                   BarCocktails = new List<BarCocktail>
        //                    {
        //                        new BarCocktail
        //                        {
        //                            BarId = "3",
        //                            CocktailId = "1"
        //                        }
        //                    }
        //                   ,
        //                   BarRating = 3
        //               });

        //            await arrangeContext.SaveChangesAsync();
        //        }
        //        using (var assertContext = new CMContext(options))
        //        {
        //            var sut = new BarServices(assertContext, fileService.Object);

        //            var ex = await Assert.ThrowsExceptionAsync<MagicExeption>(
        //                async () => await sut.GetBarByID(null)
        //                );
        //        }
        //    }

        //}
    }
}
