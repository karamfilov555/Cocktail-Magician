using CM.Data;
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
    public class Constructor_Should
    {

        Mock<IIngredientServices> _ingredientServices;
        Mock<IRecipeServices> _recipeServices;
        Mock<IFileUploadService> _fileUploadService;


        public Constructor_Should()
        {
            _ingredientServices = new Mock<IIngredientServices>();
            _recipeServices = new Mock<IRecipeServices>();
            _fileUploadService = new Mock<IFileUploadService>();
        }

        [TestMethod]
        public void ThrowMagicExeptionIfNullValue_DbContextPassed()
        {
            Assert.ThrowsException<MagicExeption>(
               () => new CocktailServices(null, _fileUploadService.Object,
                    _ingredientServices.Object, _recipeServices.Object));
        }

        [TestMethod]
        public void ThrowCorrectMessageIfNullValue_DbContextPassed()
        {
            var ex= Assert.ThrowsException<MagicExeption>(
               () => new CocktailServices(null, _fileUploadService.Object,
                    _ingredientServices.Object, _recipeServices.Object));
            Assert.AreEqual("CMContext cannot be null!", ex.Message);
        }

        [TestMethod]
        public void ThrowMagicExeptionIfNullValue_IFileUploadServicePassed()
        {
            var options = TestUtils.GetOptions(nameof(ThrowMagicExeptionIfNullValue_IFileUploadServicePassed));

            Assert.ThrowsException<MagicExeption>(
               () => new CocktailServices(new CMContext(options), null,
                    _ingredientServices.Object, _recipeServices.Object));

        }

        [TestMethod]
        public void ThrowCorrectMessageIfNullValue_IFileUploadServicePassed()
        {
            var options = TestUtils.GetOptions(nameof(ThrowCorrectMessageIfNullValue_IFileUploadServicePassed));

           var ex= Assert.ThrowsException<MagicExeption>(
               () => new CocktailServices(new CMContext(options), null,
                    _ingredientServices.Object, _recipeServices.Object));
            Assert.AreEqual("IFileUploadService cannot be null!", ex.Message);
        }

        [TestMethod]
        public void ThrowMagicExeptionIfNullValue_IIngredientServicePassed()
        {
            var options = TestUtils.GetOptions(nameof(ThrowMagicExeptionIfNullValue_IFileUploadServicePassed));

            Assert.ThrowsException<MagicExeption>(
               () => new CocktailServices(new CMContext(options), _fileUploadService.Object,
                    null, _recipeServices.Object));

        }

        [TestMethod]
        public void ThrowCorrectMessageIfNullValue_IIngredientServicePassed()
        {
            var options = TestUtils.GetOptions(nameof(ThrowCorrectMessageIfNullValue_IIngredientServicePassed));

            var ex=Assert.ThrowsException<MagicExeption>(
               () => new CocktailServices(new CMContext(options), _fileUploadService.Object,
                    null, _recipeServices.Object));
            Assert.AreEqual("IIngredientService cannot be null!", ex.Message);

        }

        [TestMethod]
        public void ThrowMagicExeptionIfNullValue_IRecipeServicePassed()
        {
            var options = TestUtils.GetOptions(nameof(ThrowMagicExeptionIfNullValue_IRecipeServicePassed));

            Assert.ThrowsException<MagicExeption>(
               () => new CocktailServices(new CMContext(options), _fileUploadService.Object,
                    _ingredientServices.Object, null));

        }

        [TestMethod]
        public void ThrowCorrectMessageIfNullValue_IRecipeServicePassed()
        {
            var options = TestUtils.GetOptions(nameof(ThrowCorrectMessageIfNullValue_IRecipeServicePassed));

            var ex=Assert.ThrowsException<MagicExeption>(
               () => new CocktailServices(new CMContext(options), _fileUploadService.Object,
                    _ingredientServices.Object, null));
            Assert.AreEqual("IRecipeService cannot be null!", ex.Message);

        }
        //[TestMethod]
        //public async Task ThrowCorrectMagicExeption_IfNullValue_IFileUploadServicePassed()
        //{
        //    var options = TestUtils.GetOptions(nameof(ThrowCorrectMagicExeption_IfNullValue_IFileUploadServicePassed));

        //    var ex = Assert.ThrowsException<MagicExeption>(
        //       () => new BarServices(new CMContext(options), null));
        //    Assert.AreEqual("IFileUploadServiceNull cannot be null!", ex.Message);
        //}
        //[TestMethod]
        //public async Task MakeInstance_OfTypeBarService_IfValidValuesPassed()
        //{
        //    var options = TestUtils.GetOptions(nameof(MakeInstance_OfTypeBarService_IfValidValuesPassed));

        //    var barService = new BarServices(new CMContext(options), new Mock<IFileUploadService>().Object);

        //    Assert.IsInstanceOfType(barService, typeof(BarServices));
        //}
    }
}

