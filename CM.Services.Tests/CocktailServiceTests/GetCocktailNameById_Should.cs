using CM.Data;
using CM.Models;
using CM.Services.Contracts;
using CM.Services.CustomExeptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CM.Services.Tests.CocktailServiceTests
{
    [TestClass]
    public class GetCocktailNameById_Should
    {
        Mock<IIngredientServices> _ingredientServices;
        Mock<IRecipeServices> _recipeServices;
        Mock<IFileUploadService> _fileUploadService;


        public GetCocktailNameById_Should()
        {
            _ingredientServices = new Mock<IIngredientServices>();
            _recipeServices = new Mock<IRecipeServices>();
            _fileUploadService = new Mock<IFileUploadService>();
        }

        [TestMethod]
        public async Task GetCorrectName_WhenValidIdIsPassed()
        {
            var options = TestUtils.GetOptions(nameof(GetCorrectName_WhenValidIdIsPassed));
            var cocktailId = "15";
            var cocktailName = "Mojito";
            var newCocktail = new Cocktail() { Id = cocktailId, Name=cocktailName };

            using (var arrangeContext = new CMContext(options))
            {
                arrangeContext.Add(newCocktail);
                await arrangeContext.SaveChangesAsync();
            }
            using (var assertContext = new CMContext(options))
            {
                var sut = new CocktailServices(assertContext, _fileUploadService.Object,
                    _ingredientServices.Object, _recipeServices.Object);
                var result = await sut.GetCocktailNameById(cocktailId);
                Assert.AreEqual(cocktailName, result);
            }
        }

        [TestMethod]
        public async Task ReturnObjectOfTheType_WhenValidIdIsPassed()
        {
            var options = TestUtils.GetOptions(nameof(ReturnObjectOfTheType_WhenValidIdIsPassed));
            var cocktailId = "15";
            var cocktailName = "Mojito";
            var newCocktail = new Cocktail() { Id = cocktailId, Name = cocktailName };

            using (var arrangeContext = new CMContext(options))
            {
                arrangeContext.Add(newCocktail);
                await arrangeContext.SaveChangesAsync();
            }
            using (var assertContext = new CMContext(options))
            {
                var sut = new CocktailServices(assertContext, _fileUploadService.Object,
                    _ingredientServices.Object, _recipeServices.Object);
                var result = await sut.GetCocktailNameById(cocktailId);
                Assert.IsInstanceOfType(result, typeof(string));
            }
        }

        [TestMethod]
        public async Task ThrowException_WhenCocktailDoesNotExist()
        {
            var options = TestUtils.GetOptions(nameof(ThrowException_WhenCocktailDoesNotExist));
            var cocktailId = "15";

            using (var assertContext = new CMContext(options))
            {
                var sut = new CocktailServices(assertContext, _fileUploadService.Object,
                    _ingredientServices.Object, _recipeServices.Object);
                var ex = await Assert.ThrowsExceptionAsync<MagicExeption>(
                  async () => await sut.GetCocktailNameById(cocktailId)
                  );
            }
        }

        [TestMethod]
        public async Task ThrowCorrectExceptionMessage_WhenCocktailDoesNotExist()
        {
            var options = TestUtils.GetOptions(nameof(ThrowCorrectExceptionMessage_WhenCocktailDoesNotExist));
            var cocktailId = "15";

            using (var assertContext = new CMContext(options))
            {
                var sut = new CocktailServices(assertContext, _fileUploadService.Object,
                    _ingredientServices.Object, _recipeServices.Object);
                var ex = await Assert.ThrowsExceptionAsync<MagicExeption>(
                  async () => await sut.GetCocktailNameById(cocktailId)
                  );
                Assert.AreEqual("Cocktail doesn't exist in DB!", ex.Message);
            }
        }

        [TestMethod]
        public async Task ThrowException_WhenPassedIdIsNull()
        {
            var options = TestUtils.GetOptions(nameof(ThrowException_WhenPassedIdIsNull));

            using (var assertContext = new CMContext(options))
            {
                var sut = new CocktailServices(assertContext, _fileUploadService.Object,
                    _ingredientServices.Object, _recipeServices.Object);
                var ex = await Assert.ThrowsExceptionAsync<MagicExeption>(
                  async () => await sut.GetCocktailNameById(null)
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
                var ex = await Assert.ThrowsExceptionAsync<MagicExeption>(
                  async () => await sut.GetCocktailNameById(null)
                  );
                Assert.AreEqual("ID cannot be null!", ex.Message);
            }
        }

        [TestMethod]
        public async Task ThrowNewException_WhenCocktailIsDeleted()
        {
            var options = TestUtils.GetOptions(nameof(ThrowNewException_WhenCocktailIsDeleted));
            var cocktailId = "15";
            var newCocktail = new Cocktail() { Id = cocktailId, DateDeleted= DateTime.Now };

            using (var arrangeContext = new CMContext(options))
            {
                arrangeContext.Add(newCocktail);
                await arrangeContext.SaveChangesAsync();
            }
            using (var assertContext = new CMContext(options))
            {
                var sut = new CocktailServices(assertContext, _fileUploadService.Object,
                    _ingredientServices.Object, _recipeServices.Object);
                var ex = await Assert.ThrowsExceptionAsync<MagicExeption>(
                  async () => await sut.GetCocktailNameById(cocktailId));
            }
        }

        [TestMethod]
        public async Task ThrowCorrectMessage_WhenCocktailDeleteDateIsNotNull()
        {
            var options = TestUtils.GetOptions(nameof(ThrowCorrectMessage_WhenCocktailDeleteDateIsNotNull));
            var cocktailId = "15";
            var newCocktail = new Cocktail() { Id = cocktailId, DateDeleted = DateTime.Now };

            using (var arrangeContext = new CMContext(options))
            {
                arrangeContext.Add(newCocktail);
                await arrangeContext.SaveChangesAsync();
            }
            using (var assertContext = new CMContext(options))
            {
                var sut = new CocktailServices(assertContext, _fileUploadService.Object,
                    _ingredientServices.Object, _recipeServices.Object);
                var ex = await Assert.ThrowsExceptionAsync<MagicExeption>(
                  async () => await sut.GetCocktailNameById(cocktailId));
                Assert.AreEqual("Cocktail doesn't exist in DB!", ex.Message);
            }
        }
    }
}
