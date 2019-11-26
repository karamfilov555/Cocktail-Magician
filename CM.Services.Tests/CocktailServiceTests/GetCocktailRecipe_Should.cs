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
    public class GetCocktailRecipe_Should
    {
        Mock<IIngredientServices> _ingredientServices;
        Mock<IRecipeServices> _recipeServices;
        Mock<IFileUploadService> _fileUploadService;


        public GetCocktailRecipe_Should()
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
                  async () => await sut.GetCocktailRecipe(null)
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
                  async () => await sut.GetCocktailRecipe(null)
                  );
                Assert.AreEqual("ID cannot be null!", ex.Message);
            }
        }

        [TestMethod]
        public async Task SetRecipe_WhenIdIsValid()
        {
            var options = TestUtils.GetOptions(nameof(SetRecipe_WhenIdIsValid));
            var cocktailName = "Mojito";
            var id = "111";
            
            using (var assertContext = new CMContext(options))
            {
                var sut = new CocktailServices(assertContext, _fileUploadService.Object,
                    _ingredientServices.Object, _recipeServices.Object);
                assertContext.Add(new Cocktail { Id = id, Name = cocktailName, Recepie=null });
                await assertContext.SaveChangesAsync();
                await sut.GetCocktailRecipe(id);
                _recipeServices.Verify(r => r.ExtractRecipe(assertContext.Cocktails.First()), Times.Once());
            }
        }

        [TestMethod]
        public async Task ReturnRecipeWhenExists_WhenIdIsValid()
        {
            var options = TestUtils.GetOptions(nameof(ReturnRecipeWhenExists_WhenIdIsValid));
            var cocktailName = "Mojito";
            var id = "111";
            var recipe = "11111";

            using (var assertContext = new CMContext(options))
            {
                var sut = new CocktailServices(assertContext, _fileUploadService.Object,
                    _ingredientServices.Object, _recipeServices.Object);
                assertContext.Add(new Cocktail { Id = id, Name = cocktailName, Recepie = recipe });
                await assertContext.SaveChangesAsync();
                var result=await sut.GetCocktailRecipe(id);
                Assert.AreEqual(recipe, result);
            }
        }
    }
}
