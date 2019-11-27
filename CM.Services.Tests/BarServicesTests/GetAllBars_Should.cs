using CM.Data;
using CM.DTOs;
using CM.Models;
using CM.Services.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CM.Services.Tests.BarServicesTests
{
    [TestClass]
    public class GetAllBars_Should
    {
        [TestMethod]
        public async Task Return_SortedPaginatedListByRating_IfPassedSortCriteria_IsRating()
        {
            var options = TestUtils.GetOptions
                (nameof(Return_SortedPaginatedListByRating_IfPassedSortCriteria_IsRating));


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

                var result = await sut.GetAllBars(null, "Rating");

              
                Assert.AreEqual(1, result[0].Rating);
                Assert.AreEqual(2, result[1].Rating);
            }
        }

        [TestMethod]
        public async Task Return_SortedPaginatedListByName_IfPassedSortCriteria_IsNull()
        {
            var options = TestUtils.GetOptions
                (nameof(Return_SortedPaginatedListByName_IfPassedSortCriteria_IsNull));


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
                
                var result = await sut.GetAllBars(null, null);


                Assert.AreEqual("BashBar", result[0].Name);
                Assert.AreEqual("BashBar2", result[1].Name);
            }
        }

        [TestMethod]
        public async Task Return_SortedPaginatedListByNameDesc_IfPassedSortCriteria_IsNameDesc()
        {
            var options = TestUtils.GetOptions
                (nameof(Return_SortedPaginatedListByNameDesc_IfPassedSortCriteria_IsNameDesc));


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

                var result = await sut.GetAllBars(null, "name_desc");


                Assert.AreEqual("Target", result[0].Name);
                Assert.AreEqual("BashBar2", result[1].Name);
            }
        }

        [TestMethod]
        public async Task Return_SortedPaginatedListByRatingDes_IfPassedSortCriteria_IsRating_asc()
        {
            var options = TestUtils.GetOptions
                (nameof(Return_SortedPaginatedListByRatingDes_IfPassedSortCriteria_IsRating_asc));


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

                var result = await sut.GetAllBars(null, "rating_asc");

                
                Assert.AreEqual(3, result[0].Rating);
                Assert.AreEqual(2, result[1].Rating);
            }
        }

        [TestMethod]
        public async Task Return_PaginatedList_WithThreeBars_PerPage()
        {
            var options = TestUtils.GetOptions
                (nameof(Return_PaginatedList_WithThreeBars_PerPage));


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
                //4
                arrangeContext.Bars.Add(
                  new Bar
                  {
                      Id = "4",
                      Name = "Target",
                      Image = "Snimka",
                      Website = "abv.bg",
                      Address = new Address
                      {
                          Id = "4",
                          BarId = "4",
                          Country = new Country
                          {
                              Id = "4",
                              Name = "Bulgaria"
                          }
                      },

                      BarCocktails = new List<BarCocktail>
                       {
                            new BarCocktail
                            {
                                BarId = "4",
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

                var result = await sut.GetAllBars(null, null);

                Assert.AreEqual(3,result.Count());
            }
        }

        [TestMethod]
        public async Task Return_TheRestOfTheBars_IfThereAreNoTwoBarsLeft()
        {
            var options = TestUtils.GetOptions
                (nameof(Return_TheRestOfTheBars_IfThereAreNoTwoBarsLeft));


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
                arrangeContext.Bars.Add(
                  new Bar
                  {
                      Id = "4",
                      Name = "Target",
                      Image = "Snimka",
                      Website = "abv.bg",
                      Address = new Address
                      {
                          Id = "4",
                          BarId = "4",
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
                                BarId = "4",
                                CocktailId = "4"
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

                var result = await sut.GetAllBars(null, null);

                Assert.AreEqual(2, result.Count());
            }
        }

        [TestMethod]
        public async Task Return_OnlyTheBarsWichIsNotDeleted()
        {
            var options = TestUtils.GetOptions
                (nameof(Return_OnlyTheBarsWichIsNotDeleted));


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
                        BarRating = 1,
                        DateDeleted = DateTime.Now
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
                       BarRating = 2,
                       DateDeleted = DateTime.Now
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

                var result = await sut.GetAllBars(null, null);

                Assert.AreEqual("Target", result[0].Name);
            }
        }


        [TestMethod]
        public async Task Return_PaginatedListWithBarDtos_WhenValidValuesPassed()
        {
            var options = TestUtils.GetOptions(nameof(Return_PaginatedListWithBarDtos_WhenValidValuesPassed));

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

                var result = await sut.GetAllBars(null,null);
                Assert.IsInstanceOfType(result, typeof(PaginatedList<BarDTO>));
            }
        }
    }
}
