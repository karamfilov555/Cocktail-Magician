using CM.Data;
using CM.Services.Contracts;
using CM.Services.CustomExceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CM.Services.Tests.CocktailServiceTests
{
    [TestClass]
    public class CheckIfCocktailExist_Should
    {
        Mock<IIngredientServices> _ingredientServices;
        Mock<IRecipeServices> _recipeServices;
        Mock<IFileUploadService> _fileUploadService;


        public CheckIfCocktailExist_Should()
        {
            _ingredientServices = new Mock<IIngredientServices>();
            _recipeServices = new Mock<IRecipeServices>();
            _fileUploadService = new Mock<IFileUploadService>();
        }

        [TestMethod]
        public async Task ThrowException_WhenPassedIdIsNull()
        {
            var options = TestUtils.GetOptions(nameof(ThrowException_WhenPassedIdIsNull));

            using (var assertContext = new CMContext(options))
            {
                var sut = new CocktailServices(assertContext, _fileUploadService.Object,
                    _ingredientServices.Object, _recipeServices.Object);
                var ex = await Assert.ThrowsExceptionAsync<MagicException>(
                  async () => await sut.CheckIfCocktailExist(null)
                  );
            }
        }

        [TestMethod]
        public async Task ThrowCorrectMessage_WhenPassedIdIsNull()
        {
            var options = TestUtils.GetOptions(nameof(ThrowCorrectMessage_WhenPassedIdIsNull));

            using (var assertContext = new CMContext(options))
            {
                var sut = new CocktailServices(assertContext, _fileUploadService.Object,
                    _ingredientServices.Object, _recipeServices.Object);
                var ex = await Assert.ThrowsExceptionAsync<MagicException>(
                  async () => await sut.CheckIfCocktailExist(null)
                  );
                Assert.AreEqual("ID cannot be null!", ex.Message);
            }
        }
    }
}
