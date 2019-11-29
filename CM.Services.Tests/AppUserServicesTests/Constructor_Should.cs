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
    public class Constructor_Should
    {
        [TestMethod]
        public async Task ThrowMagicExeption_IfNullValue_DbContext_Passed()
        {
            var fileService = new Mock<IFileUploadService>();
            Assert.ThrowsException<MagicException>(
               () => new AppUserServices(null, null));

        }
        [TestMethod]
        public async Task ThrowCorrectMagicExeption_IfNullValue_DbContext_Passed()
        {
            var fileService = new Mock<IFileUploadService>();
            var ex = Assert.ThrowsException<MagicException>(
               () => new AppUserServices(null, null));
            Assert.AreEqual("CMContext cannot be null!", ex.Message);
        }
        [TestMethod]
        public async Task ThrowMagicExeption_IfNullValue_UserManager_Passed()
        {
            var options = TestUtils.GetOptions(nameof(ThrowMagicExeption_IfNullValue_UserManager_Passed));

            Assert.ThrowsException<MagicException>(
               () => new AppUserServices(new CMContext(options), null));

        }
        [TestMethod]
        public async Task ThrowMagicCorrectExeption_IfNullValue_UserManager_Passed()
        {
           
            var options = TestUtils.GetOptions(nameof(ThrowMagicCorrectExeption_IfNullValue_UserManager_Passed));

            var ex = Assert.ThrowsException<MagicException>(
               () => new AppUserServices(new CMContext(options), null));
            Assert.AreEqual("UserManager cannot be null!", ex.Message);
        }

        [TestMethod]
        public async Task MakeInstance_OfTypeAppUserService_IfValidValuesPassed()
        {
            var options = TestUtils.GetOptions(nameof(MakeInstance_OfTypeAppUserService_IfValidValuesPassed));

            var userStoreMocked = new Mock<IUserStore<AppUser>>();
            var userManagerMocked = 
                new Mock<UserManager<AppUser>>
                (userStoreMocked.Object, null, null, null, null, null, null, null, null);

            var userService = new AppUserServices(new CMContext(options), userManagerMocked.Object);

            Assert.IsInstanceOfType(userService, typeof(AppUserServices));
        }

    }
}
