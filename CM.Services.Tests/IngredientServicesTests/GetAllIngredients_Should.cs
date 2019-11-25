using CM.Data;
using CM.DTOs;
using CM.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CM.Services.Tests.IngredientServicesTests
{
    [TestClass]
    public class GetAllIngredients_Should
    {
        [TestMethod]
        public async Task Return_IListOf_AllIngredientsDtos()
        {
            var options = TestUtils.GetOptions
                (nameof(Return_IListOf_AllIngredientsDtos));

            using (var arrangeContext = new CMContext(options))
            {
                //1
                arrangeContext.Cocktails.Add(
                    new Cocktail
                    {
                        Id = "1",
                        Name = "Cocktail"
                    });
                arrangeContext.Ingredients.Add(
                    new Ingredient
                    {
                        Id = "1",
                        Name = "target1",
                        CocktailComponents = new List<CocktailComponent>
                        {
                            new CocktailComponent
                            {
                                Id = "1",
                                Name = "vodka",
                                IngredientId = "1",
                                CocktailId = "1"
                            }
                        }
                    });
                //2
                arrangeContext.Cocktails.Add(
                    new Cocktail
                    {
                        Id = "2",
                        Name = "Cocktail2"
                    });
                arrangeContext.Ingredients.Add(
                    new Ingredient
                    {
                        Id = "2",
                        Name = "target2",
                        CocktailComponents = new List<CocktailComponent>
                        {
                            new CocktailComponent
                            {
                                Id = "2",
                                Name = "vodka",
                                IngredientId = "2",
                                CocktailId = "2"
                            }
                        }
                    });

                await arrangeContext.SaveChangesAsync();
            }

            using (var assertContext = new CMContext(options))
            {
                var sut = new IngredientServices(assertContext);

                var result = await sut.GetAllIngredients();

                Assert.AreEqual("target1", result[0].Name);
                Assert.AreEqual("target2", result[1].Name);
                Assert.AreEqual("1", result[0].Id);
                Assert.AreEqual("2", result[1].Id);
                Assert.IsInstanceOfType(result,typeof(IList<IngredientDTO>));
            }
        }

        [TestMethod]
        public async Task Return_IListOf_AllIngredientsDtos_WhichAreNotDeleted()
        {
            var options = TestUtils.GetOptions
                (nameof(Return_IListOf_AllIngredientsDtos_WhichAreNotDeleted));

            using (var arrangeContext = new CMContext(options))
            {
                //1
                arrangeContext.Cocktails.Add(
                    new Cocktail
                    {
                        Id = "1",
                        Name = "Cocktail"
                    });
                arrangeContext.Ingredients.Add(
                    new Ingredient
                    {
                        Id = "1",
                        Name = "target1",
                        CocktailComponents = new List<CocktailComponent>
                        {
                            new CocktailComponent
                            {
                                Id = "1",
                                Name = "vodka",
                                IngredientId = "1",
                                CocktailId = "1",
                            }
                        },
                        DateDeleted = DateTime.Now
                    });
                //2
                arrangeContext.Cocktails.Add(
                    new Cocktail
                    {
                        Id = "2",
                        Name = "Cocktail2"
                    });
                arrangeContext.Ingredients.Add(
                    new Ingredient
                    {
                        Id = "2",
                        Name = "target2",
                        CocktailComponents = new List<CocktailComponent>
                        {
                            new CocktailComponent
                            {
                                Id = "2",
                                Name = "vodka",
                                IngredientId = "2",
                                CocktailId = "2"
                            }
                        }
                    });

                await arrangeContext.SaveChangesAsync();
            }

            using (var assertContext = new CMContext(options))
            {
                var sut = new IngredientServices(assertContext);

                var result = await sut.GetAllIngredients();

                Assert.AreEqual("target2", result[0].Name);
                Assert.AreEqual("2", result[0].Id);
                Assert.AreEqual(1, result.Count);
                Assert.IsInstanceOfType(result, typeof(IList<IngredientDTO>));
            }
        }


        [TestMethod]
        public async Task Return_EmptyList_IfThereAreNoIngredients()
        {
            var options = TestUtils.GetOptions
                (nameof(Return_EmptyList_IfThereAreNoIngredients));

            using (var assertContext = new CMContext(options))
            {
                var sut = new IngredientServices(assertContext);

                var result = await sut.GetAllIngredients();

                Assert.IsInstanceOfType(result, typeof(IList<IngredientDTO>));
                Assert.AreEqual(result.Count, 0);
            }
        }

    }
}
