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
    public class RemoveCocktailReviewLike_Should
    {
        [TestMethod]
        public async Task RemoveLikeCocktailReview_WhenValidBarIdIsPassed()
        {
            var options = TestUtils.GetOptions(nameof(RemoveLikeCocktailReview_WhenValidBarIdIsPassed));
            var cocktail = new Cocktail { Id = "2" };
            var user = new AppUser { Id = "1" };
            var review1 = new CocktailReview { Id = "1", Rating = 6, Description = "0100101", CocktailId = "2" };
            var review2 = new CocktailReview { Id = "2", Rating = 10, Description = "0100101", CocktailId = "2" };
            var like1 = new CocktailReviewLike { Id = "1", AppUserID = "1",  CocktailReviewID=review1.Id };
            //var like2 = new BarReviewLike { Id = "2", BarReviewID = "2", AppUserID="1" };

            using (var arrangeContext = new CMContext(options))
            {
                var sut = new ReviewServices(arrangeContext);
                arrangeContext.Add(cocktail);
                arrangeContext.Add(user);
                arrangeContext.Add(review1);
                arrangeContext.Add(like1);
                await arrangeContext.SaveChangesAsync();
                Assert.AreEqual(1, arrangeContext.CocktailReviewLikes.Count());
                await sut.RemoveCocktailReviewLike(review1.Id, user.Id);
                Assert.AreEqual(0, arrangeContext.CocktailReviewLikes.Count());
            }
        }

        [TestMethod]
        public async Task ThrowException_WhenCocktailLikeDoesntExist()
        {
            var options = TestUtils.GetOptions(nameof(ThrowException_WhenCocktailLikeDoesntExist));

            var cocktail = new Cocktail { Id = "2" };
            var user = new AppUser { Id = "1" };
            var review1 = new CocktailReview { Id = "1", Rating = 6, Description = "0100101", CocktailId = "2" };
            var review2 = new CocktailReview { Id = "2", Rating = 10, Description = "0100101", CocktailId = "2" };
            //var like1 = new CocktaiilReviewLike { Id = "1", BarReviewID = "1", AppUserID = "1" };
            //var like2 = new BarReviewLike { Id = "2", BarReviewID = "2", AppUserID="1" };
            using (var arrangeContext = new CMContext(options))
            {
                var sut = new ReviewServices(arrangeContext);
                arrangeContext.Add(cocktail);
                arrangeContext.Add(user);
                arrangeContext.Add(review1);
                await arrangeContext.SaveChangesAsync();
                var ex = await Assert.ThrowsExceptionAsync<MagicException>(
                  async () => await sut.RemoveBarReviewLike(review1.Id, user.Id)
                  );
            }
        }

        [TestMethod]
        public async Task ThrowExceptionMessage_WhenCocktailLikeDoesntExist()
        {
            var options = TestUtils.GetOptions(nameof(ThrowExceptionMessage_WhenCocktailLikeDoesntExist));
                        
            var cocktail = new Cocktail { Id = "2" };
            var user = new AppUser { Id = "1" };
            var review1 = new CocktailReview { Id = "1", Rating = 6, Description = "0100101", CocktailId = "2" };
            var review2 = new CocktailReview { Id = "2", Rating = 10, Description = "0100101", CocktailId = "2" };
            //var like1 = new CocktaiilReviewLike { Id = "1", BarReviewID = "1", AppUserID = "1" };
            //var like2 = new BarReviewLike { Id = "2", BarReviewID = "2", AppUserID="1" };
            using (var arrangeContext = new CMContext(options))
            {
                var sut = new ReviewServices(arrangeContext);
                arrangeContext.Add(cocktail);
                arrangeContext.Add(user);
                arrangeContext.Add(review1);
                await arrangeContext.SaveChangesAsync();
                var ex = await Assert.ThrowsExceptionAsync<MagicException>(
                  async () => await sut.RemoveBarReviewLike(review1.Id, user.Id)
                  );
                Assert.AreEqual(ExceptionMessages.LikeNull, ex.Message);
            }
        }

        [TestMethod]
        public async Task ReturnCorrectCountOfCocktailLikes_WhenLikeIsRemoved()
        {
            var options = TestUtils.GetOptions(nameof(ReturnCorrectCountOfCocktailLikes_WhenLikeIsRemoved));


            var cocktail = new Cocktail { Id = "2" };
            var user = new AppUser { Id = "1" };
            var review1 = new CocktailReview { Id = "1", Rating = 6, Description = "0100101", CocktailId = "2" };
            var review2 = new CocktailReview { Id = "2", Rating = 10, Description = "0100101", CocktailId = "2" };
            var like1 = new CocktailReviewLike { Id = "1", CocktailReviewID = "1", AppUserID = "1" };
            var like2 = new CocktailReviewLike { Id = "2", CocktailReviewID = "1", AppUserID = "2" };
            using (var arrangeContext = new CMContext(options))
            {
                var sut = new ReviewServices(arrangeContext);
                arrangeContext.Add(cocktail);
                arrangeContext.Add(user);
                arrangeContext.Add(review1);
                arrangeContext.Add(like1);
                arrangeContext.Add(like2);
                await arrangeContext.SaveChangesAsync();
                Assert.AreEqual(2, arrangeContext.CocktailReviewLikes.Count());
                var result = await sut.RemoveCocktailReviewLike(review1.Id, user.Id);
                Assert.AreEqual(1, result);
            }
        }
        //[TestMethod]
        //public async Task ThrowExceptionForRemoveBarLike_WhenStringBarIdIsNull()
        //{
        //    var options = TestUtils.GetOptions(nameof(ThrowExceptionForRemoveBarLike_WhenStringBarIdIsNull));

        //    using (var arrangeContext = new CMContext(options))
        //    {
        //        var sut = new ReviewServices(arrangeContext);

        //        var ex = await Assert.ThrowsExceptionAsync<MagicException>(
        //          async () => await sut.RemoveBarReviewLike(null, "1")
        //          );
        //    }
        //}

        //[TestMethod]
        //public async Task ThrowCorrectMessageForRemoveBarLike_WhenStringBarIdIsNull()
        //{
        //    var options = TestUtils.GetOptions(nameof(ThrowCorrectMessageForRemoveBarLike_WhenStringBarIdIsNull));

        //    using (var arrangeContext = new CMContext(options))
        //    {
        //        var sut = new ReviewServices(arrangeContext);

        //        var ex = await Assert.ThrowsExceptionAsync<MagicException>(
        //          async () => await sut.RemoveBarReviewLike(null, "1")
        //          );
        //        Assert.AreEqual(ExceptionMessages.IdNull, ex.Message);
        //    }
        //}

        //[TestMethod]
        //public async Task ThrowExceptionForRemoveBarLike_WhenStringUserIdIsNull()
        //{
        //    var options = TestUtils.GetOptions(nameof(ThrowExceptionForRemoveBarLike_WhenStringUserIdIsNull));

        //    using (var arrangeContext = new CMContext(options))
        //    {
        //        var sut = new ReviewServices(arrangeContext);
        //        var ex = await Assert.ThrowsExceptionAsync<MagicException>(
        //          async () => await sut.RemoveBarReviewLike("1", null)
        //          );
        //    }
        //}

        //[TestMethod]
        //public async Task ThrowCorrectMessageForRemoveBarLike_WhenStringUserIdIsNull()
        //{
        //    var options = TestUtils.GetOptions(nameof(ThrowCorrectMessageForRemoveBarLike_WhenStringUserIdIsNull));

        //    using (var arrangeContext = new CMContext(options))
        //    {
        //        var sut = new ReviewServices(arrangeContext);
        //        var ex = await Assert.ThrowsExceptionAsync<MagicException>(
        //          async () => await sut.RemoveBarReviewLike("1", null)
        //          );
        //        Assert.AreEqual(ExceptionMessages.IdNull, ex.Message);
        //    }
        //}
    }
}
