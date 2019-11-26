using CM.Data;
using CM.Models;
using CM.Services.CustomExceptions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CM.Services.Tests.AppUserServicesTests
{
    [TestClass]
    public class GetAdmin_Should
    {
        [TestMethod]
        public async Task Return_AppUser_WithRoleAdministrator_WhenEverythingIsOk()
        {
            var options = TestUtils.GetOptions(nameof(Return_AppUser_WithRoleAdministrator_WhenEverythingIsOk));

            var userStoreMocked = new Mock<IUserStore<AppUser>>();
            var userManagerMocked =
                new Mock<UserManager<AppUser>>
                (userStoreMocked.Object, null, null, null, null, null, null, null, null);

            using (var assertContext = new CMContext(options))
            {
                var member = new AppUser { Id = "2", UserName = "Member" };
                var manager = new AppUser { Id = "1", UserName = "Manager" };
                var admin = new AppUser { Id = "3", UserName = "Admin" };
                assertContext.Users.Add(admin);
                assertContext.Users.Add(member);
                assertContext.Users.Add(manager);
                var role = new AppRole { Id = "1", Name = "Administrator", NormalizedName = "ADMINISTRATOR" };

                assertContext.Roles.Add(role);
                assertContext.UserRoles.Add(
                    new IdentityUserRole<string>
                    {
                        RoleId = "1",
                        UserId = "3"
                    });
                await assertContext.SaveChangesAsync();
                var sut = new AppUserServices(assertContext, userManagerMocked.Object);
                var result = await sut.GetAdmin();

                Assert.AreEqual("3", result.Id);
                Assert.AreEqual("Admin", result.UserName);
                Assert.IsInstanceOfType(result,typeof(AppUser));
            }
        }


        [TestMethod]
        public async Task ThrowCorrectMagicExeption_IfAppUser_DoesnotExist()
        {
            var options = TestUtils.GetOptions(nameof(ThrowCorrectMagicExeption_IfAppUser_DoesnotExist));

            var userStoreMocked = new Mock<IUserStore<AppUser>>();
            var userManagerMocked =
                new Mock<UserManager<AppUser>>
                (userStoreMocked.Object, null, null, null, null, null, null, null, null);

            using (var assertContext = new CMContext(options))
            {
                var role = new AppRole {Id = "1", Name = "Administrator", NormalizedName = "ADMINISTRATOR" };

                assertContext.Roles.Add(role);
                assertContext.UserRoles.Add(
                    new IdentityUserRole<string>
                    {
                        RoleId = "1",
                        UserId = "1"
                    });
                await assertContext.SaveChangesAsync();
                var sut = new AppUserServices(assertContext, userManagerMocked.Object);
                var ex = await Assert.ThrowsExceptionAsync<MagicException>
                    (async () => await sut.GetAdmin());
                Assert.AreEqual("AppUser cannot be null!",ex.Message);
            }
        }
        [TestMethod]
        public async Task ThrowMagicExeption_IfAppUser_DoesnotExist()
        {
            var options = TestUtils.GetOptions(nameof(ThrowMagicExeption_IfAppUser_DoesnotExist));

            var userStoreMocked = new Mock<IUserStore<AppUser>>();
            var userManagerMocked =
                new Mock<UserManager<AppUser>>
                (userStoreMocked.Object, null, null, null, null, null, null, null, null);

            using (var assertContext = new CMContext(options))
            {
                var role = new AppRole { Id = "1", Name = "Administrator", NormalizedName = "ADMINISTRATOR" };

                assertContext.Roles.Add(role);
                assertContext.UserRoles.Add(
                    new IdentityUserRole<string>
                    {
                        RoleId = "1",
                        UserId = "1"
                    });
                await assertContext.SaveChangesAsync();
                var sut = new AppUserServices(assertContext, userManagerMocked.Object);
                var ex = await Assert.ThrowsExceptionAsync<MagicException>
                    (async () => await sut.GetAdmin());
            }
        }


        [TestMethod]
        public async Task ThrowMagicExeption_IfUserRoleConnection_DoesnotExist()
        {
            var options = TestUtils.GetOptions(nameof(ThrowMagicExeption_IfUserRoleConnection_DoesnotExist));

            var userStoreMocked = new Mock<IUserStore<AppUser>>();
            var userManagerMocked =
                new Mock<UserManager<AppUser>>
                (userStoreMocked.Object, null, null, null, null, null, null, null, null);

            using (var assertContext = new CMContext(options))
            {
                var role = new AppRole { Name = "Administrator", NormalizedName = "ADMINISTRATOR" };
                assertContext.Roles.Add(role);
                await assertContext.SaveChangesAsync();
                var sut = new AppUserServices(assertContext, userManagerMocked.Object);
                var ex = await Assert.ThrowsExceptionAsync<MagicException>
                    (async () => await sut.GetAdmin());
            }
        }
        [TestMethod]
        public async Task ThrowCorrectMagicExeption_IfUserRoleConnection_DoesnotExist()
        {
            var options = TestUtils.GetOptions(nameof(ThrowMagicExeption_IfUserRoleConnection_DoesnotExist));

            var userStoreMocked = new Mock<IUserStore<AppUser>>();
            var userManagerMocked =
                new Mock<UserManager<AppUser>>
                (userStoreMocked.Object, null, null, null, null, null, null, null, null);

            using (var assertContext = new CMContext(options))
            {
                var role = new AppRole { Name = "Administrator", NormalizedName = "ADMINISTRATOR" };
                assertContext.Roles.Add(role);
                await assertContext.SaveChangesAsync();
                var sut = new AppUserServices(assertContext, userManagerMocked.Object);
                var ex = await Assert.ThrowsExceptionAsync<MagicException>
                    (async () => await sut.GetAdmin());
                Assert.AreEqual("UserRole cannot be null!", ex.Message);
            }
        }

        [TestMethod]
        public async Task ThrowMagicExeption_IfAppRole_DoesnotExist()
        {
            var options = TestUtils.GetOptions(nameof(ThrowMagicExeption_IfAppRole_DoesnotExist));

            var userStoreMocked = new Mock<IUserStore<AppUser>>();
            var userManagerMocked =
                new Mock<UserManager<AppUser>>
                (userStoreMocked.Object, null, null, null, null, null, null, null, null);

            using (var assertContext = new CMContext(options))
            {
                var sut = new AppUserServices(assertContext, userManagerMocked.Object);
                var ex = await Assert.ThrowsExceptionAsync<MagicException>
                    (async () => await sut.GetAdmin());
            }
        }
        [TestMethod]
        public async Task ThrowCorrectMagicExeption_IfAppRole_DoesnotExist()
        {
            var options = TestUtils.GetOptions(nameof(ThrowCorrectMagicExeption_IfAppRole_DoesnotExist));

            var userStoreMocked = new Mock<IUserStore<AppUser>>();
            var userManagerMocked =
                new Mock<UserManager<AppUser>>
                (userStoreMocked.Object, null, null, null, null, null, null, null, null);

            using (var assertContext = new CMContext(options))
            {
                var sut = new AppUserServices(assertContext, userManagerMocked.Object);
                var ex = await Assert.ThrowsExceptionAsync<MagicException>
                    (async () => await sut.GetAdmin());
                Assert.AreEqual("AppRole cannot be null!", ex.Message);
            }
        }
    }
}
