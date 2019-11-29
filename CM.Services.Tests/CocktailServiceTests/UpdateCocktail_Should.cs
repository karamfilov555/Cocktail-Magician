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
    public class UpdateCocktail_Should
    {
        Mock<IIngredientServices> _ingredientServices;
        Mock<IRecipeServices> _recipeServices;
        Mock<IFileUploadService> _fileUploadService;

        public UpdateCocktail_Should()
        {
            _ingredientServices = new Mock<IIngredientServices>();
            _recipeServices = new Mock<IRecipeServices>();
            _fileUploadService = new Mock<IFileUploadService>();
        }

        [TestMethod]
        public async Task ThrowException_WhenPassedModelIsNull()
        {
            var options = TestUtils.GetOptions(nameof(ThrowException_WhenPassedModelIsNull));

            using (var assertContext = new CMContext(options))
            {
                var sut = new CocktailServices(assertContext, _fileUploadService.Object,
                    _ingredientServices.Object, _recipeServices.Object);
                var ex = await Assert.ThrowsExceptionAsync<MagicException>(
                  async () => await sut.Update(null)
                  );
            }
        }

        [TestMethod]
        public async Task ThrowCorrectMessage_WhenPassedModelIsNull()
        {
            var options = TestUtils.GetOptions(nameof(ThrowCorrectMessage_WhenPassedModelIsNull));

            using (var assertContext = new CMContext(options))
            {
                var sut = new CocktailServices(assertContext, _fileUploadService.Object,
                    _ingredientServices.Object, _recipeServices.Object);
                var ex = await Assert.ThrowsExceptionAsync<MagicException>(
                  async () => await sut.Update(null)
                  );
                Assert.AreEqual("Model is invalid!", ex.Message);
            }
        }

        [TestMethod]
        public async Task EditCorrectCocktailInDB_WhenValidModelIsPassed()
        {
            var cocktailName = "Mojito";
            var image = new Mock<IFormFile>().Object;
            var cocktailDtoId = "15";
            var options = TestUtils.GetOptions(nameof(EditCorrectCocktailInDB_WhenValidModelIsPassed));
            var newCocktailDto = new CocktailDto
            {
                Id = cocktailDtoId,
                Name = cocktailName,
                CocktailImage = image
            };

            using (var arrangeContext = new CMContext(options))
            {
                var sut = new CocktailServices(arrangeContext, _fileUploadService.Object,
                    _ingredientServices.Object, _recipeServices.Object);

                arrangeContext.Add(new Cocktail { Id = cocktailDtoId, Name = "NotMojito", Image = null });
                await arrangeContext.SaveChangesAsync();
                await sut.Update(newCocktailDto);
                Assert.AreEqual(cocktailDtoId, arrangeContext.Cocktails.First().Id);
                Assert.AreEqual(cocktailName, arrangeContext.Cocktails.First().Name);
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
                Id = cocktailDtoId,
                Name = cocktailName,
                CocktailImage = image
            };

            using (var arrangeContext = new CMContext(options))
            {
                var sut = new CocktailServices(arrangeContext, _fileUploadService.Object,
                    _ingredientServices.Object, _recipeServices.Object);

                arrangeContext.Add(new Cocktail { Id = cocktailDtoId, Name = "NotMojito", Image = null });
                await arrangeContext.SaveChangesAsync();
                await sut.Update(newCocktailDto);
                Assert.AreNotEqual(cocktailDtoId, arrangeContext.Cocktails.First().Image);
            }
        }

        [TestMethod]
        public async Task PreserveImagePath_WhenNewImageIsNullInEdit()
        {
            var cocktailName = "Mojito";
            var cocktailDtoId = "15";
            var options = TestUtils.GetOptions(nameof(PreserveImagePath_WhenNewImageIsNullInEdit));
            var newCocktailDto = new CocktailDto
            {
                Id = cocktailDtoId,
                Name = cocktailName,
                CocktailImage = null,
                Image = "555"
            };

            using (var arrangeContext = new CMContext(options))
            {
                var sut = new CocktailServices(arrangeContext, _fileUploadService.Object,
                    _ingredientServices.Object, _recipeServices.Object);

                arrangeContext.Add(new Cocktail { Id = cocktailDtoId, Name = "NotMojito", Image = "111" });
                await arrangeContext.SaveChangesAsync();
                await sut.Update(newCocktailDto);
                Assert.AreNotEqual("555", arrangeContext.Cocktails.First().Image);
            }
        }

        [TestMethod]
        public async Task PreservRating_WhenCalled()
        {
            var cocktailName = "Mojito";
            var cocktailDtoId = "15";
            var options = TestUtils.GetOptions(nameof(PreservRating_WhenCalled));
            var newCocktailDto = new CocktailDto
            {
                Id = cocktailDtoId,
                Name = cocktailName,
                CocktailImage = null,
                Image = "555",
                Rating = 7.00
            };

            using (var arrangeContext = new CMContext(options))
            {
                var sut = new CocktailServices(arrangeContext, _fileUploadService.Object,
                    _ingredientServices.Object, _recipeServices.Object);

                arrangeContext.Add(new Cocktail
                {
                    Id = cocktailDtoId,
                    Name = "NotMojito"
                    ,
                    Image = "111",
                    Rating = 10.00
                });
                await arrangeContext.SaveChangesAsync();
                await sut.Update(newCocktailDto);
                Assert.AreEqual(10, arrangeContext.Cocktails.First().Rating);
            }
        }

        [TestMethod]
        public async Task AddAllDependentEntities_WhenValidModelIsPassed_OnUpdate()
        {
            var cocktailName = "Mojito";
            var image = new Mock<IFormFile>().Object;
            var recipe = "111";
            var cocktailDtoId = "15";
            var options = TestUtils.GetOptions(nameof(AddAllDependentEntities_WhenValidModelIsPassed_OnUpdate));
            var cocktailDTO = new CocktailDto()
            {
                Id = cocktailDtoId,
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
                arrangeContext.Add(new Cocktail
                {
                    Id = cocktailDtoId,
                    Name = "NotMojito",
                    Image = "111",
                    Rating = 10.00,
                    CocktailComponents = new List<CocktailComponent>
                        {
                            new CocktailComponent
                            {
                                IngredientId = "6",
                                Ingredient=new Ingredient
                                {
                                    Id="6",
                                    Name="Pepsi"
                                },
                        Quantity = 200,
                        Unit = Unit.ml,
                            }
                        }
                });
                await arrangeContext.SaveChangesAsync();
                await sut.Update(cocktailDTO);
                Assert.AreEqual("Vodka", arrangeContext.Cocktails.First().CocktailComponents.ToList()[0].Ingredient.Name);
                Assert.AreEqual("Vodka2", arrangeContext.Cocktails.First().CocktailComponents.ToList()[1].Ingredient.Name);
                Assert.AreEqual(2, arrangeContext.Cocktails.First().CocktailComponents.Count);

            }
        }




        [TestMethod]
        public async Task CallUploadFileMethod_WithCorrectParametersInEdit()
        {
            var options = TestUtils.GetOptions(nameof(CallUploadFileMethod_WithCorrectParametersInEdit));
            var cocktailName = "Mojito";
            var recipe = "111";
            var cocktailDtoId = "15";
            var image = new Mock<IFormFile>().Object;
            var cocktailDTO = new CocktailDto() { Id = cocktailDtoId, Name = cocktailName, Recipe = recipe, CocktailImage = image };


            using (var assertContext = new CMContext(options))
            {
                var sut = new CocktailServices(assertContext, _fileUploadService.Object,
                    _ingredientServices.Object, _recipeServices.Object);
                assertContext.Add(new Cocktail
                {
                    Id = cocktailDtoId,
                    Name = "NotMojito",
                    Image = "111",
                    Rating = 10.00
                });
                await assertContext.SaveChangesAsync();
                await sut.Update(cocktailDTO);
                _fileUploadService.Verify(n => n.SetUniqueImagePathForCocktail(image), Times.Once());
            }
        }

        [TestMethod]
        public async Task CallExtractRecipeMethod_WithCorrectParametersInEdit()
        {
            var options = TestUtils.GetOptions(nameof(CallExtractRecipeMethod_WithCorrectParametersInEdit));
            var cocktailDtoId = "15";
            var cocktailName = "Mojito";
            var recipe = "111";
            var image = new Mock<IFormFile>().Object;
            var cocktailDTO = new CocktailDto() { Id = cocktailDtoId, Name = cocktailName, Recipe = recipe, CocktailImage = image };

            using (var assertContext = new CMContext(options))
            {
                assertContext.Add(new Cocktail
                {
                    Id = cocktailDtoId,
                    Name = "NotMojito",
                    Image = "111",
                    Rating = 10.00
                });
                await assertContext.SaveChangesAsync();
                var sut = new CocktailServices(assertContext, _fileUploadService.Object,
                    _ingredientServices.Object, _recipeServices.Object);
                await sut.Update(cocktailDTO);
                _recipeServices.Verify(n => n.ExtractRecipe(assertContext.Cocktails.First()), Times.Once());
            }
        }
    }
}
