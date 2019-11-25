using CM.Data;
using CM.Models;
using CM.Services.CustomExeptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CM.Services.Tests.IngredientServicesTests
{
    [TestClass]
    public class GetIngredientNameById_Should
    {
        [TestMethod]
        public async Task Return_IngredientName_WhenValidIdPassed()
        {
            var options = TestUtils.GetOptions(nameof(Return_IngredientName_WhenValidIdPassed));

            using (var assertContext = new CMContext(options))
            {
                var sut = new IngredientServices(assertContext);
                assertContext.Ingredients.Add(new Ingredient { Id = "1", Name = "target" });
                await assertContext.SaveChangesAsync();
                var result = await sut.GetIngredientNameById("1");
                Assert.AreEqual("target", result);
            }
        }

        [TestMethod]
        public async Task Throw_MagicExeption_WhenNullIdPassed()
        {
            var options = TestUtils.GetOptions(nameof(Throw_MagicExeption_WhenNullIdPassed));

            using (var assertContext = new CMContext(options))
            {
                var sut = new IngredientServices(assertContext);

                var ex = await Assert.ThrowsExceptionAsync<MagicExeption>(
                    async () => await sut.GetIngredientNameById(null));
            }
        }
        [TestMethod]
        public async Task Throw_MagicExeption_WhenIngredientWithSuchIdDoesNotExist()
        {
            var options = TestUtils.GetOptions(nameof(Throw_MagicExeption_WhenIngredientWithSuchIdDoesNotExist));

            using (var assertContext = new CMContext(options))
            {
                var sut = new IngredientServices(assertContext);

                var ex = await Assert.ThrowsExceptionAsync<MagicExeption>(
                    async () => await sut.GetIngredientNameById("dddd"));
            }
        }
        [TestMethod]
        public async Task Throw_CorrectMagicExeption_WhenIngredientWithSuchIdDoesNotExist()
        {
            var options = TestUtils.GetOptions(nameof(Throw_MagicExeption_WhenIngredientWithSuchIdDoesNotExist));

            using (var assertContext = new CMContext(options))
            {
                var sut = new IngredientServices(assertContext);

                var ex = await Assert.ThrowsExceptionAsync<MagicExeption>(
                    async () => await sut.GetIngredientNameById("dddd"));

                Assert.AreEqual("Ingredient cannot be null!", ex.Message);
            }
        }
        [TestMethod]
        public async Task Throw_CorrectMagicExeption_WhenNullIdPassed()
        {
            var options = TestUtils.GetOptions(nameof(Throw_CorrectMagicExeption_WhenNullIdPassed));

            using (var assertContext = new CMContext(options))
            {
                var sut = new IngredientServices(assertContext);

                var ex = await Assert.ThrowsExceptionAsync<MagicExeption>(
                    async () => await sut.GetIngredientNameById(null));

                Assert.AreEqual("ID cannot be null!", ex.Message);
            }
        }
    }
}
