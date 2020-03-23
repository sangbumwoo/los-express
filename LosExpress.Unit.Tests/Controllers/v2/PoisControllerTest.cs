using Btc.Web.Logger;
using Btc.Web.Utils;
using LosExpress.Controllers.v2;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;


namespace LosExpress.Unit.Tests.Controllers.v2
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    class PoisControllerTest
    {
        private Mock<IBtcLogger<PoisController>> _mockLogger;
        //private Mock<IExampleUsersService> _mockService;
        private Mock<IErrorResponseHelper> _mockErrorResponseHelper;
        private PoisController _controller;
        private Mock<IHttpClientFactory> _mockHttpClientFactory;
        private Mock<IConfiguration> _mockConfiguration;


        [TestInitialize]
        public void Setup()
        {
            _mockLogger = new Mock<IBtcLogger<PoisController>>();
            //_mockService = new Mock<IExampleUsersService>();
            _mockErrorResponseHelper = new Mock<IErrorResponseHelper>();
            _controller = new PoisController(
                _mockErrorResponseHelper.Object, 
                _mockLogger.Object, 
                _mockHttpClientFactory.Object, 
                _mockConfiguration.Object);
        }

        [TestMethod]
        public async Task GetPoisAsync_EmptyUserId_BadRequest()
        {
            // Arrange
            _mockErrorResponseHelper
                .Setup(h => h.BadRequest(It.IsAny<IDictionary<string, string>>()))
                .Returns(new BadRequestObjectResult("response"));

            // Act
            var actionResult = await _controller.GetPoisAsync(null) as BadRequestObjectResult;

            // Assert
            Assert.IsNotNull(actionResult);
            Assert.AreEqual((int)HttpStatusCode.BadRequest, actionResult.StatusCode);
        }

    }
}

