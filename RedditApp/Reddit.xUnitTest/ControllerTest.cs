using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using NSubstitute;
using Reddit.Domain.Services.Abstractions;
using Reddit.Web.Controllers;

namespace Reddit.xUnitTest
{
    public class ControllerTest
    {
        private readonly IServiceManager _serviceManager = Substitute.For<IServiceManager>();
        private readonly IConfiguration _config = Substitute.For<IConfiguration>();
        private readonly ILogger<HomeController> _logger = Substitute.For<ILogger<HomeController>>();

        [Fact]
        public void SignInRedirect()
        {
            // Arrange
            var controller = new HomeController(_serviceManager, _config, _logger);

            // Act
            var result = controller.SignIn();

            // Assert
            var redirectToActionResult = Assert.IsType<RedirectResult>(result);
            Assert.Contains("state=reddit-app", redirectToActionResult.Url);
        }
    }
}
