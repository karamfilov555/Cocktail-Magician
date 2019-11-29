using CM.Data;
using CM.Services.CustomExceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CM.Services.Tests.ReviewServicesTests
{

    [TestClass]
    public class Constructor_Should
    {
        [TestMethod]
        public void ThrowMagicExeption_IfNullValue_DbContext_Passed()
        {
            var fileService = new Mock<IFileUploadService>();
            Assert.ThrowsException<MagicException>(
               () => new ReviewServices(null));

        }
        [TestMethod]
        public void ThrowCorrectMagicExeption_IfNullValue_DbContext_Passed()
        {
            var ex = Assert.ThrowsException<MagicException>(
               () => new ReviewServices(null));
            Assert.AreEqual("CMContext cannot be null!", ex.Message);
        }
        [TestMethod]
        public void MakeInstance_OfReviewServices_IfValidDbContext_Passed()
        {
            var options = TestUtils.GetOptions(nameof(MakeInstance_OfReviewServices_IfValidDbContext_Passed));
            var result =  new ReviewServices(new CMContext(options));
            Assert.IsInstanceOfType(result,typeof(ReviewServices));
        }
    }
}
