using CM.Data;
using CM.Services.Contracts;
using CM.Services.CustomExeptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;

namespace CM.Services.Tests.NotificationServiceTests
{
    [TestClass]
    public class ConstructorShould
    {
        Mock<IAppUserServices> _userServices;
        Mock<INotificationManager> _iNotificationManager;
        public ConstructorShould()
        {
            _userServices = new Mock<IAppUserServices>();
            _iNotificationManager = new Mock<INotificationManager>();
        }

        [TestMethod]
        public void ThrowMagicExeptionIfNullValueInConstr_DbContextPassed()
        {
            Assert.ThrowsException<MagicExeption>(
               () => new NotificationServices(null, _userServices.Object,
                   _iNotificationManager.Object)); 
        }

        [TestMethod]
        public void ThrowCorrectMessageIfNullValueInConstr_DbContextPassed()
        {
            var ex = Assert.ThrowsException<MagicExeption>(
               () => new NotificationServices(null, _userServices.Object,
                   _iNotificationManager.Object));
            Assert.AreEqual(ExeptionMessages.ContextNull, ex.Message);
        }

        [TestMethod]
        public void ThrowMagicExeptionIfNullValueInConstr_IAppUserServices()
        {
            var options = TestUtils.GetOptions(nameof(ThrowMagicExeptionIfNullValueInConstr_IAppUserServices));

            Assert.ThrowsException<MagicExeption>(
              () => new NotificationServices(new CMContext(options), null,
                  _iNotificationManager.Object));

        }

        [TestMethod]
        public void ThrowCorrectMessageValueInConstr_IAppUserServices()
        {
            var options = TestUtils.GetOptions(nameof(ThrowCorrectMessageValueInConstr_IAppUserServices));

            var ex = Assert.ThrowsException<MagicExeption>(
              () => new NotificationServices(new CMContext(options), null,
                  _iNotificationManager.Object));
            Assert.AreEqual(ExeptionMessages.IAppUserServiceNull, ex.Message);
        }

        [TestMethod]
        public void ThrowMagicExeptionIfNullValueInConstr_INotificationManager()
        {
            var options = TestUtils.GetOptions(nameof(ThrowMagicExeptionIfNullValueInConstr_INotificationManager));

            Assert.ThrowsException<MagicExeption>(
              () => new NotificationServices(new CMContext(options), _userServices.Object,
                  null));

        }

        [TestMethod]
        public void ThrowCorrectMessageValueInConstr_INotificationManager()
        {
            var options = TestUtils.GetOptions(nameof(ThrowCorrectMessageValueInConstr_INotificationManager));

            var ex = Assert.ThrowsException<MagicExeption>(
              () => new NotificationServices(new CMContext(options), _userServices.Object,
                  null));
            Assert.AreEqual(ExeptionMessages.INotificationManagerNull, ex.Message);
        }
    }
}
