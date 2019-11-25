using CM.Data;
using CM.DTOs;
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
    public class FindCocktailById_Should
    {
            Mock<IIngredientServices> _ingredientServices;
            Mock<IRecipeServices> _recipeServices;
            Mock<IFileUploadService> _fileUploadService;


            public FindCocktailById_Should()
            {
                _ingredientServices = new Mock<IIngredientServices>();
                _recipeServices = new Mock<IRecipeServices>();
                _fileUploadService = new Mock<IFileUploadService>();
            }

            [TestMethod]
            public async Task GetCorrectCocktail_WhenValidIdIsPassed()
            {
                var options = TestUtils.GetOptions(nameof(GetCorrectCocktail_WhenValidIdIsPassed));
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
                    var result = await sut.FindCocktailById(cocktailId);
                    Assert.AreEqual(cocktailId, result.Id);
                }
            }

            [TestMethod]
            public async Task ReturnObjectOfTheCorrectType_WhenValidIdIsPassed()
            {
                var options = TestUtils.GetOptions(nameof(ReturnObjectOfTheCorrectType_WhenValidIdIsPassed));
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
                    var result = await sut.FindCocktailById(cocktailId);
                    Assert.IsInstanceOfType(result, typeof(CocktailDto));
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
                      async () => await sut.FindCocktailById(cocktailId)
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
                      async () => await sut.FindCocktailById(cocktailId)
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
                      async () => await sut.FindCocktailById(null)
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
                      async () => await sut.FindCocktailById(null)
                      );
                    Assert.AreEqual("Id cannot be null!", ex.Message);
                }
            }

            [TestMethod]
            public async Task ThrowException_WhenCocktailIsDeleted()
            {
                var options = TestUtils.GetOptions(nameof(ThrowException_WhenCocktailIsDeleted));
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
                      async () => await sut.FindCocktailById(cocktailId));
                }
            }

            [TestMethod]
            public async Task ThrowCorrectMessage_WhenCocktailIsDeleted()
            {
                var options = TestUtils.GetOptions(nameof(ThrowCorrectMessage_WhenCocktailIsDeleted));
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
                      async () => await sut.FindCocktailById(cocktailId));
                    Assert.AreEqual("Cocktail doesn't exist in DB!", ex.Message);
                }
            }

            [TestMethod]
            public async Task CorrectlyLoadAllDependenciesForLoadedCocktail_WhenPassedIdIsCorrect()
            {
            var options = TestUtils.GetOptions(nameof(CorrectlyLoadAllDependenciesForLoadedCocktail_WhenPassedIdIsCorrect));
            var cocktailId = "15";
            var barId = "51";
            var barName = "Fofo";
            var cocktailComponentId = "15";
            var ingredientId = "15";
            var ingredientName = "Bira";
            var newIngredient = new Ingredient() { Id = ingredientId, Name = ingredientName };
            var newCocktailComponent = new CocktailComponent() { Id = cocktailComponentId, IngredientId = ingredientId, Ingredient = newIngredient };
            var listComponents = new List<CocktailComponent>();
            listComponents.Add(newCocktailComponent);
            var country = new Country() { Id = "1", Name = "Bulgaria" };
            var adress = new Address() { Id = "1", City = "Sofia", Country = country };
            var newBar = new Bar() { Id = barId, Name = barName, Address=adress };
            var newBarCocktail = new BarCocktail() { BarId = barId, Bar = newBar, CocktailId = cocktailId };
            var barCocktails = new List<BarCocktail>();
            barCocktails.Add(newBarCocktail);
            var newCocktail = new Cocktail()
            {
                Id = cocktailId,
                CocktailComponents = listComponents,
                BarCocktails = barCocktails
            };

            using (var arrangeContext = new CMContext(options))
            {
                arrangeContext.Add(newIngredient);
                arrangeContext.Add(country);
                arrangeContext.Add(adress);
                arrangeContext.Add(newBar);
                arrangeContext.Add(newBarCocktail);
                arrangeContext.Add(newCocktailComponent);
                arrangeContext.Add(newCocktail);
                await arrangeContext.SaveChangesAsync();
            }
            using (var assertContext = new CMContext(options))
            {
                var sut = new CocktailServices(assertContext, _fileUploadService.Object,
                    _ingredientServices.Object, _recipeServices.Object);
                var result = await sut.FindCocktailById(cocktailId);
                Assert.AreEqual(barId, result.BarCocktails[0].BarId);
                Assert.AreEqual(barName, result.BarCocktails[0].Bar.Name);
                Assert.AreEqual("Sofia", result.BarCocktails[0].Bar.Address.City);
                Assert.AreEqual("Bulgaria", result.BarCocktails[0].Bar.Address.Country.Name);
                Assert.AreEqual(ingredientName, result.Ingredients[0].Ingredient);
               
            }
        }
        }
}
