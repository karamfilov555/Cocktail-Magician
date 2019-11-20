using CM.Data;
using CM.DTOs;
using CM.DTOs.Mappers;
using CM.Models;
using CM.Services.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Linq;
using System.Threading.Tasks;

namespace CM.Services.Tests.BarServicesTests
{
    [TestClass]
    public class AddBarAsync_Should
    {
        private string barId = "1";
        private string barName = "BarName";
        private string barWebsite = "https://www.abvb.bg";
        private double barRating = 2.5;

        [TestMethod]
        public async Task AddBarToDb_WhenValidBarDTOPassed()
        {
            var options = TestUtils.GetOptions(nameof(AddBarToDb_WhenValidBarDTOPassed));

            var fileService = new Mock<IFileUploadService>();

            using (var assertContext = new CMContext(options))
            {
                var sut = new BarServices(assertContext, fileService.Object);
                var result = await sut.AddBarAsync(
                    new BarDTO {
                        Id = barId,
                        Name = barName,
                        Website = barWebsite,
                        BarImage = new Mock<IFormFile>().Object,
                    });
                Assert.AreEqual(1, assertContext.Bars.Count());
            }
        }

        [TestMethod]
        public async Task AddBarToDb_WithValidDtoParams()
        {
            var options = TestUtils.GetOptions(nameof(AddBarToDb_WithValidDtoParams));

            var fileService = new Mock<IFileUploadService>();

            using (var assertContext = new CMContext(options))
            {
                var sut = new BarServices(assertContext, fileService.Object);
                var result = await sut.AddBarAsync(
                    new BarDTO
                    {
                        Id = barId,
                        Name = barName,
                        Website = barWebsite,
                        BarImage = new Mock<IFormFile>().Object,
                    });
                Assert.AreEqual(barName, assertContext.Bars.First().Name);
                Assert.AreEqual(barWebsite, assertContext.Bars.First().Website);
                Assert.AreEqual(barId, assertContext.Bars.First().Id);
            }
        }

       
    }
}
