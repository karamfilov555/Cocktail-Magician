using CM.Data;
using CM.Models;
using CM.Services.CustomExeptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace CM.Services.Tests.IngredientServicesTests
{
    [TestClass]
    public class GetIngredientIdByNameAsync_Should
    {
        [TestMethod]
        public async Task ReturnId_WhenValidIngredientName_Passed()
        {
            var options = TestUtils.GetOptions(nameof(ReturnId_WhenValidIngredientName_Passed));

            using (var assertContext = new CMContext(options))
            {
                var sut = new IngredientServices(assertContext);
                assertContext.Ingredients.Add(new Ingredient { Id = "33", Name = "water" });
                await assertContext.SaveChangesAsync();

                var result =  await sut.GetIngredientIdByNameAsync("water");

                Assert.AreEqual("33", result);
            }
        }

        [TestMethod]
        public async Task Throw_MagicExeption_WhenNullIngredientOccurs()
        {
            var options = TestUtils.GetOptions(nameof(Throw_MagicExeption_WhenNullIngredientOccurs));

            using (var assertContext = new CMContext(options))
            {
                var sut = new IngredientServices(assertContext);

                var ex = await Assert.ThrowsExceptionAsync<MagicExeption>(
                    async () => await sut.GetIngredientIdByNameAsync("Name"));
            }
        }
        [TestMethod]
        public async Task Throw_CorrectMagicExeption_WhenNullIngredientOccurs()
        {
            var options = TestUtils.GetOptions(nameof(Throw_CorrectMagicExeption_WhenNullIngredientOccurs));

            using (var assertContext = new CMContext(options))
            {
                var sut = new IngredientServices(assertContext);

                var ex = await Assert.ThrowsExceptionAsync<MagicExeption>(
                    async () => await sut.GetIngredientIdByNameAsync("Name"));
                Assert.AreEqual("Ingredient cannot be null!", ex.Message);
            }
        }
    }
}
