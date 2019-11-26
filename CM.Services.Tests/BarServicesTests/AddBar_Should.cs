using CM.Data;
using CM.DTOs;
using CM.DTOs.Mappers;
using CM.Models;
using CM.Services.Contracts;
using CM.Services.CustomExceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
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
        [TestMethod]
        public async Task ThrowMagicExeption_WhenNullValue_IsPassed()
        {
            var options = TestUtils.GetOptions(nameof(ThrowMagicExeption_WhenNullValue_IsPassed));

            var fileService = new Mock<IFileUploadService>();

            using (var assertContext = new CMContext(options))
            {
                var sut = new BarServices(assertContext, fileService.Object);
                var ex = await Assert.ThrowsExceptionAsync<MagicException>(
                    async () => await sut.AddBarAsync(null));
            }
        }
        [TestMethod]
        public async Task ThrowCorrectMagicExeption_WhenNullValue_IsPassed()
        {
            var options = TestUtils.GetOptions(nameof(ThrowCorrectMagicExeption_WhenNullValue_IsPassed));

            var fileService = new Mock<IFileUploadService>();

            using (var assertContext = new CMContext(options))
            {
                var sut = new BarServices(assertContext, fileService.Object);
                var ex = await Assert.ThrowsExceptionAsync<MagicException>(
                    async () => await sut.AddBarAsync(null));
                Assert.AreEqual("BarDto cannot be null!", ex.Message);
            }
        }
        [TestMethod]
        public async Task SetDefaultImage_WhenBarDtoImagePathIsNull()
        {
            var options = TestUtils.GetOptions(nameof(SetDefaultImage_WhenBarDtoImagePathIsNull));
            var file = new Mock<IFormFile>().Object;
            var fileService = new Mock<IFileUploadService>();
            fileService.Setup(f => f.SetUniqueImagePath(null)).Returns("/images/defaultBarImage.jpg");

            using (var assertContext = new CMContext(options))
            {
                var expectedPath = "/images/defaultBarImage.jpg";
                var sut = new BarServices(assertContext, fileService.Object);
                var result = await sut.AddBarAsync(
                    new BarDTO
                    {
                        Id = barId,
                        Name = barName,
                        Website = barWebsite,
                    });
                Assert.AreEqual(expectedPath, assertContext.Bars.First().Image);
            }
        }
        [TestMethod]
        public async Task InvokeUploadImageMethod_WhenBarDtoImageIsValid()
        {
            var options = TestUtils.GetOptions(nameof(InvokeUploadImageMethod_WhenBarDtoImageIsValid));

            var fileService = new Mock<IFileUploadService>();

            using (var assertContext = new CMContext(options))
            {
                var sut = new BarServices(assertContext, fileService.Object);
                var barDto = 
                    new BarDTO
                    {
                        Id = barId,
                        Name = barName,
                        ImageUrl = "abv.bg",
                        Website = barWebsite,
                        BarImage = new Mock<IFormFile>().Object,
                    };
                await sut.AddBarAsync(barDto);
                fileService.Verify(x => x.SetUniqueImagePath(barDto.BarImage), Times.AtLeastOnce);
            }
        }

        [TestMethod]
        public async Task Return_NewAddedBarName_WhenEverythingIsOk()
        {
            var options = TestUtils.GetOptions(nameof(Return_NewAddedBarName_WhenEverythingIsOk));

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
                Assert.AreEqual(barName, result);
            }
        }

        [TestMethod]
        public async Task AddAll_ManyToManyConnectionsToCocktails_Of_NewBar_InDb()
        {
            var options = TestUtils.GetOptions(nameof(AddAll_ManyToManyConnectionsToCocktails_Of_NewBar_InDb));

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
                        Cocktails = new List<CocktailDto>
                        {
                            new CocktailDto
                            {
                                Id = "1",
                                Name = "cocktail",
                            },
                            new CocktailDto
                            {
                                Id = "2",
                                Name = "cocktail2",
                            }
                        }
                    });

                Assert.AreEqual(2, assertContext.BarCocktails.Count());

                Assert.AreEqual("1", assertContext.BarCocktails
                                                  .First( x => x.CocktailId == "1").CocktailId);
                Assert.AreEqual("2", assertContext.BarCocktails
                                                  .First( x => x.CocktailId == "2").CocktailId);
                Assert.AreEqual("1", assertContext.BarCocktails
                                                  .First(x => x.CocktailId == "2").BarId);
                Assert.AreEqual("1", assertContext.BarCocktails
                                                  .First(x => x.CocktailId == "1").BarId);
            }
        }
    }
}
