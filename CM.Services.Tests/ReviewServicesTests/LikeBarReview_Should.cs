using CM.Data;
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
    public class LikeBarReview_Should
    {
        [TestMethod]
        public async Task LikeBarReview_WhenValidBarIdIsPassed()
        {
            var options = TestUtils.GetOptions(nameof(LikeBarReview_WhenValidBarIdIsPassed));

            var bar = new Bar{ Id = "2" };
            var user = new AppUser { Id = "1" };
            var review1 = new BarReview { Id = "1", Rating = 6, Description = "0100101", BarId = "2" };
            var review2 = new BarReview { Id = "2", Rating = 10, Description = "0100101", BarId = "2" };
            //var like1 = new BarReviewLike { Id = "1", BarReviewID = "1" , AppUserID = "1"};
            //var like2 = new BarReviewLike { Id = "2", BarReviewID = "2", AppUserID="1" };
            using (var arrangeContext = new CMContext(options))
            {
                var sut = new ReviewServices(arrangeContext);
                arrangeContext.Add(bar);
                arrangeContext.Add(user);
                arrangeContext.Add(review1);
                arrangeContext.Add(review2);
                await arrangeContext.SaveChangesAsync();
                await sut.LikeBarReview(bar.Id, user.Id);
                Assert.AreEqual("1", arrangeContext.BarReviewLikes.First().AppUserID);
                Assert.AreEqual("2", arrangeContext.BarReviewLikes.First().BarReviewID);
            }
        }

        [TestMethod]
        public async Task ThrowException_WhenUserLikeExists()
        {
            var options = TestUtils.GetOptions(nameof(ThrowException_WhenUserLikeExists));

            var bar = new Bar { Id = "2" };
            var user = new AppUser { Id = "1" };
            var review1 = new BarReview { Id = "1", Rating = 6, Description = "0100101", BarId = "2" };
            //var review2 = new BarReview { Id = "2", Rating = 10, Description = "0100101", BarId = "2" };
            var like1 = new BarReviewLike { Id = "1", BarReviewID = "1" , AppUserID = "1"};
            //var like2 = new BarReviewLike { Id = "2", BarReviewID = "2", AppUserID="1" };
            using (var arrangeContext = new CMContext(options))
            {
                var sut = new ReviewServices(arrangeContext);
                arrangeContext.Add(bar);
                arrangeContext.Add(user);
                arrangeContext.Add(review1);
                //arrangeContext.Add(review2);
                arrangeContext.Add(like1);
                await arrangeContext.SaveChangesAsync();
                await sut.LikeBarReview(bar.Id, user.Id);
                var ex = await Assert.ThrowsExceptionAsync<MagicException>(
                  async () => await sut.LikeBarReview(bar.Id, review1.Id)
                  );
            }
        }

        [TestMethod]
        public async Task ThrowExceptionForCocktailLike_WhenUserLikeExists()
        {
            var options = TestUtils.GetOptions(nameof(ThrowExceptionForCocktailLike_WhenUserLikeExists));

            var bar = new Bar { Id = "2" };
            var user = new AppUser { Id = "1" };
            var review1 = new BarReview { Id = "1", Rating = 6, Description = "0100101", BarId = "2" };
            //var review2 = new BarReview { Id = "2", Rating = 10, Description = "0100101", BarId = "2" };
            var like1 = new BarReviewLike { Id = "1", BarReviewID = "1", AppUserID = "1" };
            //var like2 = new BarReviewLike { Id = "2", BarReviewID = "2", AppUserID="1" };
            using (var arrangeContext = new CMContext(options))
            {
                var sut = new ReviewServices(arrangeContext);
                arrangeContext.Add(bar);
                arrangeContext.Add(user);
                arrangeContext.Add(review1);
                //arrangeContext.Add(review2);
                arrangeContext.Add(like1);
                await arrangeContext.SaveChangesAsync();
                await sut.LikeBarReview(bar.Id, user.Id);
                var ex = await Assert.ThrowsExceptionAsync<MagicException>(
                  async () => await sut.LikeBarReview(bar.Id, review1.Id)
                  );
                Assert.AreEqual(ExceptionMessages.ReviewIsLiked, ex.Message);
            }
        }
            
        [TestMethod]
        public async Task GetCorrectCountOfLikes_WhenValidParamethersArePassed()
        {
            var options = TestUtils.GetOptions(nameof(GetCorrectCountOfLikes_WhenValidParamethersArePassed));

            var bar = new Bar { Id = "2" };
            var user = new AppUser { Id = "1" };
            var user2 = new AppUser { Id = "2" };
            var review1 = new BarReview { Id = "1", Rating = 6, Description = "0100101", BarId = "2" };
            //var review2 = new BarReview { Id = "2", Rating = 10, Description = "0100101", BarId = "2" };
            var like1 = new BarReviewLike { Id = "1", BarReviewID = "1", AppUserID = "1" };
            var like2 = new BarReviewLike { Id = "2", BarReviewID = "1", AppUserID="2" };
            using (var arrangeContext = new CMContext(options))
            {
                var sut = new ReviewServices(arrangeContext);
                arrangeContext.Add(bar);
                arrangeContext.Add(user);
                arrangeContext.Add(user2);
                arrangeContext.Add(review1);
                //arrangeContext.Add(review2);
                arrangeContext.Add(like1);
                await arrangeContext.SaveChangesAsync();
                await sut.LikeBarReview(bar.Id, user.Id);
                var result = await sut.LikeBarReview(bar.Id, user2.Id);
                Assert.AreEqual(2, result);
            }
        }
        [TestMethod]
        public async Task ThrowException_WhenStringBarIdIsNull()
        {
            var options = TestUtils.GetOptions(nameof(ThrowException_WhenStringBarIdIsNull));

            using (var arrangeContext = new CMContext(options))
            {
                var sut = new ReviewServices(arrangeContext);

                var ex = await Assert.ThrowsExceptionAsync<MagicException>(
                  async () => await sut.LikeBarReview( null, "1")
                  );
            }
        }

        [TestMethod]
        public async Task ThrowCorrectMessage_WhenStringBarIdIsNull()
        {
            var options = TestUtils.GetOptions(nameof(ThrowCorrectMessage_WhenStringBarIdIsNull));

            using (var arrangeContext = new CMContext(options))
            {
                var sut = new ReviewServices(arrangeContext);

                var ex = await Assert.ThrowsExceptionAsync<MagicException>(
                  async () => await sut.LikeBarReview(null, "1")
                  );
                Assert.AreEqual(ExceptionMessages.IdNull, ex.Message);
            }
        }

        [TestMethod]
        public async Task ThrowException_WhenStringUserIdIsNull()
        {
            var options = TestUtils.GetOptions(nameof(ThrowException_WhenStringUserIdIsNull));

            using (var arrangeContext = new CMContext(options))
            {
                var sut = new ReviewServices(arrangeContext);

                var ex = await Assert.ThrowsExceptionAsync<MagicException>(
                  async () => await sut.LikeBarReview("1", null)
                  );
            }
        }

        [TestMethod]
        public async Task ThrowCorrectMessage_WhenStringUserIdIsNull()
        {
            var options = TestUtils.GetOptions(nameof(ThrowCorrectMessage_WhenStringUserIdIsNull));

            using (var arrangeContext = new CMContext(options))
            {
                var sut = new ReviewServices(arrangeContext);

                var ex = await Assert.ThrowsExceptionAsync<MagicException>(
                  async () => await sut.LikeBarReview("1", null)
                  );
                Assert.AreEqual(ExceptionMessages.IdNull, ex.Message);
            }
        }
    }
}
