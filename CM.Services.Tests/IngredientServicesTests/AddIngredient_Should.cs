using CM.Data;
using CM.DTOs;
using CM.Models;
using CM.Services.CustomExeptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CM.Services.Tests.IngredientServicesTests
{
    [TestClass]
    public class AddIngredient_Should
    {
        [TestMethod]
        public async Task AddIngredient_ToDb_WhenValidIngredientDtoPassed()
        {
            var options = TestUtils.GetOptions(nameof(AddIngredient_ToDb_WhenValidIngredientDtoPassed));

            using (var arrangeContext = new CMContext(options))
            {
                var sut = new IngredientServices(arrangeContext);

                await sut.AddIngredient(
                        new IngredientDTO
                        {
                            Name = "voda",
                        });
            }
            using (var assertContext = new CMContext(options))
            {
                Assert.AreEqual("voda", assertContext.Ingredients.First().Name);
            }
        }
        [TestMethod]
        public async Task Throw_MagicMsg_WhenNullIngredientDtoPassed()
        {
            var options = TestUtils.GetOptions(nameof(Throw_MagicMsg_WhenNullIngredientDtoPassed));

            using (var assertContext = new CMContext(options))
            {
                var sut = new IngredientServices(assertContext);

                var ex = await Assert.ThrowsExceptionAsync<MagicExeption>(
                    async () => await sut.AddIngredient(null));
            }
        }

        [TestMethod]
        public async Task Throw_CorrectMagicMsg_WhenNullIngredientDtoPassed()
        {
            var options = TestUtils.GetOptions(nameof(Throw_MagicMsg_WhenNullIngredientDtoPassed));

            using (var assertContext = new CMContext(options))
            {
                var sut = new IngredientServices(assertContext);

                var ex = await Assert.ThrowsExceptionAsync<MagicExeption>(
                    async () => await sut.AddIngredient(null));

                Assert.AreEqual("IngredientDto cannot be null!", ex.Message);
            }
        }
    }
}
