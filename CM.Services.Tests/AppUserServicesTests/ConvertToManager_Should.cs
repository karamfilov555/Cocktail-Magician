using CM.Data;
using CM.Models;
using CM.Services.CustomExeptions;
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
    public class ConvertToManager_Should
    {
        [TestMethod]
        public async Task ChangeUserRole_toManager_WhenValidIdPassed()
        {
            var options = TestUtils.GetOptions(nameof(ChangeUserRole_toManager_WhenValidIdPassed));

            var userStoreMocked = new Mock<IUserStore<AppUser>>();
            var userManagerMocked =
                new Mock<UserManager<AppUser>>
                (userStoreMocked.Object, null, null, null, null, null, null, null, null);

            using (var assertContext = new CMContext(options))
            {
                var managerRole = new AppRole { Id = "1", Name = "Manager", NormalizedName = "MANAGER" };
                assertContext.Roles.Add(managerRole);

                var member = new AppUser
                {
                    Id = "1",
                    UserName = "user1",
                    ImageURL = "user1Img",
                    Email = "user1@mail",
                };
             
                assertContext.Users.Add(member);
                await assertContext.SaveChangesAsync();

                var roles = new List<string> { "Member", "bloger" };

                userManagerMocked.Setup(x => x.GetRolesAsync(member))
                      .ReturnsAsync(roles);
                userManagerMocked.Setup(x => x.RemoveFromRolesAsync(member,roles)); 
                userManagerMocked.Setup(x => x.AddToRoleAsync(member,"Manager")); 

                var sut = new AppUserServices(assertContext, userManagerMocked.Object);

                await sut.ConvertToManager("1");

                userManagerMocked.Verify(x => x.GetRolesAsync(member), Times.Once);
                userManagerMocked.Verify(x => x.RemoveFromRolesAsync(member,roles), Times.Once);
                userManagerMocked.Verify(x => x.AddToRoleAsync(member,"Manager"), Times.Once);
            }
        }

        [TestMethod]
        public async Task ThrowMagicExeption_IfNullId_Passed()
        {
            var options = TestUtils.GetOptions(nameof(ThrowMagicExeption_IfNullId_Passed));

            var userStoreMocked = new Mock<IUserStore<AppUser>>();
            var userManagerMocked =
                new Mock<UserManager<AppUser>>
                (userStoreMocked.Object, null, null, null, null, null, null, null, null);

            using (var assertContext = new CMContext(options))
            {
                var sut = new AppUserServices(assertContext, userManagerMocked.Object);
                var ex = await Assert.ThrowsExceptionAsync<MagicExeption>
                    (async () => await sut.ConvertToManager(null));
            }
        }
        [TestMethod]
        public async Task ThrowCorrectMagicExeption_IfNullId_Passed()
        {
            var options = TestUtils.GetOptions(nameof(ThrowCorrectMagicExeption_IfNullId_Passed));

            var userStoreMocked = new Mock<IUserStore<AppUser>>();
            var userManagerMocked =
                new Mock<UserManager<AppUser>>
                (userStoreMocked.Object, null, null, null, null, null, null, null, null);

            using (var assertContext = new CMContext(options))
            {
                var sut = new AppUserServices(assertContext, userManagerMocked.Object);
                var ex = await Assert.ThrowsExceptionAsync<MagicExeption>
                    (async () => await sut.ConvertToManager(null));
                Assert.AreEqual("ID cannot be null!", ex.Message);
            }
        }
        [TestMethod]
        public async Task ThrowCorrectMagicExeption_IfAppUser_WichDoesntExist_Passed()
        {
            var options = TestUtils.GetOptions(nameof(ThrowCorrectMagicExeption_IfAppUser_WichDoesntExist_Passed));

            var userStoreMocked = new Mock<IUserStore<AppUser>>();
            var userManagerMocked =
                new Mock<UserManager<AppUser>>
                (userStoreMocked.Object, null, null, null, null, null, null, null, null);

            using (var assertContext = new CMContext(options))
            {
                var sut = new AppUserServices(assertContext, userManagerMocked.Object);
                var ex = await Assert.ThrowsExceptionAsync<MagicExeption>
                    (async () => await sut.ConvertToManager("dasdad"));
                Assert.AreEqual("AppUser cannot be null!", ex.Message);
            }
        }
        [TestMethod]
        public async Task ThrowMagicExeption_IfAppUser_WichDoesntExist_Passed()
        {
            var options = TestUtils.GetOptions(nameof(ThrowMagicExeption_IfAppUser_WichDoesntExist_Passed));

            var userStoreMocked = new Mock<IUserStore<AppUser>>();
            var userManagerMocked =
                new Mock<UserManager<AppUser>>
                (userStoreMocked.Object, null, null, null, null, null, null, null, null);

            using (var assertContext = new CMContext(options))
            {
                var sut = new AppUserServices(assertContext, userManagerMocked.Object);
                var ex = await Assert.ThrowsExceptionAsync<MagicExeption>
                    (async () => await sut.ConvertToManager("dasdad"));
            }
        }
    }
}
