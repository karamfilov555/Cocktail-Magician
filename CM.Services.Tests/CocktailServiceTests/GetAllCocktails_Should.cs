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
    public class GetAllCocktails_Should
    {
        Mock<IIngredientServices> _ingredientServices;
        Mock<IRecipeServices> _recipeServices;
        Mock<IFileUploadService> _fileUploadService;


        public GetAllCocktails_Should()
        {
            _ingredientServices = new Mock<IIngredientServices>();
            _recipeServices = new Mock<IRecipeServices>();
            _fileUploadService = new Mock<IFileUploadService>();
        }

        [TestMethod]
        public async Task GetAllCocktails_WhenCalled()
        {
            var options = TestUtils.GetOptions(nameof(GetAllCocktails_WhenCalled));
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
                var result = await sut.GetAllCocktails();
                Assert.AreEqual(10, result.Count);
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
                var result = await sut.GetAllCocktails();
                Assert.IsInstanceOfType(result, typeof(List<CocktailDto>));
            }
        }
        [TestMethod]
        public async Task ExcludeDeletedCocktailsFromResults_WhenCalled()
        {
            var options = TestUtils.GetOptions(nameof(ExcludeDeletedCocktailsFromResults_WhenCalled));
            using (var arrangeContext = new CMContext(options))
            {
                for (int i = 0; i < 4; i++)
                {
                    arrangeContext.Add(new Cocktail());
                };
                arrangeContext.Add(new Cocktail() { DateDeleted = DateTime.Now });
                await arrangeContext.SaveChangesAsync();
            }
            using (var assertContext = new CMContext(options))
            {
                var sut = new CocktailServices(assertContext, _fileUploadService.Object,
                    _ingredientServices.Object, _recipeServices.Object);
                var result = await sut.GetAllCocktails();
                Assert.AreEqual(4, result.Count);
            }
        }

        [TestMethod]
        public async Task CorrectlyLoadAllDependenciesForLoadedCocktails_WhenCalled()
        {
            var user = new AppUser() { Id="1", UserName="pesho"};
            var review = new CocktailReview() { Id = "11", User = user, Rating = 10f };
            var reviews = new List<CocktailReview>();
            reviews.Add(review);
            var options = TestUtils.GetOptions(nameof(CorrectlyLoadAllDependenciesForLoadedCocktails_WhenCalled));
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
            var newBar = new Bar() { Id = barId, Name = barName };
            var newBarCocktail = new BarCocktail() { BarId = barId, Bar = newBar, CocktailId = cocktailId };
            var barCocktails = new List<BarCocktail>();
            barCocktails.Add(newBarCocktail);
            var newCocktail = new Cocktail()
            {
                Id = cocktailId,
                CocktailComponents = listComponents,
                BarCocktails = barCocktails,
                Reviews=reviews
                
            };

            using (var arrangeContext = new CMContext(options))
            {
                arrangeContext.Add(user);
                arrangeContext.Add(review);
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
                var result = await sut.GetAllCocktails();
                Assert.AreEqual(cocktailId, result.ToList()[0].Id);
                Assert.AreEqual(barId, result.ToList()[0].BarCocktails[0].BarId);
                Assert.AreEqual(barName, result.ToList()[0].BarCocktails[0].Bar.Name);
                Assert.AreEqual(ingredientName, result.ToList()[0].Ingredients[0].Ingredient);
                Assert.AreEqual("pesho", result.ToList()[0].CocktailReviews.ToList()[0].User.UserName);
                Assert.AreEqual(10, result.ToList()[0].CocktailReviews.ToList()[0].Rating);
                
            }
        }
    }
}
