using CM.Data;
using CM.Services.CustomExceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CM.Services.Tests.BarServicesTests
{
    [TestClass]
    public class Constructor_Should
    {
        [TestMethod]
        public async Task ThrowMagicExeption_IfNullValue_DbContextPassed()
        {
            var fileService = new Mock<IFileUploadService>();
            Assert.ThrowsException<MagicException>(
               () => new BarServices(null, fileService.Object));

        }
        [TestMethod]
        public async Task ThrowCorrectMagicExeption_IfNullValue_DbContextPassed()
        {
            var fileService = new Mock<IFileUploadService>();
            var ex = Assert.ThrowsException<MagicException>(
               () => new BarServices(null, fileService.Object));
            Assert.AreEqual("CMContext cannot be null!", ex.Message);
        }
        [TestMethod]
        public async Task ThrowMagicExeption_IfNullValue_IFileUploadServicePassed()
        {
            var options = TestUtils.GetOptions(nameof(ThrowMagicExeption_IfNullValue_IFileUploadServicePassed));

            Assert.ThrowsException<MagicException>(
               () => new BarServices(new CMContext(options), null));

        }
        [TestMethod]
        public async Task ThrowCorrectMagicExeption_IfNullValue_IFileUploadServicePassed()
        {
            var options = TestUtils.GetOptions(nameof(ThrowCorrectMagicExeption_IfNullValue_IFileUploadServicePassed));

            var ex = Assert.ThrowsException<MagicException>(
               () => new BarServices(new CMContext(options), null));
            Assert.AreEqual("IFileUploadService cannot be null!", ex.Message);
        }
        [TestMethod]
        public async Task MakeInstance_OfTypeBarService_IfValidValuesPassed()
        {
            var options = TestUtils.GetOptions(nameof(MakeInstance_OfTypeBarService_IfValidValuesPassed));

            var barService =  new BarServices(new CMContext(options),new Mock<IFileUploadService>().Object);

            Assert.IsInstanceOfType(barService,typeof(BarServices));
        }
    }
}
