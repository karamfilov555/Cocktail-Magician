using CM.Data;
using CM.DTOs;
using CM.Models;
using CM.Services.Contracts;
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
    public class GetCocktailsForHomePage_Should
    {
        Mock<IIngredientServices> _ingredientServices;
        Mock<IRecipeServices> _recipeServices;
        Mock<IFileUploadService> _fileUploadService;


        public GetCocktailsForHomePage_Should()
        {
            _ingredientServices = new Mock<IIngredientServices>();
            _recipeServices = new Mock<IRecipeServices>();
            _fileUploadService = new Mock<IFileUploadService>();
        }

        [TestMethod]
        public async Task GetMax5Cocktails_WhenCalled()
        {
            var options = TestUtils.GetOptions(nameof(GetMax5Cocktails_WhenCalled));
            using (var arrangeContext = new CMContext(options))
            {
                for (int i = 0; i < 10; i++)
                {
                    arrangeContext.Add(new Cocktail());
                };
                await arrangeContext.SaveChangesAsync();
            }
            using (var assertContext = new CMContext(options))
            {
                var sut = new CocktailServices(assertContext, _fileUploadService.Object,
                    _ingredientServices.Object, _recipeServices.Object);
                var result = await sut.GetCocktailsForHomePage();
                Assert.AreEqual(5, result.Count);
            }
        }

        [TestMethod]
        public async Task Get5CocktailsWithHighesRate_WhenCalled()
        {
            var options = TestUtils.GetOptions(nameof(Get5CocktailsWithHighesRate_WhenCalled));
            using (var arrangeContext = new CMContext(options))
            {
                for (int i = 0; i < 10; i++)
                {
                    arrangeContext.Add(new Cocktail() { Rating = i + 1 });
                };
                await arrangeContext.SaveChangesAsync();
            }
            using (var assertContext = new CMContext(options))
            {
                var sut = new CocktailServices(assertContext, _fileUploadService.Object,
                    _ingredientServices.Object, _recipeServices.Object);
                var result = await sut.GetCocktailsForHomePage();
                double rating = 10f;
                foreach (var item in result)
                {
                    Assert.AreEqual(rating, item.Rating);
                    rating--;
                }
            }
        }
        [TestMethod]
        public async Task ReturnCorrectType_WhenCalled()
        {
            var options = TestUtils.GetOptions(nameof(ReturnCorrectType_WhenCalled));
            using (var arrangeContext = new CMContext(options))
            {
                for (int i = 0; i < 5; i++)
                {
                    arrangeContext.Add(new Cocktail());
                };
                await arrangeContext.SaveChangesAsync();
            }
            using (var assertContext = new CMContext(options))
            {
                var sut = new CocktailServices(assertContext, _fileUploadService.Object,
                    _ingredientServices.Object, _recipeServices.Object);
                var result = await sut.GetCocktailsForHomePage();
                Assert.IsInstanceOfType(result, typeof(List<CocktailDto>) );
            }
        }
        [TestMethod]
        public async Task ExcludeDeletedCocktailsFromHomePage_WhenCalled()
        {
            var options = TestUtils.GetOptions(nameof(ExcludeDeletedCocktailsFromHomePage_WhenCalled));
            using (var arrangeContext = new CMContext(options))
            {
                for (int i = 0; i < 4; i++)
                {
                    arrangeContext.Add(new Cocktail());
                };
                arrangeContext.Add(new Cocktail() { DateDeleted=DateTime.Now});
                await arrangeContext.SaveChangesAsync();
            }
            using (var assertContext = new CMContext(options))
            {
                var sut = new CocktailServices(assertContext, _fileUploadService.Object,
                    _ingredientServices.Object, _recipeServices.Object);
                var result = await sut.GetCocktailsForHomePage();
                Assert.AreEqual(4, result.Count);
            }
        }

        [TestMethod]
        public async Task CorrectlyLoadAllDependenciesForLoadedCocktails_WhenCalledForHomePage()
        {
            var options = TestUtils.GetOptions(nameof(CorrectlyLoadAllDependenciesForLoadedCocktails_WhenCalledForHomePage));
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
            var newBar = new Bar() { Id = barId, Name=barName };
            var newBarCocktail = new BarCocktail() { BarId = barId, Bar=newBar, CocktailId = cocktailId };
            var barCocktails = new List<BarCocktail>();
            barCocktails.Add(newBarCocktail);
            var newCocktail = new Cocktail() { Id = cocktailId, CocktailComponents = listComponents,
            BarCocktails=barCocktails};

            using (var arrangeContext = new CMContext(options))
            {
                arrangeContext.Add(newIngredient);
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
                var result = await sut.GetCocktailsForHomePage();
                Assert.AreEqual(cocktailId, result.ToList()[0].Id);
                Assert.AreEqual(barId, result.ToList()[0].BarCocktails[0].BarId);
                Assert.AreEqual(barName, result.ToList()[0].BarCocktails[0].Bar.Name);
                Assert.AreEqual(ingredientName, result.ToList()[0].Ingredients[0].Ingredient);
            }
        }
    }
}
