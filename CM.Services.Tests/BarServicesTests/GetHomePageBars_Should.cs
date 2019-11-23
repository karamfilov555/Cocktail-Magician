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
    public class GetHomePageBars_Should
    {
        [TestMethod]
        public async Task Return_FiveBarsDTOs()
        {
            var options = TestUtils.GetOptions(nameof(Return_FiveBarsDTOs));

            var fileService = new Mock<IFileUploadService>();

            using (var arrangeContext = new CMContext(options))
            {
                //1
                arrangeContext.Bars.Add(
                    new Bar
                    {
                        Id = "1",
                        Name = "BashBar",
                        BarRating = 1,
                        Address = new Address
                        {
                          Id ="1",
                          BarId = "1", 
                          Country = new Country
                          {
                              Id = "1",
                              Name = "Bulgaria"
                          }
                        } 
                    });
                //2
                arrangeContext.Bars.Add(
                   new Bar
                   {
                       Id = "2",
                       Name = "BashBar",
                       Address = new Address
                       {
                           Id = "2",
                           BarId = "2",
                           Country = new Country
                           {
                               Id = "2",
                               Name = "Bulgaria"
                           }
                       }
                       ,
                       BarRating = 2
                   });
                //3
                arrangeContext.Bars.Add(
                   new Bar
                   {
                       Id = "3",
                       Name = "BashBar",
                       Address = new Address
                       {
                           Id = "3",
                           BarId = "3",
                           Country = new Country
                           {
                               Id = "3",
                               Name = "Bulgaria"
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
                       Name = "BashBar",
                       Address = new Address
                       {
                           Id = "4",
                           BarId = "4",
                           Country = new Country
                           {
                               Id = "4",
                               Name = "Bulgaria"
                           }
                       }
                       ,
                       BarRating = 4
                   });
                //5
                arrangeContext.Bars.Add(
                   new Bar
                   {
                       Id = "5",
                       Name = "BashBar",
                       Address = new Address
                       {
                           Id = "5",
                           BarId = "5",
                           Country = new Country
                           {
                               Id = "5",
                               Name = "Bulgaria"
                           }
                       }
                       ,
                       BarRating = 5
                   });
                //6
                arrangeContext.Bars.Add(
                   new Bar
                   {
                       Id = "6",
                       Name = "BashBar",
                       Address = new Address
                       {
                           Id = "6",
                           BarId = "6",
                           Country = new Country
                           {
                               Id = "6",
                               Name = "Bulgaria"
                           }
                       }
                       ,
                       BarRating = 6
                   });
                await arrangeContext.SaveChangesAsync();
            }
            using (var assertContext = new CMContext(options))
            {
                var sut = new BarServices(assertContext, fileService.Object);

                var result = await sut.GetHomePageBars();

                Assert.AreEqual(5, result.Count());
            }
        }

        [TestMethod]
        public async Task Return_ICollectionOfHomePageDTOs_WithTopRating()
        {
            var options = TestUtils.GetOptions(nameof(Return_ICollectionOfHomePageDTOs_WithTopRating));

            var fileService = new Mock<IFileUploadService>();

            using (var arrangeContext = new CMContext(options))
            {
                //1
                arrangeContext.Bars.Add(
                    new Bar
                    {
                        Id = "1",
                        Name = "BashBar",
                        BarRating = 1,
                        Address = new Address
                        {
                            Id = "1",
                            BarId = "1",
                            Country = new Country
                            {
                                Id = "1",
                                Name = "Bulgaria"
                            }
                        }
                    });
                //2
                arrangeContext.Bars.Add(
                   new Bar
                   {
                       Id = "2",
                       Name = "BashBar",
                       Address = new Address
                       {
                           Id = "2",
                           BarId = "2",
                           Country = new Country
                           {
                               Id = "2",
                               Name = "Bulgaria"
                           }
                       }
                       ,
                       BarRating = 2
                   });
                //3
                arrangeContext.Bars.Add(
                   new Bar
                   {
                       Id = "3",
                       Name = "BashBar",
                       Address = new Address
                       {
                           Id = "3",
                           BarId = "3",
                           Country = new Country
                           {
                               Id = "3",
                               Name = "Bulgaria"
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
                       Name = "BashBar",
                       Address = new Address
                       {
                           Id = "4",
                           BarId = "4",
                           Country = new Country
                           {
                               Id = "4",
                               Name = "Bulgaria"
                           }
                       }
                       ,
                       BarRating = 4
                   });
                //5
                arrangeContext.Bars.Add(
                   new Bar
                   {
                       Id = "5",
                       Name = "BashBar",
                       Address = new Address
                       {
                           Id = "5",
                           BarId = "5",
                           Country = new Country
                           {
                               Id = "5",
                               Name = "Bulgaria"
                           }
                       }
                       ,
                       BarRating = 5
                   });
                //6
                arrangeContext.Bars.Add(
                   new Bar
                   {
                       Id = "6",
                       Name = "BashBar",
                       Address = new Address
                       {
                           Id = "6",
                           BarId = "6",
                           Country = new Country
                           {
                               Id = "6",
                               Name = "Bulgaria"
                           }
                       }
                       ,
                       BarRating = 6
                   });
                await arrangeContext.SaveChangesAsync();
            }
            using (var assertContext = new CMContext(options))
            {
                var sut = new BarServices(assertContext, fileService.Object);

                var result = await sut.GetHomePageBars();
                var resultList = result.ToList();

                Assert.AreEqual("6", resultList[0].Id);
                Assert.AreEqual("5", resultList[1].Id);
                Assert.AreEqual("4", resultList[2].Id);
                Assert.AreEqual("3", resultList[3].Id);
                Assert.AreEqual("2", resultList[4].Id);
                Assert.IsInstanceOfType(result, typeof(ICollection<HomePageBarDTO>));
            }
        }

        [TestMethod]
        public async Task NotInclude_DeletedBars_forHomePage()
        {
            var options = TestUtils.GetOptions(nameof(NotInclude_DeletedBars_forHomePage));

            var fileService = new Mock<IFileUploadService>();

            using (var arrangeContext = new CMContext(options))
            {
                //1
                arrangeContext.Bars.Add(
                    new Bar
                    {
                        Id = "1",
                        Name = "BashBar",
                        BarRating = 1,
                        Address = new Address
                        {
                            Id = "1",
                            BarId = "1",
                            Country = new Country
                            {
                                Id = "1",
                                Name = "Bulgaria"
                            }
                        }
                    });
                //2
                arrangeContext.Bars.Add(
                   new Bar
                   {
                       Id = "2",
                       Name = "BashBar",
                       Address = new Address
                       {
                           Id = "2",
                           BarId = "2",
                           Country = new Country
                           {
                               Id = "2",
                               Name = "Bulgaria"
                           }
                       }
                       ,
                       BarRating = 2
                   });
                //3
                arrangeContext.Bars.Add(
                   new Bar
                   {
                       Id = "3",
                       Name = "BashBar",
                       Address = new Address
                       {
                           Id = "3",
                           BarId = "3",
                           Country = new Country
                           {
                               Id = "3",
                               Name = "Bulgaria"
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
                       Name = "BashBar",
                       Address = new Address
                       {
                           Id = "4",
                           BarId = "4",
                           Country = new Country
                           {
                               Id = "4",
                               Name = "Bulgaria"
                           }
                       }
                       ,
                       BarRating = 4
                   });
                //5
                arrangeContext.Bars.Add(
                   new Bar
                   {
                       Id = "5",
                       Name = "BashBar",
                       Address = new Address
                       {
                           Id = "5",
                           BarId = "5",
                           Country = new Country
                           {
                               Id = "5",
                               Name = "Bulgaria"
                           }
                       }
                       ,
                       BarRating = 5
                   });
                //6
                arrangeContext.Bars.Add(
                   new Bar
                   {
                       Id = "6",
                       Name = "BashBar",
                       Address = new Address
                       {
                           Id = "6",
                           BarId = "6",
                           Country = new Country
                           {
                               Id = "6",
                               Name = "Bulgaria"
                           }
                       }
                       ,
                       BarRating = 6,
                       DateDeleted = DateTime.Now //this bar is deleted
                   }) ;
                await arrangeContext.SaveChangesAsync();
            }
            using (var assertContext = new CMContext(options))
            {
                var sut = new BarServices(assertContext, fileService.Object);

                var result = await sut.GetHomePageBars();
                var resultList = result.ToList();

                Assert.AreEqual("5", resultList[0].Id);
                Assert.AreEqual("4", resultList[1].Id);
                Assert.AreEqual("3", resultList[2].Id);
                Assert.AreEqual("2", resultList[3].Id);
                Assert.AreEqual("1", resultList[4].Id);
                Assert.IsInstanceOfType(result, typeof(ICollection<HomePageBarDTO>));
            }
        }


        [TestMethod]
        public async Task Throw_IfBarAddressIsNotIncluded()
        {
            var options = TestUtils.GetOptions(nameof(Throw_IfBarAddressIsNotIncluded));

            var fileService = new Mock<IFileUploadService>();

            using (var arrangeContext = new CMContext(options))
            {
                //1
                arrangeContext.Bars.Add(
                    new Bar
                    {
                        Id = "1",
                        Name = "BashBar",
                        BarRating = 1,
                       
                    });
                //2
                arrangeContext.Bars.Add(
                   new Bar
                   {
                       Id = "2",
                       Name = "BashBar",
                       Address = new Address
                       {
                           Id = "2",
                           BarId = "2",
                           Country = new Country
                           {
                               Id = "2",
                               Name = "Bulgaria"
                           }
                       }
                       ,
                       BarRating = 2
                   });
                //3
                arrangeContext.Bars.Add(
                   new Bar
                   {
                       Id = "3",
                       Name = "BashBar",
                       Address = new Address
                       {
                           Id = "3",
                           BarId = "3",
                           Country = new Country
                           {
                               Id = "3",
                               Name = "Bulgaria"
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
                       Name = "BashBar",
                       Address = new Address
                       {
                           Id = "4",
                           BarId = "4",
                           Country = new Country
                           {
                               Id = "4",
                               Name = "Bulgaria"
                           }
                       }
                       ,
                       BarRating = 4
                   });
                //5
                arrangeContext.Bars.Add(
                   new Bar
                   {
                       Id = "5",
                       Name = "BashBar",
                       Address = new Address
                       {
                           Id = "5",
                           BarId = "5",
                           Country = new Country
                           {
                               Id = "5",
                               Name = "Bulgaria"
                           }
                       }
                       ,
                       BarRating = 5
                   });
                //6
                arrangeContext.Bars.Add(
                   new Bar
                   {
                       Id = "6",
                       Name = "BashBar",
                       
                       
                       BarRating = 6,
                    
                   });
                await arrangeContext.SaveChangesAsync();
            }
            using (var assertContext = new CMContext(options))
            {
                var sut = new BarServices(assertContext, fileService.Object);

                //var result = await sut.GetHomePageBars(); // tuk trow-va , dolu ne?!
                Assert.ThrowsException<Exception>(
                    async () => await sut.GetHomePageBars()
                    );
            }
        }

        //Method tested:
        //public async Task<ICollection<HomePageBarDTO>> GetHomePageBars()
        //{
        //    var bars = await _context.Bars
        //        .Include(b => b.Address)
        //        .Where(b => b.DateDeleted == null)
        //        .OrderByDescending(b => b.BarRating)
        //        .Take(5)
        //        .Select(b => b.MapBarToHomePageBarDTO())
        //        .ToListAsync()
        //        .ConfigureAwait(false);
        //    return bars;
        //}
    }
}
