using CM.Data;
using CM.DTOs;
using CM.Models;
using CM.Services.CustomExceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CM.Services.Tests.IngredientServicesTests
{
    [TestClass]
    public class GetIngredientById_Should
    {
        [TestMethod]
        public async Task Return_IngredientDto_WhenValidValuesPassed()
        {
            var options = TestUtils.GetOptions(nameof(Return_IngredientDto_WhenValidValuesPassed));

            using (var arrangeContext = new CMContext(options))
            {
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

                var result =  await sut.GetIngredientById("1");
                Assert.AreEqual("target1", result.Name);
                Assert.AreEqual("1", result.Id);
                Assert.IsInstanceOfType(result,typeof(IngredientDTO));
            }
        }

        [TestMethod]
        public async Task Throw_MagicExeption_IfIngredient_IsDeleted()
        {
            var options = TestUtils.GetOptions(nameof(Throw_MagicExeption_IfIngredient_IsDeleted));

            using (var arrangeContext = new CMContext(options))
            {
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
                    async () => await sut.GetIngredientById("1"));
            }
        }
        [TestMethod]
        public async Task Throw_CorrectMagicExeption_IfIngredient_IsDeleted()
        {
            var options = TestUtils.GetOptions(nameof(Throw_CorrectMagicExeption_IfIngredient_IsDeleted));

            using (var arrangeContext = new CMContext(options))
            {
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
                    async () => await sut.GetIngredientById("1"));
                Assert.AreEqual("Ingredient cannot be null!", ex.Message);
            }
        }
        [TestMethod]
        public async Task Throw_CorrectMagicMsg_IngredientWhitSuchId_DoesNotExist()
        {
            var options = TestUtils.GetOptions(nameof(Throw_CorrectMagicMsg_IngredientWhitSuchId_DoesNotExist));

            using (var assertContext = new CMContext(options))
            {
                var sut = new IngredientServices(assertContext);

                var ex = await Assert.ThrowsExceptionAsync<MagicException>(
                    async () => await sut.GetIngredientById("ddd"));
                Assert.AreEqual("Ingredient cannot be null!", ex.Message);
            }
        }

        [TestMethod]
        public async Task Throw_MagicExeption_IngredientWhitSuchId_DoesNotExist()
        {
            var options = TestUtils.GetOptions(nameof(Throw_MagicExeption_IngredientWhitSuchId_DoesNotExist));

            using (var assertContext = new CMContext(options))
            {
                var sut = new IngredientServices(assertContext);

                var ex = await Assert.ThrowsExceptionAsync<MagicException>(
                    async () => await sut.GetIngredientById("ddd"));
            }
        }
        [TestMethod]
        public async Task Throw_MagicExeption_IfNullId_Passed()
        {
            var options = TestUtils.GetOptions(nameof(Throw_MagicExeption_IfNullId_Passed));

            using (var assertContext = new CMContext(options))
            {
                var sut = new IngredientServices(assertContext);

                var ex = await Assert.ThrowsExceptionAsync<MagicException>(
                    async () => await sut.GetIngredientById(null));
            }
        }
        [TestMethod]
        public async Task Throw_CorrectMagicMsg_IfNullId_Passed()
        {
            var options = TestUtils.GetOptions(nameof(Throw_CorrectMagicMsg_IfNullId_Passed));

            using (var assertContext = new CMContext(options))
            {
                var sut = new IngredientServices(assertContext);

                var ex = await Assert.ThrowsExceptionAsync<MagicException>(
                    async () => await sut.GetIngredientById(null));
                Assert.AreEqual("ID cannot be null!", ex.Message);
            }
        }
    }
}
