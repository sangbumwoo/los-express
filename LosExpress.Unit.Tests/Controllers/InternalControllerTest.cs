using System.Diagnostics.CodeAnalysis;
using System.Net;
using LosExpress.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LosExpress.Unit.Tests.Controllers
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class InternalControllerTest
    {
        private InternalController _internalController;

        [TestInitialize]
        public void Setup()
        {
            _internalController = new InternalController();
        }

        [TestMethod]
        [Description("Should return HTTP Status 501 NotImplemented when called")]
        public void DeleteUserData_NotImplemented()
        {
            // Arrange
            // Act
            var actionResult = _internalController.DeleteUserData("test") as StatusCodeResult;

            // Assert
            Assert.IsNotNull(actionResult);
            Assert.AreEqual((int)HttpStatusCode.NotImplemented, actionResult.StatusCode);
        }
    }
}
