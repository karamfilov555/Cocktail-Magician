using CM.Data;
using CM.Models;
using CM.Services.CustomExceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CM.Services.Tests.IngredientServicesTests
{
    [TestClass]
    public class DeleteIngredientAsync_Should
    {
        [TestMethod]
        public async Task DeleteIngredient_IfItsNotPartOfAnyCocktail()
        {
            var options = TestUtils.GetOptions
                (nameof(DeleteIngredient_IfItsNotPartOfAnyCocktail));

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
                        Name = "target1"
                      
                    });

                await arrangeContext.SaveChangesAsync();
            }

            using (var assertContext = new CMContext(options))
            {
                var sut = new IngredientServices(assertContext);

                var result = await sut.DeleteIngredientAsync("1");
                Assert.AreEqual(0, assertContext.Ingredients.Count());
            }
        }
        [TestMethod]
        public async Task Return_IngredientName_IfItsDeletedSuccessfully()
        {
            var options = TestUtils.GetOptions
                (nameof(Return_IngredientName_IfItsDeletedSuccessfully));

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
                        Name = "target1"

                    });

                await arrangeContext.SaveChangesAsync();
            }

            using (var assertContext = new CMContext(options))
            {
                var sut = new IngredientServices(assertContext);

                var result = await sut.DeleteIngredientAsync("1");
                Assert.AreEqual("target1", result);
            }
        }

        [TestMethod]
        public async Task Throw_MagicExeption_IfIngredientIsAlreadyDeleted()
        {
            var options = TestUtils.GetOptions
                (nameof(Throw_MagicExeption_IfIngredientIsAlreadyDeleted));

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
                        DateDeleted= DateTime.Now

                    });

                await arrangeContext.SaveChangesAsync();
            }

            using (var assertContext = new CMContext(options))
            {
                var sut = new IngredientServices(assertContext);

                var ex = await Assert.ThrowsExceptionAsync<MagicException>(
                   async () => await sut.DeleteIngredientAsync("1"));
            }
        }

        [TestMethod]
        public async Task Throw_CorrectMagicExeption_IfIngredientIsAlreadyDeleted()
        {
            var options = TestUtils.GetOptions
                (nameof(Throw_CorrectMagicExeption_IfIngredientIsAlreadyDeleted));

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
                        DateDeleted = DateTime.Now

                    });

                await arrangeContext.SaveChangesAsync();
            }

            using (var assertContext = new CMContext(options))
            {
                var sut = new IngredientServices(assertContext);

                var ex = await Assert.ThrowsExceptionAsync<MagicException>(
                   async () => await sut.DeleteIngredientAsync("1"));
                Assert.AreEqual("Ingredient cannot be null!", ex.Message);
            }
        }


        [TestMethod]
        public async Task Throw_MagicExeption_WhenNullIdPassed()
        {
            var options = TestUtils.GetOptions(nameof(Throw_MagicExeption_WhenNullIdPassed));

            using (var assertContext = new CMContext(options))
            {
                var sut = new IngredientServices(assertContext);

                var ex = await Assert.ThrowsExceptionAsync<MagicException>(
                    async () => await sut.DeleteIngredientAsync(null));
            }
        }
        [TestMethod]
        public async Task Throw_CorrectMagicMsg_WhenNullIdPassed()
        {
            var options = TestUtils.GetOptions(nameof(Throw_CorrectMagicMsg_WhenNullIdPassed));

            using (var assertContext = new CMContext(options))
            {
                var sut = new IngredientServices(assertContext);

                var ex = await Assert.ThrowsExceptionAsync<MagicException>(
                    async () => await sut.DeleteIngredientAsync(null));
                Assert.AreEqual("ID cannot be null!", ex.Message);
            }
        }
        [TestMethod]
        public async Task Throw_MagicExeption_WhenIngredientWithSuchId_DoesnotExist()
        {
            var options = TestUtils.GetOptions(nameof(Throw_MagicExeption_WhenIngredientWithSuchId_DoesnotExist));

            using (var assertContext = new CMContext(options))
            {
                var sut = new IngredientServices(assertContext);

                var ex = await Assert.ThrowsExceptionAsync<MagicException>(
                    async () => await sut.DeleteIngredientAsync("lll"));
            }
        }
        [TestMethod]
        public async Task Throw_CorectMagicExeption_WhenIngredientWithSuchId_DoesnotExist()
        {
            var options = TestUtils.GetOptions(nameof(Throw_CorectMagicExeption_WhenIngredientWithSuchId_DoesnotExist));

            using (var assertContext = new CMContext(options))
            {
                var sut = new IngredientServices(assertContext);

                var ex = await Assert.ThrowsExceptionAsync<MagicException>(
                    async () => await sut.DeleteIngredientAsync("lll"));
                Assert.AreEqual("Ingredient cannot be null!", ex.Message);
            }
        }
        [TestMethod]
        public async Task Throw_MagicExeption_IfYouTryToDeleteIngredient_WichIsPartOfCocktail()
        {
            var options = TestUtils.GetOptions
                (nameof(Throw_MagicExeption_IfYouTryToDeleteIngredient_WichIsPartOfCocktail));

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

                await arrangeContext.SaveChangesAsync();
            }

            using (var assertContext = new CMContext(options))
            {
                var sut = new IngredientServices(assertContext);

                var ex = await Assert.ThrowsExceptionAsync<MagicException>(
                    async () => await sut.DeleteIngredientAsync("1"));

            }
        }

        [TestMethod]
        public async Task Throw_CorrectMagicExeption_IfYouTryToDeleteIngredient_WichIsPartOfCocktail()
        {
            var options = TestUtils.GetOptions
                (nameof(Throw_CorrectMagicExeption_IfYouTryToDeleteIngredient_WichIsPartOfCocktail));

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

                await arrangeContext.SaveChangesAsync();
            }

            using (var assertContext = new CMContext(options))
            {
                var sut = new IngredientServices(assertContext);

                var ex = await Assert.ThrowsExceptionAsync<MagicException>(
                    async () => await sut.DeleteIngredientAsync("1"));
                Assert.AreEqual("You cannot delete this ingredient", ex.Message);
            }
        }
    }
}
