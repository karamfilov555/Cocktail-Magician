using CM.Data;
using CM.Models;
using CM.Services.Contracts;
using CM.Services.CustomExceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CM.Services.Tests.CocktailServiceTests
{
    [TestClass]
    public class GetCocktail_Should
    {
        Mock<IIngredientServices> _ingredientServices;
        Mock<IRecipeServices> _recipeServices;
        Mock<IFileUploadService> _fileUploadService;
       

        public GetCocktail_Should()
        {
            _ingredientServices = new Mock<IIngredientServices>();
            _recipeServices = new Mock<IRecipeServices>();
            _fileUploadService = new Mock<IFileUploadService>();
        }

        [TestMethod]
        public async Task GetCorrectCocktailEntity_WhenValidIdIsPassed()
        {
            var options = TestUtils.GetOptions(nameof(GetCorrectCocktailEntity_WhenValidIdIsPassed));
            var cocktailId = "15";
            var newCocktail = new Cocktail() { Id = cocktailId };

            using (var arrangeContext = new CMContext(options))
            {
                arrangeContext.Add(newCocktail);
                await arrangeContext.SaveChangesAsync();
            }
            using (var assertContext = new CMContext(options))
            {
                var sut = new CocktailServices(assertContext, _fileUploadService.Object,
                    _ingredientServices.Object, _recipeServices.Object);
                var result = await sut.GetCocktail(cocktailId);
                Assert.AreEqual(cocktailId, result.Id );
            }
        }

        [TestMethod]
        public async Task ReturnCorrectObject_WhenValidIdIsPassed()
        {
            var options = TestUtils.GetOptions(nameof(ReturnCorrectObject_WhenValidIdIsPassed));
            var cocktailId = "15";
            var newCocktail = new Cocktail() { Id = cocktailId };

            using (var arrangeContext = new CMContext(options))
            {
                arrangeContext.Add(newCocktail);
                await arrangeContext.SaveChangesAsync();
            }
            using (var assertContext = new CMContext(options))
            {
                var sut = new CocktailServices(assertContext, _fileUploadService.Object,
                    _ingredientServices.Object, _recipeServices.Object);
                var result = await sut.GetCocktail(cocktailId);
                Assert.IsInstanceOfType(result, typeof(Cocktail));
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
                var ex = await Assert.ThrowsExceptionAsync<MagicException>(
                  async () => await sut.GetCocktail(cocktailId)
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
                var ex = await Assert.ThrowsExceptionAsync<MagicException>(
                  async () => await sut.GetCocktail(cocktailId)
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
                var ex = await Assert.ThrowsExceptionAsync<MagicException>(
                  async () => await sut.GetCocktail(null)
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
                  async () => await sut.GetCocktail(null)
                  );
                Assert.AreEqual("ID cannot be null!", ex.Message);
            }
        }

        [TestMethod]
        public async Task ThrowMagicException_WhenCocktailIsDeleted()
        {
            var options = TestUtils.GetOptions(nameof(ThrowMagicException_WhenCocktailIsDeleted));
            var cocktailId = "15";
            var newCocktail = new Cocktail() { Id = cocktailId, DateDeleted=DateTime.Now };
            
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
                  async () => await sut.GetCocktail(cocktailId));
            }
        }

        [TestMethod]
        public async Task ThrowCorrectMagicMessage_WhenCocktailIsDeleted()
        {
            var options = TestUtils.GetOptions(nameof(ThrowCorrectMagicMessage_WhenCocktailIsDeleted));
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
                var ex = await Assert.ThrowsExceptionAsync<MagicException>(
                  async () => await sut.GetCocktail(cocktailId));
                Assert.AreEqual("Cocktail doesn't exist in DB!", ex.Message);
            }
        }

        [TestMethod]
        public async Task CorrectlyLoadDependenciesForLoadedCocktail_WhenPassedIdIsCorrect()
        {
            var options = TestUtils.GetOptions(nameof(CorrectlyLoadDependenciesForLoadedCocktail_WhenPassedIdIsCorrect));
            var cocktailId = "15";
            var cocktailComponentId = "15";
            var ingredientId = "15";
            var ingredientName = "Bira";
            var newIngredient = new Ingredient() { Id = ingredientId, Name = ingredientName };
            var newCocktailComponent = new CocktailComponent() { Id = cocktailComponentId, IngredientId = ingredientId, Ingredient=newIngredient };
            var listComponents = new List<CocktailComponent>();
            listComponents.Add(newCocktailComponent);
            var newCocktail = new Cocktail() { Id = cocktailId, CocktailComponents=listComponents};
            
            using (var arrangeContext = new CMContext(options))
            {
                arrangeContext.Add(newIngredient);
                arrangeContext.Add(newCocktailComponent);
                arrangeContext.Add(newCocktail);
                await arrangeContext.SaveChangesAsync();
            }
            using (var assertContext = new CMContext(options))
            {
                var sut = new CocktailServices(assertContext, _fileUploadService.Object,
                    _ingredientServices.Object, _recipeServices.Object);
                var result = await sut.GetCocktail(cocktailId);
                Assert.AreEqual(cocktailComponentId, result.CocktailComponents.ToList()[0].Id);
                Assert.AreEqual(ingredientId, result.CocktailComponents.ToList()[0].IngredientId);
                Assert.AreEqual(ingredientName, result.CocktailComponents.ToList()[0].Ingredient.Name);
            }
        }
    }
}
