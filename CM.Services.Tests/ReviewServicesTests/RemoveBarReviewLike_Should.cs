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
    public class RemoveBarReviewLike_Should
    {

        [TestMethod]
        public async Task RemoveLikeBarReview_WhenValidBarIdIsPassed()
        {
            var options = TestUtils.GetOptions(nameof(RemoveLikeBarReview_WhenValidBarIdIsPassed));
            var bar = new Bar { Id = "2" };
            var user = new AppUser { Id = "1" };
            var review1 = new BarReview { Id = "1", Rating = 6, Description = "0100101", BarId = "2" };
            var review2 = new BarReview { Id = "2", Rating = 10, Description = "0100101", BarId = "2" };
            var like1 = new BarReviewLike { Id = "1", BarReviewID = "1" , AppUserID = "1"};
            //var like2 = new BarReviewLike { Id = "2", BarReviewID = "2", AppUserID="1" };
            using (var arrangeContext = new CMContext(options))
            {
                var sut = new ReviewServices(arrangeContext);
                arrangeContext.Add(bar);
                arrangeContext.Add(user);
                arrangeContext.Add(review1);
                arrangeContext.Add(like1);
                await arrangeContext.SaveChangesAsync();
                Assert.AreEqual(1, arrangeContext.BarReviewLikes.Count());
                await sut.RemoveBarReviewLike(review1.Id, user.Id);
                Assert.AreEqual(0, arrangeContext.BarReviewLikes.Count());
            }
        }

        [TestMethod]
        public async Task ThrowException_WhenLikeDoesntExist()
        {
            var options = TestUtils.GetOptions(nameof(ThrowException_WhenLikeDoesntExist));

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
                //arrangeContext.Add(like1);
                await arrangeContext.SaveChangesAsync();
                var ex = await Assert.ThrowsExceptionAsync<MagicException>(
                  async () => await sut.RemoveBarReviewLike(review1.Id, user.Id)
                  );
            }
        }

        [TestMethod]
        public async Task ThrowExceptionForCocktailLike_WhenLikeDoesntExist()
        {
            var options = TestUtils.GetOptions(nameof(ThrowExceptionForCocktailLike_WhenLikeDoesntExist));

            var bar = new Bar { Id = "2" };
            var user = new AppUser { Id = "1" };
            var review1 = new BarReview { Id = "1", Rating = 6, Description = "0100101", BarId = "2" };
            //var review2 = new BarReview { Id = "2", Rating = 10, Description = "0100101", BarId = "2" };
            //var like1 = new BarReviewLike { Id = "1", BarReviewID = "1", AppUserID = "1" };
            //var like2 = new BarReviewLike { Id = "2", BarReviewID = "2", AppUserID="1" };
            using (var arrangeContext = new CMContext(options))
            {
                var sut = new ReviewServices(arrangeContext);
                arrangeContext.Add(bar);
                arrangeContext.Add(user);
                arrangeContext.Add(review1);
                //arrangeContext.Add(review2);
                //arrangeContext.Add(like1);
                await arrangeContext.SaveChangesAsync();
                await sut.LikeBarReview(bar.Id, user.Id);
                var ex = await Assert.ThrowsExceptionAsync<MagicException>(
                  async () => await sut.RemoveBarReviewLike(review1.Id, user.Id)
                  );
                Assert.AreEqual(ExceptionMessages.LikeNull, ex.Message);
            }
        }

        [TestMethod]
        public async Task ReturnCorrectCountOfLikes_WhenLikeIsRemoved()
        {
            var options = TestUtils.GetOptions(nameof(ReturnCorrectCountOfLikes_WhenLikeIsRemoved));

            var bar = new Bar { Id = "2" };
            var user = new AppUser { Id = "1" };
            var user2 = new AppUser { Id = "2" };
            var review1 = new BarReview { Id = "1", Rating = 6, Description = "0100101", BarId = "2" };
            //var review2 = new BarReview { Id = "2", Rating = 10, Description = "0100101", BarId = "2" };
            var like1 = new BarReviewLike { Id = "1", BarReviewID = "1", AppUserID = "1" };
            var like2 = new BarReviewLike { Id = "2", BarReviewID = "1", AppUserID = "2" };
            using (var arrangeContext = new CMContext(options))
            {
                var sut = new ReviewServices(arrangeContext);
                arrangeContext.Add(bar);
                arrangeContext.Add(user);
                arrangeContext.Add(user2);
                arrangeContext.Add(review1);
                //arrangeContext.Add(review2);
                arrangeContext.Add(like1);
                arrangeContext.Add(like2);
                await arrangeContext.SaveChangesAsync();
                var result = await sut.RemoveBarReviewLike(review1.Id, user.Id);
                Assert.AreEqual(1, result);
            }
        }
        [TestMethod]
        public async Task ThrowExceptionForRemoveBarLike_WhenStringBarIdIsNull()
        {
            var options = TestUtils.GetOptions(nameof(ThrowExceptionForRemoveBarLike_WhenStringBarIdIsNull));

            using (var arrangeContext = new CMContext(options))
            {
                var sut = new ReviewServices(arrangeContext);

                var ex = await Assert.ThrowsExceptionAsync<MagicException>(
                  async () => await sut.RemoveBarReviewLike(null, "1")
                  );
            }
        }

        [TestMethod]
        public async Task ThrowCorrectMessageForRemoveBarLike_WhenStringBarIdIsNull()
        {
            var options = TestUtils.GetOptions(nameof(ThrowCorrectMessageForRemoveBarLike_WhenStringBarIdIsNull));

            using (var arrangeContext = new CMContext(options))
            {
                var sut = new ReviewServices(arrangeContext);

                var ex = await Assert.ThrowsExceptionAsync<MagicException>(
                  async () => await sut.RemoveBarReviewLike(null, "1")
                  );
                Assert.AreEqual(ExceptionMessages.IdNull, ex.Message);
            }
        }

        [TestMethod]
        public async Task ThrowExceptionForRemoveBarLike_WhenStringUserIdIsNull()
        {
            var options = TestUtils.GetOptions(nameof(ThrowExceptionForRemoveBarLike_WhenStringUserIdIsNull));

            using (var arrangeContext = new CMContext(options))
            {
                var sut = new ReviewServices(arrangeContext);
                var ex = await Assert.ThrowsExceptionAsync<MagicException>(
                  async () => await sut.RemoveBarReviewLike("1", null)
                  );
            }
        }

        [TestMethod]
        public async Task ThrowCorrectMessageForRemoveBarLike_WhenStringUserIdIsNull()
        {
            var options = TestUtils.GetOptions(nameof(ThrowCorrectMessageForRemoveBarLike_WhenStringUserIdIsNull));

            using (var arrangeContext = new CMContext(options))
            {
                var sut = new ReviewServices(arrangeContext);
                var ex = await Assert.ThrowsExceptionAsync<MagicException>(
                  async () => await sut.RemoveBarReviewLike("1", null)
                  );
                Assert.AreEqual(ExceptionMessages.IdNull, ex.Message);
            }
        }
    }
}
