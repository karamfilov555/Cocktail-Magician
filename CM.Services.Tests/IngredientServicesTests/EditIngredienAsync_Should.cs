using CM.Data;
using CM.DTOs;
using CM.Models;
using CM.Services.CustomExeptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CM.Services.Tests.IngredientServicesTests
{
    [TestClass]
    public class EditIngredienAsync_Should
    {
        [TestMethod]
        public async Task ReturnUpdatedResultName_WhenValidIngredientDto_Passed()
        {
            var options = TestUtils.GetOptions(nameof(ReturnUpdatedResultName_WhenValidIngredientDto_Passed));

            using (var assertContext = new CMContext(options))
            {
                var sut = new IngredientServices(assertContext);
                assertContext.Ingredients.Add(
                    new Ingredient
                    {
                        Id = "1",
                        Name = "water",
                        Brand = "perrier"
                    });
                await assertContext.SaveChangesAsync();

                var result = await sut.EditIngredienAsync(
                    new IngredientDTO
                    {
                        Name = "fanta",
                        Brand = "coca-cola"
                    }
                    );
                Assert.AreEqual("fanta", result);
                //Assert.AreEqual("coca-cola", assertContext.Ingredients.First(x=>x.Id=="1").Brand);
                //Assert.AreEqual("fanta", assertContext.Ingredients.First(x=>x.Id=="1").Name);
            }
        }

        [TestMethod]
        public async Task Throw_MagicExeption_WhenNullIngredientDto_Passed()
        {
            var options = TestUtils.GetOptions(nameof(Throw_MagicExeption_WhenNullIngredientDto_Passed));

            using (var assertContext = new CMContext(options))
            {
                var sut = new IngredientServices(assertContext);

                var ex = await Assert.ThrowsExceptionAsync<MagicExeption>(
                    async () => await sut.EditIngredienAsync(null));
            }
        }

        [TestMethod]
        public async Task Throw_CorrectMagicExeption_WhenNullIngredientDto_Passed()
        {
            var options = TestUtils.GetOptions(nameof(Throw_CorrectMagicExeption_WhenNullIngredientDto_Passed));

            using (var assertContext = new CMContext(options))
            {
                var sut = new IngredientServices(assertContext);

                var ex = await Assert.ThrowsExceptionAsync<MagicExeption>(
                    async () => await sut.EditIngredienAsync(null));

                Assert.AreEqual("IngredientDto cannot be null!", ex.Message);
            }
        }



    }
}
