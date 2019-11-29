using CM.Data;
using CM.DTOs;
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
    public class GetAllIngredientsNames_Should
    {
        [TestMethod]
        public async Task Return_ICollection_OFAllIngredientNames_WhichAreNotDeleted_AsString()
        {
            var options = TestUtils.GetOptions
                (nameof(Return_ICollection_OFAllIngredientNames_WhichAreNotDeleted_AsString));

            using (var arrangeContext = new CMContext(options))
            {
               //1
                arrangeContext.Ingredients.Add(
                    new Ingredient
                    {
                        Id = "1",
                        Name = "target1"
                    });
                //2
                arrangeContext.Ingredients.Add(
                    new Ingredient
                    {
                        Id = "2",
                        Name = "target2"
                    });

                await arrangeContext.SaveChangesAsync();
            }

            using (var assertContext = new CMContext(options))
            {
                var sut = new IngredientServices(assertContext);

                var result = await sut.GetAllIngredientsNames();

                Assert.AreEqual("target1", result.ToList()[0]);
                Assert.AreEqual("target2", result.ToList()[1]);
                Assert.AreEqual(2, result.ToList().Count());
            
                Assert.IsInstanceOfType(result, typeof(ICollection<String>));
            }
        }

        [TestMethod]
        public async Task DontReturnNames_OfDeletedIngredients()
        {
            var options = TestUtils.GetOptions
                (nameof(DontReturnNames_OfDeletedIngredients));

            using (var arrangeContext = new CMContext(options))
            {
                //1
                arrangeContext.Ingredients.Add(
                    new Ingredient
                    {
                        Id = "1",
                        Name = "target1"
                    });
                //2
                arrangeContext.Ingredients.Add(
                    new Ingredient
                    {
                        Id = "2",
                        Name = "target2",
                        DateDeleted = DateTime.Now
                    });

                await arrangeContext.SaveChangesAsync();
            }

            using (var assertContext = new CMContext(options))
            {
                var sut = new IngredientServices(assertContext);

                var result = await sut.GetAllIngredientsNames();

                Assert.AreEqual("target1", result.ToList()[0]);
                Assert.AreEqual(1, result.ToList().Count());

                Assert.IsInstanceOfType(result, typeof(ICollection<String>));
            }
        }

        [TestMethod]
        public async Task ReturnEmptyICollection_IfThereIsNoIngredients()
        {
            var options = TestUtils.GetOptions
                (nameof(ReturnEmptyICollection_IfThereIsNoIngredients));

            using (var arrangeContext = new CMContext(options))
            {
                //1
                arrangeContext.Ingredients.Add(
                    new Ingredient
                    {
                        Id = "1",
                        Name = "target1",
                        DateDeleted = DateTime.Now
                    });
                //2
                arrangeContext.Ingredients.Add(
                    new Ingredient
                    {
                        Id = "2",
                        Name = "target2",
                        DateDeleted = DateTime.Now
                    });

                await arrangeContext.SaveChangesAsync();
            }

            using (var assertContext = new CMContext(options))
            {
                var sut = new IngredientServices(assertContext);

                var result = await sut.GetAllIngredientsNames();

                Assert.AreEqual(0, result.ToList().Count());

                Assert.IsInstanceOfType(result, typeof(ICollection<String>));
            }
        }
    }
}
