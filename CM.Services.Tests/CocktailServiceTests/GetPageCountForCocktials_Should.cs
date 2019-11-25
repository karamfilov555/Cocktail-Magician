using CM.Data;
using CM.Models;
using CM.Services.Contracts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CM.Services.Tests.CocktailServiceTests
{
    [TestClass]
   public class GetPageCountForCocktials_Should
    {
        Mock<IIngredientServices> _ingredientServices;
        Mock<IRecipeServices> _recipeServices;
        Mock<IFileUploadService> _fileUploadService;


        public GetPageCountForCocktials_Should()
        {
            _ingredientServices = new Mock<IIngredientServices>();
            _recipeServices = new Mock<IRecipeServices>();
            _fileUploadService = new Mock<IFileUploadService>();
        }

        [TestMethod]
        public async Task GetCorrectNumerOfPages_WhenCalled()
        {
            var options = TestUtils.GetOptions(nameof(GetCorrectNumerOfPages_WhenCalled));
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
                var result = await sut.GetPageCountForCocktials(2);
                Assert.AreEqual(5, result);
            }
        }

        [TestMethod]
        public async Task ExcludeDeletedCocktails_WhenCalled()
        {
            var options = TestUtils.GetOptions(nameof(ExcludeDeletedCocktails_WhenCalled));
            using (var arrangeContext = new CMContext(options))
            {
                for (int i = 0; i < 4; i++)
                {
                    arrangeContext.Add(new Cocktail());
                };
                arrangeContext.Add(new Cocktail() { DateDeleted = DateTime.Now });
                arrangeContext.Add(new Cocktail() { DateDeleted = DateTime.Now });
                await arrangeContext.SaveChangesAsync();
            }
            using (var assertContext = new CMContext(options))
            {
                var sut = new CocktailServices(assertContext, _fileUploadService.Object,
                    _ingredientServices.Object, _recipeServices.Object);
                var result = await sut.GetPageCountForCocktials(2);
                Assert.AreEqual(2, result);
            }
            
        }


    }
}
