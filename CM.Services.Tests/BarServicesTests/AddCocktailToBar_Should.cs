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
    public class AddCocktailToBar_Should
    {
        [TestMethod]
        public async Task ThrowMagicExeption_IfNullValue_Cocktail_Passed()
        {
            var options = TestUtils.GetOptions(nameof(ThrowMagicExeption_IfNullValue_Cocktail_Passed));

            var fileService = new Mock<IFileUploadService>();

            using (var assertContext = new CMContext(options))
            {
                var sut = new BarServices(assertContext, fileService.Object);
                
                var ex = await Assert.ThrowsExceptionAsync<MagicException>(
                   async () => await sut.AddCocktailToBar(null, null)
                    );
            }
        }
        [TestMethod]
        public async Task ThrowCorrectMagicExeption_IfNullValue_Cocktail_Passed()
        {
            var options = TestUtils.GetOptions(nameof(ThrowCorrectMagicExeption_IfNullValue_Cocktail_Passed));

            var fileService = new Mock<IFileUploadService>();

            using (var assertContext = new CMContext(options))
            {
                var sut = new BarServices(assertContext, fileService.Object);

                var ex = await  Assert.ThrowsExceptionAsync<MagicException>(
                   async  () => await sut.AddCocktailToBar(null, null));

                Assert.AreEqual("Cocktail cannot be null!", ex.Message);
            }
        }

        [TestMethod]
        public async Task ThrowMagicExeption_IfNullValue_Bar_Passed()
        {
            var options = TestUtils.GetOptions(nameof(ThrowMagicExeption_IfNullValue_Bar_Passed));

            var fileService = new Mock<IFileUploadService>();

            using (var assertContext = new CMContext(options))
            {
                var sut = new BarServices(assertContext, fileService.Object);

                var ex = await Assert.ThrowsExceptionAsync<MagicException>(
                  async () => await sut.AddCocktailToBar(null, null));

            }
        }
        [TestMethod]
        public async Task ThrowCorrectMagicExeption_IfNullValue_Bar_Passed()
        {
            var options = TestUtils.GetOptions(nameof(ThrowCorrectMagicExeption_IfNullValue_Bar_Passed));

            var fileService = new Mock<IFileUploadService>();

            using (var assertContext = new CMContext(options))
            {
                var sut = new BarServices(assertContext, fileService.Object);

                var ex = await Assert.ThrowsExceptionAsync<MagicException>(
                    async () => await sut.AddCocktailToBar(new Cocktail { Id ="1",Name="cocktail"}, null));
                Assert.AreEqual("Bar cannot be null!", ex.Message);
            }
        }

        [TestMethod]
        public async Task AddNew_BarCocktailConnectionInDb_WhenValidModelsPassed()
        {
            var options = TestUtils.GetOptions(nameof(AddNew_BarCocktailConnectionInDb_WhenValidModelsPassed));

            var fileService = new Mock<IFileUploadService>();
            var bar = new Bar
            {
                Id = "112",
                Name = "Bar",
                BarCocktails = new List<BarCocktail> { new BarCocktail { BarId = "112", CocktailId = "2" } }
            };
            var cocktail = new Cocktail
            {
                Id = "151",
                Name = "Cocktail"
            };
            using (var assertContext = new CMContext(options))
            {
                var sut = new BarServices(assertContext, fileService.Object);
                assertContext.Bars.Add(bar);
                assertContext.Cocktails.Add(cocktail);

                await sut.AddCocktailToBar(cocktail, bar);
                    

                await assertContext.SaveChangesAsync();

                Assert.AreEqual(2, assertContext.BarCocktails.Count());

                Assert.AreEqual("151", assertContext.BarCocktails
                                                  .First(x=>x.BarId == "112" &&                                                     x.CocktailId=="151").CocktailId);
                
                
            }
        }
        [TestMethod]
        public async Task DontAddNew_BarCocktailConnectionInDb_IfThisCocktailAlreadyExistInBar()
        {
            var options = TestUtils.GetOptions(nameof(DontAddNew_BarCocktailConnectionInDb_IfThisCocktailAlreadyExistInBar));

            var fileService = new Mock<IFileUploadService>();
            var bar = new Bar
            {
                Id = "112",
                Name = "Bar",
                BarCocktails = new List<BarCocktail>
                { new BarCocktail { BarId = "112", CocktailId = "2" } }
            };
            var cocktail = new Cocktail
            {
                Id = "2",
                Name = "Cocktail"
            };
            using (var assertContext = new CMContext(options))
            {
                var sut = new BarServices(assertContext, fileService.Object);
                assertContext.Bars.Add(bar);
                assertContext.Cocktails.Add(cocktail);

                await sut.AddCocktailToBar(cocktail, bar);


                await assertContext.SaveChangesAsync();

                Assert.AreEqual(1, assertContext.BarCocktails.Count());

                Assert.AreEqual("2", assertContext.BarCocktails
                                                  .First().CocktailId);


            }
        }
    }
}
