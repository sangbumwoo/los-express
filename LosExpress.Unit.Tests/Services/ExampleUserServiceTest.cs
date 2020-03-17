using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using LosExpress.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LosExpress.Unit.Tests.Services
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class ExampleUserServiceTest
    {
        private ExampleUsersService _service;

        [TestInitialize]
        public void Setup()
        {
            _service = new ExampleUsersService();
        }

        [TestMethod]
        public async Task GetUserAsync_ReturnsUser()
        {
            // Arrange
            // Act
            var user = await _service.GetUserByIdAsync("test");

            // Assert
            Assert.IsNotNull(user);
        }
    }
}
