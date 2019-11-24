using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using Moq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using CM.Models;
using CM.Data;
using CM.DTOs;
using CM.DTOs.Mappers;
using System.Linq;

namespace CM.Services.Tests.AppUserServicesTests
{
    [TestClass]
    public class GetAllUsers_Should
    {
        [TestMethod]
        public async Task Return_ICollectionOfAll_AppUsersDtos_WithCorrectAttributes()
        {
            var options = TestUtils.GetOptions(nameof(Return_ICollectionOfAll_AppUsersDtos_WithCorrectAttributes));

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

                var roles = new List<string> {"Manager" };
                var roles2 = new List<string> {"Admin" };
                userManagerMocked.Setup(x => x.GetRolesAsync(manager))
                      .ReturnsAsync(roles);
                userManagerMocked.Setup(y => y.GetRolesAsync(admin))
                      .ReturnsAsync(roles2);

                var sut = new AppUserServices(assertContext, userManagerMocked.Object);

                var result = await sut.GetAllUsers();

                Assert.AreEqual(2, result.Count);
                Assert.AreEqual("Manager", result.First(x=>x.Id=="1").Role);
                Assert.AreEqual("user1", result.First(x=>x.Id=="1").Username);
                Assert.AreEqual("user1@mail", result.First(x=>x.Id=="1").Email);
                Assert.AreEqual("user1Img", result.First(x=>x.Id=="1").ImageURL);
                Assert.AreEqual("Admin", result.First(x=>x.Id=="2").Role);
                Assert.AreEqual("user2", result.First(x=>x.Id=="2").Username);
                Assert.AreEqual("user2@mail", result.First(x=>x.Id=="2").Email);
                Assert.AreEqual("user2Img", result.First(x=>x.Id=="2").ImageURL);
                Assert.IsInstanceOfType(result, typeof(ICollection<AppUserDTO>));
            }
        }

        [TestMethod]
        public async Task Return_OnlyAppUserDtosWich_AreNotDeleted()
        {
            var options = TestUtils.GetOptions(nameof(Return_OnlyAppUserDtosWich_AreNotDeleted));

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
                    DateDeleted = DateTime.Now
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

                var result = await sut.GetAllUsers();

                Assert.AreEqual(1, result.Count);
                Assert.AreEqual("Admin", result.First().Role);
                Assert.AreEqual("user2", result.First().Username);
                Assert.AreEqual("user2@mail", result.First().Email);
                Assert.AreEqual("user2Img", result.First().ImageURL);
                Assert.IsInstanceOfType(result, typeof(ICollection<AppUserDTO>));
            }
        }

        [TestMethod]
        public async Task Return_RoleName_NoRole_IfUserHasNoRole()
        {
            var options = TestUtils.GetOptions(nameof(Return_RoleName_NoRole_IfUserHasNoRole));

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

                var roles = new List<string> {  };
                userManagerMocked.Setup(x => x.GetRolesAsync(user))
                      .ReturnsAsync(roles);

                var sut = new AppUserServices(assertContext, userManagerMocked.Object);

                var result = await sut.GetAllUsers();

                Assert.AreEqual(1, result.Count);
                Assert.AreEqual("No role", result.First().Role);
                Assert.AreEqual("user1", result.First().Username);
                Assert.AreEqual("user1@mail", result.First().Email);
                Assert.AreEqual("user1Img", result.First().ImageURL);
                Assert.IsInstanceOfType(result, typeof(ICollection<AppUserDTO>));
            }
        }
    }
}
