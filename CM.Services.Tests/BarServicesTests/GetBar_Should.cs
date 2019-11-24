using CM.Data;
using CM.Models;
using CM.Services.CustomExeptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CM.Services.Tests.BarServicesTests
{
    [TestClass]
    public class GetBar_Should
    {
        [TestMethod]
        public async Task Return_BarCtxModel_WithCorrectAttributes_WhenValidIdPassed()
        {
            var options = TestUtils.GetOptions(nameof(Return_BarCtxModel_WithCorrectAttributes_WhenValidIdPassed));

            var fileService = new Mock<IFileUploadService>();

            using (var arrangeContext = new CMContext(options))
            {

                //1
                arrangeContext.Cocktails.Add(new Cocktail { Id = "1", Name = "cocktail" });
                arrangeContext.Bars.Add(
                    new Bar
                    {
                        Id = "1",
                        Name = "BashBar",
                        Address = new Address
                        {
                            Id = "1",
                            BarId = "1",
                            Country = new Country
                            {
                                Id = "1",
                                Name = "Bulgaria"
                            }
                        },

                        BarCocktails = new List<BarCocktail>
                        {
                            new BarCocktail
                            {
                                BarId = "1",
                                CocktailId = "1"
                            }
                        }
                    });
                //2
                arrangeContext.Bars.Add(
                   new Bar
                   {
                       Id = "2",
                       Name = "BashBar2",
                       Address = new Address
                       {
                           Id = "2",
                           BarId = "2",
                           Country = new Country
                           {
                               Id = "2",
                               Name = "Bulgaria"
                           }
                       },

                       BarCocktails = new List<BarCocktail>
                        {
                            new BarCocktail
                            {
                                BarId = "2",
                                CocktailId = "1"
                            }
                        }
                   });
                //3
                arrangeContext.Bars.Add(
                   new Bar
                   {
                       Id = "3",
                       Name = "Target",
                       Image = "Snimka",
                       Website = "abv.bg",
                       Address = new Address
                       {
                           Id = "3",
                           BarId = "3",
                           Country = new Country
                           {
                               Id = "3",
                               Name = "Bulgaria"
                           }
                       },

                       BarCocktails = new List<BarCocktail>
                        {
                            new BarCocktail
                            {
                                BarId = "3",
                                CocktailId = "1"
                            }
                        }
                       ,
                       BarRating = 3
                   });

                await arrangeContext.SaveChangesAsync();
            }
            using (var assertContext = new CMContext(options))
            {
                var sut = new BarServices(assertContext, fileService.Object);

                var result = await sut.GetBar("3");

                Assert.AreEqual("Target", result.Name);
                Assert.AreEqual("3", result.Id);
                Assert.AreEqual("abv.bg", result.Website);
                Assert.AreEqual("Snimka", result.Image);
                Assert.IsInstanceOfType(result,typeof(Bar));
            }
        }

        [TestMethod]
        public async Task ThrowMagicExeption_WhenNullIdPassed()
        {
            var options = TestUtils.GetOptions(nameof(ThrowMagicExeption_WhenNullIdPassed));

            var fileService = new Mock<IFileUploadService>();

            using (var arrangeContext = new CMContext(options))
            {
                //1
                arrangeContext.Cocktails.Add(new Cocktail { Id = "1", Name = "cocktail" });

                arrangeContext.Bars.Add(
                   new Bar
                   {
                       Id = "3",
                       Name = "Target",
                       Image = "Snimka",
                       Website = "abv.bg",
                       Address = new Address
                       {
                           Id = "3",
                           BarId = "3",
                           Country = new Country
                           {
                               Id = "3",
                               Name = "Bulgaria"
                           }
                       },

                       BarCocktails = new List<BarCocktail>
                        {
                            new BarCocktail
                            {
                                BarId = "3",
                                CocktailId = "1"
                            }
                        }
                       ,
                       BarRating = 3
                   });

                await arrangeContext.SaveChangesAsync();
            }
            using (var assertContext = new CMContext(options))
            {
                var sut = new BarServices(assertContext, fileService.Object);

                var ex = await Assert.ThrowsExceptionAsync<MagicExeption>(
                    async () => await sut.GetBar(null)
                    );
            }
        }

        [TestMethod]
        public async Task ThrowCorrectMagicMsg_WhenNullIdPassed()
        {
            var options = TestUtils.GetOptions(nameof(ThrowCorrectMagicMsg_WhenNullIdPassed));

            var fileService = new Mock<IFileUploadService>();

            using (var arrangeContext = new CMContext(options))
            {
                //1
                arrangeContext.Cocktails.Add(new Cocktail { Id = "1", Name = "cocktail" });

                arrangeContext.Bars.Add(
                   new Bar
                   {
                       Id = "3",
                       Name = "Target",
                       Image = "Snimka",
                       Website = "abv.bg",
                       Address = new Address
                       {
                           Id = "3",
                           BarId = "3",
                           Country = new Country
                           {
                               Id = "3",
                               Name = "Bulgaria"
                           }
                       },

                       BarCocktails = new List<BarCocktail>
                        {
                            new BarCocktail
                            {
                                BarId = "3",
                                CocktailId = "1"
                            }
                        }
                       ,
                       BarRating = 3
                   });

                await arrangeContext.SaveChangesAsync();
            }
            using (var assertContext = new CMContext(options))
            {
                var sut = new BarServices(assertContext, fileService.Object);

                var ex = await Assert.ThrowsExceptionAsync<MagicExeption>(
                    async () => await sut.GetBar(null)
                    );
                Assert.AreEqual("ID cannot be null!", ex.Message);
            }
        }

        [TestMethod]
        public async Task ThrowMagicExeption_WhenBarDoesNotExist()
        {
            var options = TestUtils.GetOptions(nameof(ThrowMagicExeption_WhenBarDoesNotExist));

            var fileService = new Mock<IFileUploadService>();

            using (var arrangeContext = new CMContext(options))
            {
                //1
                arrangeContext.Cocktails.Add(new Cocktail { Id = "1", Name = "cocktail" });

                arrangeContext.Bars.Add(
                   new Bar
                   {
                       Id = "3",
                       Name = "Target",
                       Image = "Snimka",
                       Website = "abv.bg",
                       Address = new Address
                       {
                           Id = "3",
                           BarId = "3",
                           Country = new Country
                           {
                               Id = "3",
                               Name = "Bulgaria"
                           }
                       },

                       BarCocktails = new List<BarCocktail>
                        {
                            new BarCocktail
                            {
                                BarId = "3",
                                CocktailId = "1"
                            }
                        }
                       ,
                       BarRating = 3
                   });

                await arrangeContext.SaveChangesAsync();
            }
            using (var assertContext = new CMContext(options))
            {
                var sut = new BarServices(assertContext, fileService.Object);

                var ex = await Assert.ThrowsExceptionAsync<MagicExeption>(
                    async () => await sut.GetBar("4")
                    );
               
            }
        }

        [TestMethod]
        public async Task ThrowCorrectMagicExeption_WhenBarDoesNotExist()
        {
            var options = TestUtils.GetOptions(nameof(ThrowCorrectMagicExeption_WhenBarDoesNotExist));

            var fileService = new Mock<IFileUploadService>();

            using (var arrangeContext = new CMContext(options))
            {
                //1
                arrangeContext.Cocktails.Add(new Cocktail { Id = "1", Name = "cocktail" });

                arrangeContext.Bars.Add(
                   new Bar
                   {
                       Id = "3",
                       Name = "Target",
                       Image = "Snimka",
                       Website = "abv.bg",
                       Address = new Address
                       {
                           Id = "3",
                           BarId = "3",
                           Country = new Country
                           {
                               Id = "3",
                               Name = "Bulgaria"
                           }
                       },

                       BarCocktails = new List<BarCocktail>
                        {
                            new BarCocktail
                            {
                                BarId = "3",
                                CocktailId = "1"
                            }
                        }
                       ,
                       BarRating = 3
                   });

                await arrangeContext.SaveChangesAsync();
            }
            using (var assertContext = new CMContext(options))
            {
                var sut = new BarServices(assertContext, fileService.Object);

                var ex = await Assert.ThrowsExceptionAsync<MagicExeption>(
                    async () => await sut.GetBar("4")
                    );
                Assert.AreEqual("Bar cannot be null!", ex.Message);
            }
        }
    }
}
