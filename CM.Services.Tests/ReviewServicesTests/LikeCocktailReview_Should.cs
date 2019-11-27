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
    public class LikeCocktailReview_Should
    {
        [TestMethod]
        public async Task LikeCocktailReview_WhenValidBarIdIsPassed()
        {
            var options = TestUtils.GetOptions(nameof(LikeCocktailReview_WhenValidBarIdIsPassed));

            var cocktail = new Cocktail { Id = "2" };
            var user = new AppUser { Id = "1" };
            var review1 = new CocktailReview { Id = "1", Rating = 6, Description = "0100101", UserId = user.Id, CocktailId = "2" };
            var review2 = new CocktailReview { Id = "2", Rating = 10, Description = "0100101", UserId = user.Id, CocktailId = "2" };
            //var like1 = new BarReviewLike { Id = "1", BarReviewID = "1" , AppUserID = "1"};
            //var like2 = new BarReviewLike { Id = "2", BarReviewID = "2", AppUserID="1" };
            using (var arrangeContext = new CMContext(options))
            {
                var sut = new ReviewServices(arrangeContext);
                arrangeContext.Add(cocktail);
                arrangeContext.Add(user);
                arrangeContext.Add(review1);
                arrangeContext.Add(review2);
                await arrangeContext.SaveChangesAsync();
                await sut.LikeCocktailReview(cocktail.Id, user.Id);
                Assert.AreEqual("1", arrangeContext.CocktailReviews.First().UserId);
                Assert.AreEqual("2", arrangeContext.CocktailReviews.First().CocktailId);
            }
        }

        [TestMethod]
        public async Task ThrowException_WhenUserCocktailLikeExists()
        {
            var options = TestUtils.GetOptions(nameof(ThrowException_WhenUserCocktailLikeExists));

            var cocktail = new Cocktail { Id = "2" };
            var user = new AppUser { Id = "1" };
            var review1 = new CocktailReview { Id = "1", Rating = 6, Description = "0100101", UserId = user.Id, CocktailId = "2" };
            var review2 = new CocktailReview { Id = "2", Rating = 10, Description = "0100101", UserId = user.Id, CocktailId = "2" };
            var like1 = new CocktailReviewLike { Id = "1", CocktailReviewID = "1", AppUserID = "1" };
            //var like2 = new BarReviewLike { Id = "2", BarReviewID = "2", AppUserID="1" };
            using (var arrangeContext = new CMContext(options))
            {
                var sut = new ReviewServices(arrangeContext);
                arrangeContext.Add(cocktail);
                arrangeContext.Add(user);
                arrangeContext.Add(review1);
                arrangeContext.Add(review2);
                arrangeContext.Add(like1);
                await arrangeContext.SaveChangesAsync();
                var ex = await Assert.ThrowsExceptionAsync<MagicException>(
                 async () => await sut.LikeCocktailReview(review1.Id, user.Id)
                );
            }
        }

        [TestMethod]
        public async Task ThrowCorrectMessageException_WhenUserCocktailLikeExists()
        {
            var options = TestUtils.GetOptions(nameof(ThrowCorrectMessageException_WhenUserCocktailLikeExists));

            var cocktail = new Cocktail { Id = "2" };
            var user = new AppUser { Id = "1" };
            var review1 = new CocktailReview { Id = "1", Rating = 6, Description = "0100101", UserId = user.Id, CocktailId = "2" };
            var review2 = new CocktailReview { Id = "2", Rating = 10, Description = "0100101", UserId = user.Id, CocktailId = "2" };
            var like1 = new CocktailReviewLike { Id = "1", CocktailReviewID = "1", AppUserID = "1" };
            //var like2 = new BarReviewLike { Id = "2", BarReviewID = "2", AppUserID="1" };
            using (var arrangeContext = new CMContext(options))
            {
                var sut = new ReviewServices(arrangeContext);
                arrangeContext.Add(cocktail);
                arrangeContext.Add(user);
                arrangeContext.Add(review1);
                arrangeContext.Add(review2);
                arrangeContext.Add(like1);
                await arrangeContext.SaveChangesAsync();
                var ex = await Assert.ThrowsExceptionAsync<MagicException>(
                 async () => await sut.LikeCocktailReview(review1.Id, user.Id)
                );
                Assert.AreEqual(ExceptionMessages.ReviewIsLiked, ex.Message);
            }
        }
        [TestMethod]
        public async Task GetCorrectCountCocktailReviewLikes_WhenValidParamethersArePassed()
        {
            var options = TestUtils.GetOptions(nameof(GetCorrectCountCocktailReviewLikes_WhenValidParamethersArePassed));

            var cocktail = new Cocktail { Id = "2" };
            var user = new AppUser { Id = "1" };
            var user2 = new AppUser { Id = "2" };
            var review1 = new CocktailReview { Id = "1", Rating = 6, Description = "0100101", UserId = user.Id, CocktailId = "2" };
            var review2 = new CocktailReview { Id = "2", Rating = 10, Description = "0100101", UserId = user.Id, CocktailId = "2" };
            var like1 = new CocktailReviewLike { Id = "1", CocktailReviewID = "1", AppUserID = "1" };
            var like2 = new CocktailReviewLike { Id = "2", CocktailReviewID = "1", AppUserID="1" };
            using (var arrangeContext = new CMContext(options))
            {
                var sut = new ReviewServices(arrangeContext);
                arrangeContext.Add(cocktail);
                arrangeContext.Add(user);
                arrangeContext.Add(review1);
                arrangeContext.Add(review2);
                arrangeContext.Add(like1);
                arrangeContext.Add(like2);
                await arrangeContext.SaveChangesAsync();
                var result = await sut.LikeCocktailReview("1", user2.Id);
                Assert.AreEqual(3, result);
            }
        }
        [TestMethod]
        public async Task ThrowExceptionForCocktailReview_WhenStringBarIdIsNull()
        {
            var options = TestUtils.GetOptions(nameof(ThrowExceptionForCocktailReview_WhenStringBarIdIsNull));

            using (var arrangeContext = new CMContext(options))
            {
                var sut = new ReviewServices(arrangeContext);

                var ex = await Assert.ThrowsExceptionAsync<MagicException>(
                  async () => await sut.LikeCocktailReview( null, "1")
                  );
            }
        }

        [TestMethod]
        public async Task ThrowCorrectMessageForCocktailReview_WhenStringBarIdIsNull()
        {
            var options = TestUtils.GetOptions(nameof(ThrowCorrectMessageForCocktailReview_WhenStringBarIdIsNull));

            using (var arrangeContext = new CMContext(options))
            {
                var sut = new ReviewServices(arrangeContext);

                var ex = await Assert.ThrowsExceptionAsync<MagicException>(
                  async () => await sut.LikeCocktailReview(null, "1")
                  );
                Assert.AreEqual(ExceptionMessages.IdNull, ex.Message);
            }
        }

        [TestMethod]
        public async Task ThrowExceptionForCocktailReview_WhenStringUserIdIsNull()
        {
            var options = TestUtils.GetOptions(nameof(ThrowExceptionForCocktailReview_WhenStringUserIdIsNull));

            using (var arrangeContext = new CMContext(options))
            {
                var sut = new ReviewServices(arrangeContext);

                var ex = await Assert.ThrowsExceptionAsync<MagicException>(
                  async () => await sut.LikeCocktailReview("1", null)
                  );
            }
        }

        [TestMethod]
        public async Task ThrowCorrectMessageForCocktailReview_WhenStringUserIdIsNull()
        {
            var options = TestUtils.GetOptions(nameof(ThrowCorrectMessageForCocktailReview_WhenStringUserIdIsNull));

            using (var arrangeContext = new CMContext(options))
            {
                var sut = new ReviewServices(arrangeContext);

                var ex = await Assert.ThrowsExceptionAsync<MagicException>(
                  async () => await sut.LikeCocktailReview("1", null)
                  );
                Assert.AreEqual(ExceptionMessages.IdNull, ex.Message);
            }
        }
    }
}
