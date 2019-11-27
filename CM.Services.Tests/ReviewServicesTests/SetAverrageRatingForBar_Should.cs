using CM.Data;
using CM.DTOs;
using CM.Models;
using CM.Services.CustomExceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CM.Services.Tests.ReviewServicesTests
{
    [TestClass]
    public class SetAverrageRatingForBar_Should
    {
        [TestMethod]
        public async Task ThrowException_WhenBarDoesNotExist()
        {
            var options = TestUtils.GetOptions(nameof(ThrowException_WhenBarDoesNotExist));

            using (var arrangeContext = new CMContext(options))
            {
                var sut = new ReviewServices(arrangeContext);

                var ex = await Assert.ThrowsExceptionAsync<MagicException>(
                  async () => await sut.SetAverrageRatingForBar("11")
                  );
            }
        }

        [TestMethod]
        public async Task ThrowCorrectMessage_WhenBarDoesNotExist()
        {
            var options = TestUtils.GetOptions(nameof(ThrowCorrectMessage_WhenBarDoesNotExist));

            using (var arrangeContext = new CMContext(options))
            {
                var sut = new ReviewServices(arrangeContext);

                var ex = await Assert.ThrowsExceptionAsync<MagicException>(
                  async () => await sut.SetAverrageRatingForBar("11")
                  );
                Assert.AreEqual(ExceptionMessages.BarNull, ex.Message);
            }
        }

        [TestMethod]
        public async Task ThrowException_WhenStringIsNull()
        {
            var options = TestUtils.GetOptions(nameof(ThrowException_WhenStringIsNull));

            using (var arrangeContext = new CMContext(options))
            {
                var sut = new ReviewServices(arrangeContext);

                var ex = await Assert.ThrowsExceptionAsync<MagicException>(
                  async () => await sut.SetAverrageRatingForBar(null)
                  );
            }
        }

        [TestMethod]
        public async Task ThrowCorrectMessage_WhenStringIsNull()
        {
            var options = TestUtils.GetOptions(nameof(ThrowCorrectMessage_WhenStringIsNull));

            using (var arrangeContext = new CMContext(options))
            {
                var sut = new ReviewServices(arrangeContext);

                var ex = await Assert.ThrowsExceptionAsync<MagicException>(
                  async () => await sut.SetAverrageRatingForBar("null")
                  );
                Assert.AreEqual(ExceptionMessages.BarNull, ex.Message);
            }
        }



        [TestMethod]
        public async Task SetsNewRating_WhenValidBarIdIsPassed()
        {
            var options = TestUtils.GetOptions(nameof(SetsNewRating_WhenValidBarIdIsPassed));
            
            var bar = new Bar { Id = "2" };
            var Bar = new Bar
            {
                Id = "1"
            };
            var review1 = new BarReview { Rating = 6, Description = "0100101", BarId = "2" };
            var review2 = new BarReview {  Rating = 10, Description = "0100101", BarId = "2" };
            

            using (var arrangeContext = new CMContext(options))
            {
                var sut = new ReviewServices(arrangeContext);
                arrangeContext.Add(bar);
                arrangeContext.Add(review1);
                arrangeContext.Add(review2);
                await arrangeContext.SaveChangesAsync();
                await sut.SetAverrageRatingForBar("2");
                Assert.AreEqual(8, arrangeContext.Bars.First().BarRating);
            }
        }
    }
}
