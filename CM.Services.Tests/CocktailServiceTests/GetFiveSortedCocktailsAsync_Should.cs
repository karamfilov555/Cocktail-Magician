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
    public class GetFiveSortedCocktailsAsync_Should
    {
        Mock<IIngredientServices> _ingredientServices;
        Mock<IRecipeServices> _recipeServices;
        Mock<IFileUploadService> _fileUploadService;


        public GetFiveSortedCocktailsAsync_Should()
        {
            _ingredientServices = new Mock<IIngredientServices>();
            _recipeServices = new Mock<IRecipeServices>();
            _fileUploadService = new Mock<IFileUploadService>();
        }

        [TestMethod]
        public async Task GetMax5SortedCocktails_WhenCalled()
        {
            var options = TestUtils.GetOptions(nameof(GetMax5SortedCocktails_WhenCalled));
            var sortOrder = "name_desc";
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
                var result = await sut.GetFiveSortedCocktailsAsync(sortOrder);
                Assert.AreEqual(5, result.Count);
            }
        }

        [TestMethod]
        public async Task GetCorrectlySortedCocktails_WhenCalledWithNameDesc()
        {
            var options = TestUtils.GetOptions(nameof(GetCorrectlySortedCocktails_WhenCalledWithNameDesc));
            var sortOrder = "name_desc";
            using (var arrangeContext = new CMContext(options))
            {
                string name = "A";
                for (int i = 0; i < 5; i++)
                {
                    arrangeContext.Add(new Cocktail { Name=name});
                    name += i;
                };
                await arrangeContext.SaveChangesAsync();
            }
            using (var assertContext = new CMContext(options))
            {
                var sut = new CocktailServices(assertContext, _fileUploadService.Object,
                    _ingredientServices.Object, _recipeServices.Object);
                var result = await sut.GetFiveSortedCocktailsAsync(sortOrder);
                Assert.AreEqual("A0123", result.ToList()[0].Name);
                Assert.AreEqual("A", result.ToList()[4].Name);
            }
        }
        [TestMethod]
        public async Task GetCorrectlySortedCocktails_WhenCalledWithoutSortOrder()
        {
            var options = TestUtils.GetOptions(nameof(GetCorrectlySortedCocktails_WhenCalledWithoutSortOrder));
            
            using (var arrangeContext = new CMContext(options))
            {
                string name = "A";
                for (int i = 0; i < 5; i++)
                {
                    arrangeContext.Add(new Cocktail { Name = name });
                    name += i;
                };
                await arrangeContext.SaveChangesAsync();
            }
            using (var assertContext = new CMContext(options))
            {
                var sut = new CocktailServices(assertContext, _fileUploadService.Object,
                    _ingredientServices.Object, _recipeServices.Object);
                var result = await sut.GetFiveSortedCocktailsAsync(null);
                Assert.AreEqual("A", result.ToList()[0].Name);
                Assert.AreEqual("A0123", result.ToList()[4].Name);
            }
        }

        [TestMethod]
        public async Task GetCorrectlySortedCocktails_WhenCalledWitRating()
        {
            var options = TestUtils.GetOptions(nameof(GetCorrectlySortedCocktails_WhenCalledWitRating));
            var sortOrder = "rating";
            using (var arrangeContext = new CMContext(options))
            {
                double rating = 1;
                for (int i = 0; i < 5; i++)
                {
                    arrangeContext.Add(new Cocktail { Rating=rating });
                    rating++;
                };
                await arrangeContext.SaveChangesAsync();
            }
            using (var assertContext = new CMContext(options))
            {
                var sut = new CocktailServices(assertContext, _fileUploadService.Object,
                    _ingredientServices.Object, _recipeServices.Object);
                var result = await sut.GetFiveSortedCocktailsAsync(sortOrder);
                Assert.AreEqual(1, result.ToList()[0].Rating);
                Assert.AreEqual(5, result.ToList()[4].Rating);
            }
        }

        [TestMethod]
        public async Task GetCorrectlySortedCocktails_WhenCalledWitRatingDesc()
        {
            var options = TestUtils.GetOptions(nameof(GetCorrectlySortedCocktails_WhenCalledWitRatingDesc));
            var sortOrder = "rating_desc";
            using (var arrangeContext = new CMContext(options))
            {
                double rating = 1;
                for (int i = 0; i < 5; i++)
                {
                    arrangeContext.Add(new Cocktail { Rating = rating });
                    rating++;
                };
                await arrangeContext.SaveChangesAsync();
            }
            using (var assertContext = new CMContext(options))
            {
                var sut = new CocktailServices(assertContext, _fileUploadService.Object,
                    _ingredientServices.Object, _recipeServices.Object);
                var result = await sut.GetFiveSortedCocktailsAsync(sortOrder);
                Assert.AreEqual(5, result.ToList()[0].Rating);
                Assert.AreEqual(1, result.ToList()[4].Rating);
            }
        }
        [TestMethod]
        public async Task GetCorrectNumberofSortedCocktails_WhenCalledForLastPage()
        {
            var options = TestUtils.GetOptions(nameof(GetCorrectNumberofSortedCocktails_WhenCalledForLastPage));
            var sortOrder = "name_desc";
            using (var arrangeContext = new CMContext(options))
            {
                for (int i = 0; i < 8; i++)
                {
                    arrangeContext.Add(new Cocktail());
                };
                await arrangeContext.SaveChangesAsync();
            }
            using (var assertContext = new CMContext(options))
            {
                var sut = new CocktailServices(assertContext, _fileUploadService.Object,
                    _ingredientServices.Object, _recipeServices.Object);
                var result = await sut.GetFiveSortedCocktailsAsync(sortOrder, 2);
                Assert.AreEqual(3, result.Count);
            }
        }


        [TestMethod]
        public async Task ReturnCorrectTypeFor5Cocktails_WhenCalled()
        {
            var options = TestUtils.GetOptions(nameof(ReturnCorrectTypeFor5Cocktails_WhenCalled));
            var sortOrder = "name_desc";
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
                var result = await sut.GetFiveSortedCocktailsAsync(sortOrder);
                Assert.IsInstanceOfType(result, typeof(List<CocktailDto>));
            }
        }
        [TestMethod]
        public async Task ExcludeAllDeletedCocktailsFromResults_WhenCalled()
        {
            var options = TestUtils.GetOptions(nameof(ExcludeAllDeletedCocktailsFromResults_WhenCalled));
            var sortOrder = "name_desc";
            using (var arrangeContext = new CMContext(options))
            {
                for (int i = 0; i < 2; i++)
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
                var result = await sut.GetFiveSortedCocktailsAsync(sortOrder);
                Assert.AreEqual(2, result.Count);
            }
        }

        [TestMethod]
        public async Task CorrectlyLoadAllDependenciesForCocktails_WhenCalled()
        {
            var sortOrder = "name_desc";
            var user = new AppUser() { Id = "1", UserName = "pesho" };
            var review = new CocktailReview() { Id = "11", User = user, Rating = 10f };
            var reviews = new List<CocktailReview>();
            reviews.Add(review);
            var options = TestUtils.GetOptions(nameof(CorrectlyLoadAllDependenciesForCocktails_WhenCalled));
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
                Reviews = reviews

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
                var result = await sut.GetFiveSortedCocktailsAsync(sortOrder);
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

