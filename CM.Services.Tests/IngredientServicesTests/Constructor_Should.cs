using CM.Data;
using CM.Services.CustomExeptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CM.Services.Tests.IngredientServicesTests
{
    [TestClass]
    public class Constructor_Should
    {
        [TestMethod]
        public async Task MakeInstance_OfTypeIngredientService_WhenValidValuesPassed()
        {
            var options = TestUtils.GetOptions(nameof(MakeInstance_OfTypeIngredientService_WhenValidValuesPassed));
            using (var assertContext = new CMContext(options))
            {
                var sut = new IngredientServices(assertContext);
                Assert.IsInstanceOfType(sut, typeof(IngredientServices));
            }
        }
        [TestMethod]
        public async Task ThrowMagicExeption_IfNullValue_DbContextPassed()
        {
            Assert.ThrowsException<MagicExeption>
                ( () => new IngredientServices(null));
        }
        [TestMethod]
        public async Task ThrowCorrectMagicExeption_IfNullValue_DbContextPassed()
        {
            var ex = Assert.ThrowsException<MagicExeption>
                (() => new IngredientServices(null));
            Assert.AreEqual("CMContext cannot be null!", ex.Message);
        }
    }
}
