using CM.Data;
using CM.Models;
using CM.Services.Contracts;
using CM.Services.CustomExceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Linq;
using System.Threading.Tasks;

namespace CM.Services.Tests.BarServicesTests
{
    [TestClass]
    public class Delete_Should
    {
        [TestMethod]
        public async Task ThrowMagicExeption_IfNullId_Passed()
        {
            var options = TestUtils.GetOptions(nameof(ThrowMagicExeption_IfNullId_Passed));

            var fileService = new Mock<IFileUploadService>();

            using (var assertContext = new CMContext(options))
            {
                var sut = new BarServices(assertContext, fileService.Object);

                var ex = await Assert.ThrowsExceptionAsync<MagicException>(
                   async () => await sut.Delete(null));

            }
        }
        [TestMethod]
        public async Task ThrowCorrectMagicExeption_IfNullId_Passed()
        {
            var options = TestUtils.GetOptions(nameof(ThrowCorrectMagicExeption_IfNullId_Passed));

            var fileService = new Mock<IFileUploadService>();

            using (var assertContext = new CMContext(options))
            {
                var sut = new BarServices(assertContext, fileService.Object);

                var ex = await Assert.ThrowsExceptionAsync<MagicException>(
                   async () => await sut.Delete(null));
                Assert.AreEqual("ID cannot be null!", ex.Message);
            }
        }
        [TestMethod]
        public async Task ThrowMagicExeption_IfNullBar_Passed()
        {
            var options = TestUtils.GetOptions(nameof(ThrowMagicExeption_IfNullBar_Passed));

            var fileService = new Mock<IFileUploadService>();

            using (var assertContext = new CMContext(options))
            {
                var sut = new BarServices(assertContext, fileService.Object);

                var ex = await Assert.ThrowsExceptionAsync<MagicException>(
                   async () => await sut.Delete("dddd"));
               
            }
        }
        [TestMethod]
        public async Task ThrowCorrectMagicExeption_IfNullBar_Passed()
        {
            var options = TestUtils.GetOptions(nameof(ThrowCorrectMagicExeption_IfNullBar_Passed));

            var fileService = new Mock<IFileUploadService>();

            using (var assertContext = new CMContext(options))
            {
                var sut = new BarServices(assertContext, fileService.Object);

                var ex = await Assert.ThrowsExceptionAsync<MagicException>(
                   async () => await sut.Delete("dddd"));
                Assert.AreEqual("Bar cannot be null!", ex.Message);
            }
        }
        [TestMethod]
        public async Task SetDateDaletedToDateTimeNow_AndSaveChangesInDb_IfValidValues_Passed()
        {
            var options = TestUtils.GetOptions(nameof(ThrowCorrectMagicExeption_IfNullBar_Passed));

            var fileService = new Mock<IFileUploadService>();

            using (var assertContext = new CMContext(options))
            {
                var sut = new BarServices(assertContext, fileService.Object);
                assertContext.Bars.Add(new Bar { Id = "1", Name = "Bar" });
                await assertContext.SaveChangesAsync();
                Assert.AreEqual(null, assertContext.Bars.First().DateDeleted);
                await sut.Delete("1");
                Assert.AreNotEqual(null, assertContext.Bars.First().DateDeleted);
            }
        }
        [TestMethod]
        public async Task Return_NameOfDeletedBar_IfEverythingIsOk()
        {
            var options = TestUtils.GetOptions(nameof(Return_NameOfDeletedBar_IfEverythingIsOk));

            var fileService = new Mock<IFileUploadService>();

            using (var assertContext = new CMContext(options))
            {
                var sut = new BarServices(assertContext, fileService.Object);
                assertContext.Bars.Add(new Bar { Id = "1", Name = "BashBar" });
                await assertContext.SaveChangesAsync();
                var result = await sut.Delete("1");
                Assert.AreEqual(result, "BashBar");
            }
        }
    }
}
