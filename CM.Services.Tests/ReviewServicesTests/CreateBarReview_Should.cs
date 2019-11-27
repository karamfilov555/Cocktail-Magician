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
    public class CreateBarReview_Should
    {
        [TestMethod]
        public async Task AddCorrectReviewToDB_WhenValidModelIsPassed()
        {
            var options = TestUtils.GetOptions(nameof(AddCorrectReviewToDB_WhenValidModelIsPassed));
            var user = new AppUser { Id = "1" };
            var bar = new Bar { Id = "2" };
            var barReviewDTO = new BarReviewDTO
            {
                Rating = 10,
                Description = "10",
                UserID = "1",
                BarId = "2"
            };
            using (var arrangeContext = new CMContext(options))
            {
                var sut = new ReviewServices(arrangeContext);
                arrangeContext.Add(user);
                arrangeContext.Add(bar);
                await arrangeContext.SaveChangesAsync();
                await sut.CreateBarReview(barReviewDTO);
                Assert.AreEqual(10, arrangeContext.BarReviews.First().Rating);
                Assert.AreEqual("10", arrangeContext.BarReviews.First().Description);
                Assert.AreEqual("1", arrangeContext.BarReviews.First().UserId);
                Assert.AreEqual("2", arrangeContext.BarReviews.First().BarId);
            }
        }

        [TestMethod]
        public async Task ThrowException_WhenPassedBarIsNull()
        {
            var options = TestUtils.GetOptions(nameof(ThrowException_WhenPassedBarIsNull));
            var user = new AppUser { Id = "1" };
            var barReviewDTO = new BarReviewDTO
            {
                Rating = 10,
                Description = "10",
                UserID = "1",
                BarId = "2"
            };
            using (var arrangeContext = new CMContext(options))
            {
                var sut = new ReviewServices(arrangeContext);
                arrangeContext.Add(user);
                await arrangeContext.SaveChangesAsync();
                var ex = await Assert.ThrowsExceptionAsync<MagicException>(
                  async () => await sut.CreateBarReview(barReviewDTO)
                  );
            }
        }

        [TestMethod]
        public async Task ThrowCorrectMessage_WhenPassedBarIsNull()
        {
            var options = TestUtils.GetOptions(nameof(ThrowCorrectMessage_WhenPassedBarIsNull));
            var user = new AppUser { Id = "1" };
            var barReviewDTO = new BarReviewDTO
            {
                Rating = 10,
                Description = "10",
                UserID = "1",
                BarId = "2"
            };
            using (var arrangeContext = new CMContext(options))
            {
                var sut = new ReviewServices(arrangeContext);
                arrangeContext.Add(user);
                await arrangeContext.SaveChangesAsync();
                var ex = await Assert.ThrowsExceptionAsync<MagicException>(
                  async () => await sut.CreateBarReview(barReviewDTO)
                  );
                Assert.AreEqual(ExceptionMessages.BarNull, ex.Message);
            }
        }

        [TestMethod]
        public async Task ThrowException_WhenPassedUserIsNull()
        {
            var options = TestUtils.GetOptions(nameof(ThrowException_WhenPassedUserIsNull));
            var bar = new Bar { Id = "2" };
            var barReviewDTO = new BarReviewDTO
            {
                Rating = 10,
                Description = "10",
                UserID = "1",
                BarId = "2"
            };
            using (var arrangeContext = new CMContext(options))
            {
                var sut = new ReviewServices(arrangeContext);
                arrangeContext.Add(bar);
                await arrangeContext.SaveChangesAsync();
                var ex = await Assert.ThrowsExceptionAsync<MagicException>(
                  async () => await sut.CreateBarReview(barReviewDTO)
                  );
            }
        }

        [TestMethod]
        public async Task ThrowCorrectMessage_WhenPassedUserIsNull()
        {
            var options = TestUtils.GetOptions(nameof(ThrowCorrectMessage_WhenPassedUserIsNull));
            var bar = new Bar { Id = "2" };
            var barReviewDTO = new BarReviewDTO
            {
                Rating = 10,
                Description = "10",
                UserID = "1",
                BarId = "2"
            };
            using (var arrangeContext = new CMContext(options))
            {
                var sut = new ReviewServices(arrangeContext);
                arrangeContext.Add(bar);
                await arrangeContext.SaveChangesAsync();
                var ex = await Assert.ThrowsExceptionAsync<MagicException>(
                  async () => await sut.CreateBarReview(barReviewDTO)
                  );
                Assert.AreEqual(ExceptionMessages.AppUserNull, ex.Message);
            }
        }

        [TestMethod]
        public async Task ThrowException_WhenUserAlreadyReviewedBar()
        {
            var options = TestUtils.GetOptions(nameof(ThrowException_WhenUserAlreadyReviewedBar));
            var user = new AppUser { Id = "1" };
            var bar = new Bar { Id = "2" };
            var barReviewDTO = new BarReviewDTO
            {
                Rating = 10,
                Description = "10",
                UserID = "1",
                BarId = "2"
            };
            var review = new BarReview { Rating = 5, Description = "0100101", UserId = "1", BarId = "2" };
            using (var arrangeContext = new CMContext(options))
            {
                var sut = new ReviewServices(arrangeContext);
                arrangeContext.Add(user);
                arrangeContext.Add(bar);
                arrangeContext.Add(review);
                await arrangeContext.SaveChangesAsync();
                var ex = await Assert.ThrowsExceptionAsync<MagicException>(
                  async () => await sut.CreateBarReview(barReviewDTO)
                  );
            }
        }

        [TestMethod]
        public async Task ThrowExceptionWithCorrectMessage_WhenUserAlreadyReviewedBar()
        {
            var options = TestUtils.GetOptions(nameof(ThrowExceptionWithCorrectMessage_WhenUserAlreadyReviewedBar));
            var user = new AppUser { Id = "1" };
            var bar = new Bar { Id = "2" };
            var barReviewDTO = new BarReviewDTO
            {
                Rating = 10,
                Description = "10",
                UserID = "1",
                BarId = "2"
            };
            var review = new BarReview { Rating = 5, Description = "0100101", UserId = "1", BarId = "2" };
            using (var arrangeContext = new CMContext(options))
            {
                var sut = new ReviewServices(arrangeContext);
                arrangeContext.Add(user);
                arrangeContext.Add(bar);
                arrangeContext.Add(review);
                await arrangeContext.SaveChangesAsync();
                var ex = await Assert.ThrowsExceptionAsync<MagicException>(
                  async () => await sut.CreateBarReview(barReviewDTO)
                  );
                Assert.AreEqual("You have already reviewed this bar!", ex.Message);
            }
        }

        [TestMethod]
        public async Task SetsNewRating_WhenValidModelIsPassed()
        {
            var options = TestUtils.GetOptions(nameof(SetsNewRating_WhenValidModelIsPassed));
            var user = new AppUser { Id = "1" };
            var bar = new Bar { Id = "2"};
            var barReviewDTO = new BarReviewDTO
            {
                Rating = 8,
                Description = "10",
                UserID = "1",
                BarId = "2"
            };
            var review = new BarReview { Rating = 10, Description = "0100101", BarId = "2" };

            using (var arrangeContext = new CMContext(options))
            {
                var sut = new ReviewServices(arrangeContext);
                arrangeContext.Add(user);
                arrangeContext.Add(bar);
                arrangeContext.Add(review);
                await arrangeContext.SaveChangesAsync();
                await sut.CreateBarReview(barReviewDTO);
                Assert.AreEqual(9, arrangeContext.Bars.First().BarRating);
            }
        }
    }
}
