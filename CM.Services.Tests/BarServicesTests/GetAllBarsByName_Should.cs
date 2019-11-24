using CM.Data;
using CM.DTOs;
using CM.Models;
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
    public class GetAllBarsByName_Should
    {
        [TestMethod]
        public async Task Return_ICollectionOfBarsDtos_WichMachedSearchCriteria()
        {
            var options = TestUtils.GetOptions
                (nameof(Return_ICollectionOfBarsDtos_WichMachedSearchCriteria));


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
                        },
                        BarRating = 1
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
                        },
                       BarRating = 2
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

                var result = await sut.GetAllBarsByName("Bash");


                Assert.AreEqual(2, result.Count());
                Assert.AreEqual("BashBar", result.ToList()[0].Name);
                Assert.AreEqual("BashBar2", result.ToList()[1].Name);
                Assert.IsInstanceOfType(result, typeof(ICollection<BarDTO>));
            }
        }

        [TestMethod]
        public async Task Return_EmptyCollection_IfThereAreNoMatches()
        {
            var options = TestUtils.GetOptions
                (nameof(Return_EmptyCollection_IfThereAreNoMatches));


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
                        },
                        BarRating = 1
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
                        },
                       BarRating = 2
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

                var result = await sut.GetAllBarsByName("laksdlakdka");


                Assert.AreEqual(0, result.Count());
                Assert.IsInstanceOfType(result, typeof(ICollection<BarDTO>));
            }
        }
    }
}
