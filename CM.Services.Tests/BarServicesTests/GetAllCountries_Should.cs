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
    public class GetAllCountries_Should
    {
        [TestClass]
        public class GetAllBarsByName_Should
        {
            [TestMethod]
            public async Task Return_ICollectionOfAllCountriesDtos_FromDb()
            {
                var options = TestUtils.GetOptions
                    (nameof(Return_ICollectionOfAllCountriesDtos_FromDb));


                var fileService = new Mock<IFileUploadService>();

                using (var arrangeContext = new CMContext(options))
                {

                    //1

                    arrangeContext.Countries.Add(
                        new Country
                        {
                            Id = "1",
                            Name = "Bangladesh",

                        });
                    //2
                    arrangeContext.Countries.Add(
                      new Country
                      {
                          Id = "2",
                          Name = "Africa",

                      });
                    //3
                    arrangeContext.Countries.Add(
                      new Country
                      {
                          Id = "3",
                          Name = "Zanobia",

                      });

                    await arrangeContext.SaveChangesAsync();
                }
                using (var assertContext = new CMContext(options))
                {
                    var sut = new BarServices(assertContext, fileService.Object);

                    var result = await sut.GetAllCountries();


                    Assert.AreEqual(3, result.Count());
                    Assert.IsInstanceOfType(result, typeof(ICollection<CountryDTO>));
                }
            }

            [TestMethod]
            public async Task Return_ICollection_SortedByName_OfAllCountriesDtos_FromDb()
            {
                var options = TestUtils.GetOptions
                    (nameof(Return_ICollection_SortedByName_OfAllCountriesDtos_FromDb));


                var fileService = new Mock<IFileUploadService>();

                using (var arrangeContext = new CMContext(options))
                {

                    //1

                    arrangeContext.Countries.Add(
                        new Country
                        {
                            Id = "1",
                            Name = "Bangladesh",

                        });
                    //2
                    arrangeContext.Countries.Add(
                      new Country
                      {
                          Id = "2",
                          Name = "Africa",

                      });
                    //3
                    arrangeContext.Countries.Add(
                      new Country
                      {
                          Id = "3",
                          Name = "Zanobia",

                      });

                    await arrangeContext.SaveChangesAsync();
                }
                using (var assertContext = new CMContext(options))
                {
                    var sut = new BarServices(assertContext, fileService.Object);

                    var result = await sut.GetAllCountries();


                    Assert.AreEqual(3, result.Count());
                    Assert.AreEqual("Africa", result.ToList()[0].Name);
                    Assert.AreEqual("Bangladesh", result.ToList()[1].Name);
                    Assert.AreEqual("Zanobia", result.ToList()[2].Name);
                    Assert.IsInstanceOfType(result, typeof(ICollection<CountryDTO>));
                }
            }

        }
    }
}
