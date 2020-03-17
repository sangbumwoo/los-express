using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Net;
using System.Threading.Tasks;
using LosExpress.Controllers.v2;
using LosExpress.Models;
using LosExpress.Services;
using Btc.Web.Logger;
using Btc.Web.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace LosExpress.Unit.Tests.Controllers.v1
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class ExampleUsersControllerTest
    {
        private Mock<IBtcLogger<ExampleUsersController>> _mockLogger;
        private Mock<IExampleUsersService> _mockService;
        private Mock<IErrorResponseHelper> _mockErrorResponseHelper;
        private ExampleUsersController _controller;

        [TestInitialize]
        public void Setup()
        {
            _mockLogger = new Mock<IBtcLogger<ExampleUsersController>>();
            _mockService = new Mock<IExampleUsersService>();
            _mockErrorResponseHelper = new Mock<IErrorResponseHelper>();
            _controller = new ExampleUsersController(_mockService.Object, _mockErrorResponseHelper.Object, _mockLogger.Object);
        }

        [TestMethod]
        public async Task GetUserAsync_EmptyUserId_BadRequest()
        {
            // Arrange
            _mockErrorResponseHelper
                .Setup(h => h.BadRequest(It.IsAny<IDictionary<string, string>>()))
                .Returns(new BadRequestObjectResult("response"));

            // Act
            var actionResult = await _controller.GetUserAsync(null) as BadRequestObjectResult;

            // Assert
            Assert.IsNotNull(actionResult);
            Assert.AreEqual((int)HttpStatusCode.BadRequest, actionResult.StatusCode);
        }

        [TestMethod]
        public async Task GetUserAsync_FoundUser_Ok()
        {
            // Arrange
            _mockService
                .Setup(s => s.GetUserByIdAsync(It.IsAny<string>()))
                .ReturnsAsync(new ExampleUser());

            // Act
            var actionResult = await _controller.GetUserAsync("test") as OkObjectResult;

            // Assert
            Assert.IsNotNull(actionResult);
            Assert.AreEqual((int)HttpStatusCode.OK, actionResult.StatusCode);
        }

        [TestMethod]
        public async Task GetUserAsync_NullUserResult_NotFound()
        {
            // Arrange
            _mockService
                .Setup(s => s.GetUserByIdAsync(It.IsAny<string>()))
                .ReturnsAsync((ExampleUser)null);

            // Act
            var actionResult = await _controller.GetUserAsync("test") as NotFoundResult;

            // Assert
            Assert.IsNotNull(actionResult);
            Assert.AreEqual((int)HttpStatusCode.NotFound, actionResult.StatusCode);
        }

        [TestMethod]
        public async Task GetUserAsync_ServiceException_InternalServerError()
        {
            // Arrange
            _mockErrorResponseHelper
                .Setup(h => h.InternalServerError(It.IsAny<string>(), It.IsAny<string>()))
                .Returns(new ObjectResult("response") { StatusCode = 500 });
            _mockService
                .Setup(s => s.GetUserByIdAsync(It.IsAny<string>()))
                .ThrowsAsync(new Exception());

            // Act
            var actionResult = await _controller.GetUserAsync("test") as ObjectResult;

            // Assert
            Assert.IsNotNull(actionResult);
            Assert.AreEqual((int)HttpStatusCode.InternalServerError, actionResult.StatusCode);
        }
    }
}
