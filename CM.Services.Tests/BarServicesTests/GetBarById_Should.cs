using CM.Data;
using CM.DTOs;
using CM.Models;
using CM.Services.CustomExeptions;
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
    public class GetBarById_Should
    {
        [TestMethod]
        public async Task Return_BarWithCorrectAttributes_WhenValidIdPassed()
        {
            var options = TestUtils.GetOptions(nameof(Return_BarWithCorrectAttributes_WhenValidIdPassed));

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

                var result = await sut.GetBarByID("3");
                
                Assert.AreEqual("Target", result.Name);
                Assert.AreEqual("3", result.Id);
                Assert.AreEqual("abv.bg", result.Website);
                Assert.AreEqual("Snimka", result.ImageUrl);
                Assert.AreEqual("Bulgaria", result.Country);
            }
        }

        [TestMethod]
        public async Task Return_InstanceOfTypeDto_WhenValidIdPassed()
        {
            var options = TestUtils.GetOptions(nameof(Return_InstanceOfTypeDto_WhenValidIdPassed));

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

                var result = await sut.GetBarByID("3");

                Assert.IsInstanceOfType(result, typeof(BarDTO));
            }
        }

        [TestMethod]
        public async Task Throw_MagicExeption_WhenNullIdPassed()
        {
            var options = TestUtils.GetOptions(nameof(Throw_MagicExeption_WhenNullIdPassed));

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

               var ex =  await Assert.ThrowsExceptionAsync<MagicExeption>(
                   async ()=> await sut.GetBarByID(null)
                   );
            }
        }

        [TestMethod]
        public async Task Throw_CorrectMagicMsg_WhenNullIdPassed()
        {
            var options = TestUtils.GetOptions(nameof(Throw_CorrectMagicMsg_WhenNullIdPassed));

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
                    async () => await sut.GetBarByID(null)
                    );
                Assert.AreEqual("ID cannot be null!", ex.Message);
            }
        }
    }
}
