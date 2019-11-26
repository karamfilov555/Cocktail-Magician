using CM.Data;
using CM.DTOs;
using CM.Models;
using CM.Services.Contracts;
using CM.Services.CustomExceptions;
using Microsoft.AspNetCore.Http;
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
    public class AddCocktail_Should
    {
        Mock<IIngredientServices> _ingredientServices;
        Mock<IRecipeServices> _recipeServices;
        Mock<IFileUploadService> _fileUploadService;


        public AddCocktail_Should()
        {
            _ingredientServices = new Mock<IIngredientServices>();
            _recipeServices = new Mock<IRecipeServices>();
            _fileUploadService = new Mock<IFileUploadService>();
        }

        [TestMethod]
        public async Task AddCorrectCocktailToDB_WhenValidModelIsPassed()
        {
            var cocktailName = "Mojito";
            var image = new Mock<IFormFile>().Object;
            var options = TestUtils.GetOptions(nameof(AddCorrectCocktailToDB_WhenValidModelIsPassed));

            using (var arrangeContext = new CMContext(options))
            {
                var sut = new CocktailServices(arrangeContext, _fileUploadService.Object,
                    _ingredientServices.Object, _recipeServices.Object);
                await sut.AddCocktail(
                    new CocktailDto
                    {
                        Name = cocktailName,
                        CocktailImage = image
                    });
                Assert.AreEqual(cocktailName, arrangeContext.Cocktails.First().Name);
            }
        }

        [TestMethod]
        public async Task AddAllDependentEntities_WhenValidModelIsPassed()
        {
            var cocktailName = "Mojito";
            var image = new Mock<IFormFile>().Object;
            var recipe = "111";
            var options = TestUtils.GetOptions(nameof(AddAllDependentEntities_WhenValidModelIsPassed));
            var cocktailDTO = new CocktailDto()
            {
                Name = cocktailName,
                CocktailImage = image,
                Recipe = recipe,
                Ingredients = new List<CocktailComponentDTO>
                        {
                            new CocktailComponentDTO
                            {
                                IngredientId = "1",
                        Quantity = 200,
                        Unit = Unit.ml,
                        Ingredient = "Vodka"

                            },
                            new CocktailComponentDTO
                            {
                                IngredientId = "2",
                        Quantity = 300,
                        Unit = Unit.ml,
                        Ingredient = "Vodka2"

                            }
                        }
            };
            using (var arrangeContext = new CMContext(options))
            {
                var sut = new CocktailServices(arrangeContext, _fileUploadService.Object,
                    _ingredientServices.Object, _recipeServices.Object);
                await sut.AddCocktail(cocktailDTO);
                Assert.AreEqual(cocktailName, arrangeContext.Cocktails.First().Name);
                Assert.AreEqual(2, arrangeContext.Cocktails.First().CocktailComponents.Count);

            }
        }

        [TestMethod]
        public async Task AddTheCocktailToDB_WhenValidModelIsPassed()
        {
            var cocktailName = "Mojito";
            var image = new Mock<IFormFile>().Object;
            var options = TestUtils.GetOptions(nameof(AddTheCocktailToDB_WhenValidModelIsPassed));

            using (var assertContext = new CMContext(options))
            {
                var sut = new CocktailServices(assertContext, _fileUploadService.Object,
                    _ingredientServices.Object, _recipeServices.Object);
                await sut.AddCocktail(
                    new CocktailDto
                    {
                        Name = cocktailName,
                        CocktailImage = image
                    });
                Assert.AreEqual(1, assertContext.Cocktails.Count());
                
            }
        }


        [TestMethod]
        public async Task CallUploadFileMethod_WithCorrectParameters()
        {
            var options = TestUtils.GetOptions(nameof(CallUploadFileMethod_WithCorrectParameters));
            var cocktailName = "Mojito";
            var recipe = "111";
            var image = new Mock<IFormFile>().Object;
            var cocktailDTO = new CocktailDto() { Name = cocktailName, Recipe = recipe, CocktailImage = image };


            using (var assertContext = new CMContext(options))
            {
                var sut = new CocktailServices(assertContext, _fileUploadService.Object,
                    _ingredientServices.Object, _recipeServices.Object);
                await sut.AddCocktail(cocktailDTO);
                _fileUploadService.Verify(n => n.UploadFile(image), Times.Once());
            } 
        }

        [TestMethod]
        public async Task CallExtractRecipeMethod_WithCorrectParameters()
        {
            var options = TestUtils.GetOptions(nameof(CallExtractRecipeMethod_WithCorrectParameters));
            var cocktailName = "Mojito";
            var recipe = "111";
            var image = new Mock<IFormFile>().Object;
            var cocktailDTO = new CocktailDto() { Name = cocktailName, Recipe = recipe, CocktailImage = image };


            using (var assertContext = new CMContext(options))
            {
                var sut = new CocktailServices(assertContext, _fileUploadService.Object,
                    _ingredientServices.Object, _recipeServices.Object);
                await sut.AddCocktail(cocktailDTO);
                var cocktail = assertContext.Cocktails.First();
                _recipeServices.Verify(n => n.ExtractRecipe(cocktail), Times.Once());
            }
        }

        [TestMethod]
        public async Task EditImagePath_WhenNewImageIsPassed()
        {
            var cocktailName = "Mojito";
            var image = new Mock<IFormFile>().Object;
            var cocktailDtoId = "15";
            var options = TestUtils.GetOptions(nameof(EditImagePath_WhenNewImageIsPassed));
            var newCocktailDto = new CocktailDto
            {
                Name = cocktailName,
                CocktailImage = image
            };

            using (var arrangeContext = new CMContext(options))
            {
                var sut = new CocktailServices(arrangeContext, _fileUploadService.Object,
                    _ingredientServices.Object, _recipeServices.Object);

                arrangeContext.Add(new Cocktail {  Name = "NotMojito", Image = null });
                await arrangeContext.SaveChangesAsync();
                await sut.AddCocktail(newCocktailDto);
                Assert.AreNotEqual(cocktailDtoId, arrangeContext.Cocktails.First().Image);
            }
        }

        [TestMethod]
        public async Task PreserveImagePath_WhenNewImageIsNull()
        {
            var cocktailName = "Mojito";
           
            var options = TestUtils.GetOptions(nameof(PreserveImagePath_WhenNewImageIsNull));
            var newCocktailDto = new CocktailDto
            {
               
                Name = cocktailName,
                CocktailImage = null,
                Image = "555"
            };

            using (var arrangeContext = new CMContext(options))
            {
                var sut = new CocktailServices(arrangeContext, _fileUploadService.Object,
                    _ingredientServices.Object, _recipeServices.Object);

                arrangeContext.Add(new Cocktail { Name = "NotMojito", Image = "111" });
                await arrangeContext.SaveChangesAsync();
                await sut.AddCocktail(newCocktailDto);
                Assert.AreNotEqual("555", arrangeContext.Cocktails.First().Image);
            }
        }


    }
}

