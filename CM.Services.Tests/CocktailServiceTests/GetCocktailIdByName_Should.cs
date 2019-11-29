using CM.Data;
using CM.Models;
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
    public class GetCocktailIdByName_Should
    {
        Mock<IIngredientServices> _ingredientServices;
        Mock<IRecipeServices> _recipeServices;
        Mock<IFileUploadService> _fileUploadService;


        public GetCocktailIdByName_Should()
        {
            _ingredientServices = new Mock<IIngredientServices>();
            _recipeServices = new Mock<IRecipeServices>();
            _fileUploadService = new Mock<IFileUploadService>();
        }

        [TestMethod]
        public async Task GetCorrectCocktail_WhenValidNameIsPassed()
        {
            var options = TestUtils.GetOptions(nameof(GetCorrectCocktail_WhenValidNameIsPassed));
            var cocktailId = "15";
            var cocktailName = "Mohito";
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
                var result = await sut.GetCocktailIdByName(cocktailName);
                Assert.AreEqual(cocktailId, result);
            }
        }


        [TestMethod]
        public async Task ThrowException_WhenCocktailDoesNotExist()
        {
            var options = TestUtils.GetOptions(nameof(ThrowException_WhenCocktailDoesNotExist));
            var cocktailName = "Mohito";

            using (var assertContext = new CMContext(options))
            {
                var sut = new CocktailServices(assertContext, _fileUploadService.Object,
                    _ingredientServices.Object, _recipeServices.Object);
                var ex = await Assert.ThrowsExceptionAsync<MagicException>(
                  async () => await sut.GetCocktailIdByName(cocktailName));
            }
        }

        [TestMethod]
        public async Task ThrowCorrectExceptionMessage_WhenCocktailDoesNotExist()
        {
            var options = TestUtils.GetOptions(nameof(ThrowException_WhenCocktailDoesNotExist));
            var cocktailName = "Mohito";

            using (var assertContext = new CMContext(options))
            {
                var sut = new CocktailServices(assertContext, _fileUploadService.Object,
                    _ingredientServices.Object, _recipeServices.Object);
                var ex = await Assert.ThrowsExceptionAsync<MagicException>(
                  async () => await sut.GetCocktailIdByName(cocktailName));
                Assert.AreEqual("Cocktail doesn't exist in DB!", ex.Message);
            }
        }

        [TestMethod]
        public async Task ThrowException_WhenPassedStringIsNull()
        {
            var options = TestUtils.GetOptions(nameof(ThrowException_WhenPassedStringIsNull));

            using (var assertContext = new CMContext(options))
            {
                var sut = new CocktailServices(assertContext, _fileUploadService.Object,
                    _ingredientServices.Object, _recipeServices.Object);
                var ex = await Assert.ThrowsExceptionAsync<MagicException>(
                  async () => await sut.GetCocktailIdByName(null)
                  );
            }
        }

        [TestMethod]
        public async Task ThrowCorrectMessage_WhenPassedStringIsNull()
        {
            var options = TestUtils.GetOptions(nameof(ThrowCorrectMessage_WhenPassedStringIsNull));

            using (var assertContext = new CMContext(options))
            {
                var sut = new CocktailServices(assertContext, _fileUploadService.Object,
                    _ingredientServices.Object, _recipeServices.Object);
                var ex = await Assert.ThrowsExceptionAsync<MagicException>(
                  async () => await sut.GetCocktailIdByName(null)
                  );
                Assert.AreEqual("Name cannot be null!", ex.Message);
            }
        }

        [TestMethod]
        public async Task ThrowException_WhenTheCocktailIsDeleted()
        {
            var options = TestUtils.GetOptions(nameof(ThrowException_WhenTheCocktailIsDeleted));
            var cocktailId = "15";
            var cocktailName = "Mohito";
            var newCocktail = new Cocktail() { Id = cocktailId, Name = cocktailName, DateDeleted = DateTime.Now };

            using (var arrangeContext = new CMContext(options))
            {
                arrangeContext.Add(newCocktail);
                await arrangeContext.SaveChangesAsync();
            }
            using (var assertContext = new CMContext(options))
            {
                var sut = new CocktailServices(assertContext, _fileUploadService.Object,
                    _ingredientServices.Object, _recipeServices.Object);
                var ex = await Assert.ThrowsExceptionAsync<MagicException>(
                  async () => await sut.GetCocktailIdByName(cocktailName));
            }
        }

        [TestMethod]
        public async Task ThrowCorrectMessage_WhenTheCocktailIsDeleted()
        {
            var options = TestUtils.GetOptions(nameof(ThrowCorrectMessage_WhenTheCocktailIsDeleted));
            var cocktailId = "15";
            var cocktailName = "Mohito";
            var newCocktail = new Cocktail() { Id = cocktailId, Name = cocktailName, DateDeleted = DateTime.Now };

            using (var arrangeContext = new CMContext(options))
            {
                arrangeContext.Add(newCocktail);
                await arrangeContext.SaveChangesAsync();
            }
            using (var assertContext = new CMContext(options))
            {
                var sut = new CocktailServices(assertContext, _fileUploadService.Object,
                    _ingredientServices.Object, _recipeServices.Object);
                var ex = await Assert.ThrowsExceptionAsync<MagicException>(
                  async () => await sut.GetCocktailIdByName(cocktailName));
                Assert.AreEqual("Cocktail doesn't exist in DB!", ex.Message);
            }
        }
    }
}

