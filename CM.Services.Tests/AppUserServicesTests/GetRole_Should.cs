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
    public class GetRole_Should
    {
        [TestMethod]
        public async Task Return_RoleAsString_WhenValidAppUserPassed()
        {
            var options = TestUtils.GetOptions(nameof(Return_RoleAsString_WhenValidAppUserPassed));

            var userStoreMocked = new Mock<IUserStore<AppUser>>();
            var userManagerMocked =
                new Mock<UserManager<AppUser>>
                (userStoreMocked.Object, null, null, null, null, null, null, null, null);

            using (var assertContext = new CMContext(options))
            {
                var managerRole = new AppRole { Id = "1", Name = "Manager", NormalizedName = "MANAGER" };
                var adminRole = new AppRole { Id = "2", Name = "Admin", NormalizedName = "ADMIN" };
                assertContext.Roles.Add(managerRole);
                assertContext.Roles.Add(adminRole);
                var manager = new AppUser
                {
                    Id = "1",
                    UserName = "user1",
                    ImageURL = "user1Img",
                    Email = "user1@mail",
                };
                var admin = new AppUser
                {
                    Id = "2",
                    UserName = "user2",
                    ImageURL = "user2Img",
                    Email = "user2@mail",
                };
                assertContext.Users.Add(manager);
                assertContext.Users.Add(admin);
                await assertContext.SaveChangesAsync();

                var roles = new List<string> { "Manager" };
                var roles2 = new List<string> { "Admin" };
                userManagerMocked.Setup(x => x.GetRolesAsync(manager))
                      .ReturnsAsync(roles);
                userManagerMocked.Setup(y => y.GetRolesAsync(admin))
                      .ReturnsAsync(roles2);

                var sut = new AppUserServices(assertContext, userManagerMocked.Object);

                var result = await sut.GetRole(manager);
                Assert.AreEqual("Manager", result);
                var result2 = await sut.GetRole(admin);
                Assert.AreEqual("Admin", result2);
                Assert.IsInstanceOfType(result, typeof(String));
            }
        }

        [TestMethod]
        public async Task Return_NoRoleString_IfUserHasNoRole()
        {
            var options = TestUtils.GetOptions(nameof(Return_NoRoleString_IfUserHasNoRole));

            var userStoreMocked = new Mock<IUserStore<AppUser>>();
            var userManagerMocked =
                new Mock<UserManager<AppUser>>
                (userStoreMocked.Object, null, null, null, null, null, null, null, null);

            using (var assertContext = new CMContext(options))
            {
                var managerRole = new AppRole { Id = "1", Name = "Manager", NormalizedName = "MANAGER" };
                var adminRole = new AppRole { Id = "2", Name = "Admin", NormalizedName = "ADMIN" };
                assertContext.Roles.Add(managerRole);
                assertContext.Roles.Add(adminRole);
                var user = new AppUser
                {
                    Id = "1",
                    UserName = "user1",
                    ImageURL = "user1Img",
                    Email = "user1@mail",
                };

                assertContext.Users.Add(user);
                await assertContext.SaveChangesAsync();

                var roles = new List<string> { };
                userManagerMocked.Setup(x => x.GetRolesAsync(user))
                      .ReturnsAsync(roles);

                var sut = new AppUserServices(assertContext, userManagerMocked.Object);

                var result = await sut.GetRole(user);

                Assert.AreEqual("No role", result);
                Assert.IsInstanceOfType(result, typeof(String));
            }
        }
        [TestMethod]
        public async Task Throw_MagicExeption_IfNullValue_AppUser_Passed()
        {
            var options = TestUtils.GetOptions(nameof(Throw_MagicExeption_IfNullValue_AppUser_Passed));

            var userStoreMocked = new Mock<IUserStore<AppUser>>();
            var userManagerMocked =
                new Mock<UserManager<AppUser>>
                (userStoreMocked.Object, null, null, null, null, null, null, null, null);

            using (var assertContext = new CMContext(options))
            {
                var sut = new AppUserServices(assertContext, userManagerMocked.Object);

                var ex = await  Assert.ThrowsExceptionAsync<MagicException>(
                    async()=> await sut.GetRole(null));
            }
        }
        [TestMethod]
        public async Task Throw_CorrectMagicExeption_IfNullValue_AppUser_Passed()
        {
            var options = TestUtils.GetOptions(nameof(Throw_CorrectMagicExeption_IfNullValue_AppUser_Passed));

            var userStoreMocked = new Mock<IUserStore<AppUser>>();
            var userManagerMocked =
                new Mock<UserManager<AppUser>>
                (userStoreMocked.Object, null, null, null, null, null, null, null, null);

            using (var assertContext = new CMContext(options))
            {
                var sut = new AppUserServices(assertContext, userManagerMocked.Object);

                var ex = await Assert.ThrowsExceptionAsync<MagicException>(
                    async () => await sut.GetRole(null));
                Assert.AreEqual("AppUser cannot be null!", ex.Message);
            }
        }
    }
}
