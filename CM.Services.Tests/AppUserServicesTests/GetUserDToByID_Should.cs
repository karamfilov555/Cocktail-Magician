using CM.Data;
using CM.DTOs;
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
    public class GetUserDToByID_Should
    {
        [TestClass]
        public class GetUserByID_Should
        {
            [TestMethod]
            public async Task Return_AppUserDTOWithCorrectAttributes_WhenValidIdPassed()
            {
                var options = TestUtils.GetOptions(nameof(Return_AppUserDTOWithCorrectAttributes_WhenValidIdPassed));

                var userStoreMocked = new Mock<IUserStore<AppUser>>();
                var userManagerMocked =
                    new Mock<UserManager<AppUser>>
                    (userStoreMocked.Object, null, null, null, null, null, null, null, null);
               
                using (var arrangeContext = new CMContext(options))
                {
                    arrangeContext.Roles.Add(new AppRole { Id = "1", Name = "Manager", NormalizedName = "MANAGER" });
                   
                    //1
                    arrangeContext.Users.Add(
                        new AppUser
                    {
                        Id = "1",
                        UserName = "user1",
                        Email ="aa@abv.bg"
                    });

                    //2
                    arrangeContext.Users.Add(
                        new AppUser
                        {
                            Id = "2",
                            UserName = "user2",
                            Email = "bb@yahoo.com"
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

                    IList<string> roles = new List<string> { "Manager" };
                    userManagerMocked.Setup(x => x.GetRolesAsync(user))
                     .ReturnsAsync(roles);

                    var sut = new AppUserServices(arrangeContext, userManagerMocked.Object);
                   
                    var result = await sut.GetUserDToByID("3");

                    Assert.AreEqual("user3", result.Username);
                    Assert.AreEqual("cc@google.com", result.Email);
                    Assert.AreEqual("3", result.Id);
                    Assert.AreEqual("Manager", result.Role);
                    Assert.IsInstanceOfType(result, typeof(AppUserDTO));
                }
            }

            [TestMethod]
            public async Task Throw_MagicExeption_NullValueId_Passed()
            {
                var options = TestUtils.GetOptions(nameof(Throw_MagicExeption_NullValueId_Passed));

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

                    var result = Assert.ThrowsExceptionAsync<MagicException>
                        (async () => await sut.GetUserDToByID(null));
                }
            }
            [TestMethod]
            public async Task Throw_CorrectMagicExeption_NullValueId_Passed()
            {
                var options = TestUtils.GetOptions(nameof(Throw_CorrectMagicExeption_NullValueId_Passed));

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

                    var result = await Assert.ThrowsExceptionAsync<MagicException>
                        (async () => await sut.GetUserDToByID(null));
                    Assert.AreEqual("ID cannot be null!", result.Message);
                }
            }

            [TestMethod]
            public async Task Throw_MagicExeption_IfUser_WithThisID_DoesNotExist()
            {
                var options = TestUtils.GetOptions(nameof(Throw_MagicExeption_IfUser_WithThisID_DoesNotExist));

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

                    var result = await Assert.ThrowsExceptionAsync<MagicException>
                        (async () => await sut.GetUserDToByID("dasda"));
                }
            }

            [TestMethod]
            public async Task Throw_CorrectMagicExeption_IfUser_WithThisID_DoesNotExist()
            {
                var options = TestUtils.GetOptions(nameof(Throw_CorrectMagicExeption_IfUser_WithThisID_DoesNotExist));

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

                    var result = await Assert.ThrowsExceptionAsync<MagicException>
                        (async () => await sut.GetUserByID("dasda"));
                    Assert.AreEqual("AppUser cannot be null!", result.Message);
                }
            }
        }
    }
}
