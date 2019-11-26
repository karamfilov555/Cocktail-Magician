using CM.Data;
using CM.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CM.Services.Tests.IngredientServicesTests
{
    [TestClass]
    public class GetImagesForHpAsync_Should
    {
        [TestMethod]
        public async Task Return_ICollection_ImageUrlsAsString_OfExactIngredientModels_IdsFromJsonSeed()
        {
            var options = TestUtils.GetOptions
                (nameof(Return_ICollection_ImageUrlsAsString_OfExactIngredientModels_IdsFromJsonSeed));

            using (var arrangeContext = new CMContext(options))
            {
                //1
                arrangeContext.Ingredients.Add(
                    new Ingredient
                    {
                        Id = "10",
                        Name = "target1",
                        ImageUrl ="img1"
                    });
                //2
                arrangeContext.Ingredients.Add(
                    new Ingredient
                    {
                        Id = "13",
                        Name = "target2",
                        ImageUrl = "img2"
                    });
                //3
                arrangeContext.Ingredients.Add(
                    new Ingredient
                    {
                        Id = "14",
                        Name = "target3",
                        ImageUrl = "img3"
                    });
                //4
                arrangeContext.Ingredients.Add(
                    new Ingredient
                    {
                        Id = "15",
                        Name = "target4",
                        ImageUrl = "img4"
                    });
                //5
                arrangeContext.Ingredients.Add(
                    new Ingredient
                    {
                        Id = "9",
                        Name = "target5",
                        ImageUrl = "img5"
                    });

                await arrangeContext.SaveChangesAsync();
            }

            using (var assertContext = new CMContext(options))
            {
                var sut = new IngredientServices(assertContext);

                var result = await sut.GetImagesForHpAsync();

                Assert.AreEqual("img1", result.ToList()[0]);
                Assert.AreEqual("img2", result.ToList()[1]);
                Assert.AreEqual("img3", result.ToList()[2]);
                Assert.AreEqual("img4", result.ToList()[3]);
                Assert.AreEqual("img5", result.ToList()[4]);
                Assert.AreEqual(5, result.ToList().Count());

                Assert.IsInstanceOfType(result, typeof(ICollection<String>));
            }
        }

        [TestMethod]
        public async Task Return_EmptyICollection_IfThereIsNoIngredients()
        {
            var options = TestUtils.GetOptions
                (nameof(Return_EmptyICollection_IfThereIsNoIngredients));

            using (var assertContext = new CMContext(options))
            {
                var sut = new IngredientServices(assertContext);

                var result = await sut.GetImagesForHpAsync();

                Assert.AreEqual(0, result.ToList().Count());

                Assert.IsInstanceOfType(result, typeof(ICollection<String>));
            }
        }
    }
}
