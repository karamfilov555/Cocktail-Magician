using CM.Data;
using CM.DTOs;
using CM.Models;
using CM.Services.CustomExceptions;
using Microsoft.AspNetCore.Http;
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
    public class Update_Should
    {
        private string barId = "1";
        private string barName = "BarName";
        private string barWebsite = "https://www.abvb.bg";
        private double barRating = 2.5;

        [TestMethod]
        public async Task UpdateBarProperties_AndSaveItToDb_WhenValidValuesPassed()
        {
            var options = TestUtils.GetOptions(nameof(UpdateBarProperties_AndSaveItToDb_WhenValidValuesPassed));

            var fileService = new Mock<IFileUploadService>();

            using (var assertContext = new CMContext(options))
            {
                var sut = new BarServices(assertContext, fileService.Object);
                var file = new Mock<IFormFile>().Object;
                assertContext.Bars.Add(new Bar { Id = "1", Name = "BashBar", Website = "abv.bg", Address = new Address {City = "Sofia",Id = "1" } });
                await assertContext.SaveChangesAsync();
                var result = await sut.Update(
                    new BarDTO
                    {
                        Id = "1",
                        Name = barName,
                        Website = barWebsite,
                        BarImage = file,
                    });
                Assert.AreEqual(barName, assertContext.Bars.First().Name);
                Assert.AreEqual(barWebsite, assertContext.Bars.First().Website);
            }
        }

        
        [TestMethod]
        public async Task ThrowMagicExeption_WhenNullBarDto_IsPassed()
        {
            var options = TestUtils.GetOptions(nameof(ThrowMagicExeption_WhenNullBarDto_IsPassed));

            var fileService = new Mock<IFileUploadService>();

            using (var assertContext = new CMContext(options))
            {
                var sut = new BarServices(assertContext, fileService.Object);
                var ex = await Assert.ThrowsExceptionAsync<MagicException>(
                    async () => await sut.Update(null));
            }
        }
        [TestMethod]
        public async Task ThrowCorrectMagicExeption_WhenNullBarDto_IsPassed()
        {
            var options = TestUtils.GetOptions(nameof(ThrowCorrectMagicExeption_WhenNullBarDto_IsPassed));

            var fileService = new Mock<IFileUploadService>();

            using (var assertContext = new CMContext(options))
            {
                var sut = new BarServices(assertContext, fileService.Object);
                var ex = await Assert.ThrowsExceptionAsync<MagicException>(
                    async () => await sut.Update(null));
                Assert.AreEqual("BarDto cannot be null!", ex.Message);
            }
        }
        [TestMethod]
        public async Task ThrowMagicExeption_WhenBarDtoIdDoesntExist()
        {
            var options = TestUtils.GetOptions(nameof(ThrowMagicExeption_WhenBarDtoIdDoesntExist));

            var fileService = new Mock<IFileUploadService>();
            var barDto = new BarDTO { Id = "ddd", Name = "dto" };
            using (var assertContext = new CMContext(options))
            {
                var sut = new BarServices(assertContext, fileService.Object);
                var ex = await Assert.ThrowsExceptionAsync<MagicException>(
                    async () => await sut.Update(barDto));
            }
        }
        [TestMethod]
        public async Task ThrowCorrectMagicExeption_WhenBarDtoIdDoesntExist()
        {
            var options = TestUtils.GetOptions(nameof(ThrowCorrectMagicExeption_WhenBarDtoIdDoesntExist));

            var fileService = new Mock<IFileUploadService>();
            var barDto = new BarDTO { Id = "ddd", Name = "dto" };
            using (var assertContext = new CMContext(options))
            {
                var sut = new BarServices(assertContext, fileService.Object);
                var ex = await Assert.ThrowsExceptionAsync<MagicException>(
                    async () => await sut.Update(barDto));
                Assert.AreEqual("Bar cannot be null!", ex.Message);
            }
        }

        [TestMethod]
        public async Task UpdateDefaultImage_WhenBarDtoImagePathIsNull()
        {
            var options = TestUtils.GetOptions(nameof(UpdateDefaultImage_WhenBarDtoImagePathIsNull));

            var fileService = new Mock<IFileUploadService>();
            fileService.Setup(f => f.SetUniqueImagePath(null)).Returns("/images/defaultBarImage.jpg");
            using (var assertContext = new CMContext(options))
            {
                assertContext.Bars.Add(new Bar { Id = "1", Name = "BashBar", Website = "abv.bg", Address = new Address { City = "Sofia", Id = "1" } });
                await assertContext.SaveChangesAsync();
                var expectedPath = "/images/defaultBarImage.jpg";
                var sut = new BarServices(assertContext, fileService.Object);
                var result = await sut.Update(
                    new BarDTO
                    {
                        Id = "1",
                        Name = barName,
                        Website = barWebsite,
        
                    });
                Assert.AreEqual(expectedPath, assertContext.Bars.First().Image);
            }
        }
        [TestMethod]
        public async Task Invoke_SetImageMethod_WhenBarDtoImageIsValid()
        {
            var options = TestUtils.GetOptions(nameof(Invoke_SetImageMethod_WhenBarDtoImageIsValid));

            var fileService = new Mock<IFileUploadService>();
            fileService.Setup(f => f.SetUniqueImagePath(null));

            using (var assertContext = new CMContext(options))
            {
                assertContext.Bars.Add(new Bar { Id = "1", Name = "BashBar", Website = "abv.bg", Address = new Address { City = "Sofia", Id = "1" } });
                await assertContext.SaveChangesAsync();
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
                await sut.Update(barDto);
                fileService.Verify(x => x.SetUniqueImagePath(barDto.BarImage), Times.AtLeastOnce);
            }
        }

        [TestMethod]
        public async Task Return_UpdatedBarName_WhenEverythingIsOk()
        {
            var options = TestUtils.GetOptions(nameof(Return_UpdatedBarName_WhenEverythingIsOk));

            var fileService = new Mock<IFileUploadService>();

            using (var assertContext = new CMContext(options))
            {
                assertContext.Bars.Add(new Bar { Id = "1", Name = "BashBar", Website = "abv.bg", Address = new Address { City = "Sofia", Id = "1" } });
                await assertContext.SaveChangesAsync();
                var sut = new BarServices(assertContext, fileService.Object);
                var result = await sut.Update(
                    new BarDTO
                    {
                        Id = "1",
                        Name = barName,
                        Website = barWebsite,
                        BarImage = new Mock<IFormFile>().Object,
                    });
                Assert.AreEqual(barName, result);
            }
        }

        [TestMethod]
        public async Task UpdateAll_ManyToManyConnectionsToCocktails_OfBar_InDb()
        {
            var options = TestUtils.GetOptions(nameof(UpdateAll_ManyToManyConnectionsToCocktails_OfBar_InDb));

            var fileService = new Mock<IFileUploadService>();

            using (var assertContext = new CMContext(options))
            {
                assertContext.Bars.Add(new Bar { Id = "1", Name = "BashBar", Website = "abv.bg", Address = new Address { City = "Sofia", Id = "1" } });
                await assertContext.SaveChangesAsync();
                var sut = new BarServices(assertContext, fileService.Object);
                var result = await sut.Update(
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
                                                  .First(x => x.CocktailId == "1").CocktailId);
                Assert.AreEqual("2", assertContext.BarCocktails
                                                  .First(x => x.CocktailId == "2").CocktailId);
                Assert.AreEqual("1", assertContext.BarCocktails
                                                  .First(x => x.CocktailId == "2").BarId);
                Assert.AreEqual("1", assertContext.BarCocktails
                                                  .First(x => x.CocktailId == "1").BarId);
            }
        }
    }
}
