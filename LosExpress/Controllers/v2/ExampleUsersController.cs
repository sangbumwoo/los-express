using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Btc.Swagger;
using LosExpress.Models;
using LosExpress.Services;
using LosExpress.ViewModels;
using Btc.Web.Logger;
using Btc.Web.Utils;
using Microsoft.AspNetCore.Mvc;

namespace LosExpress.Controllers.v2
{
    [ApiController]
    [ApiVersion("2.0")]
    [Route("api/v{version:apiVersion}/users")]
    [BtcSwaggerTag("Get users")]
    public class ExampleUsersController : ControllerBase
    {
        private readonly IExampleUsersService _exampleUserService;
        private readonly IBtcLogger<ExampleUsersController> _logger;
        private readonly IErrorResponseHelper _errorResponseHelper;

        public ExampleUsersController(IExampleUsersService exampleUserService, IErrorResponseHelper errorResponseHelper, IBtcLogger<ExampleUsersController> logger)
        {
            _exampleUserService = exampleUserService;
            _logger = logger;
            _errorResponseHelper = errorResponseHelper;
        }

        // ** GET api/v1/users/{userId} **
        /// <summary>
        /// Get a user.
        /// </summary>
        /// <returns>Single object. Possible response status codes are 200, 404 and 500.</returns>
        /// <response code="200">Resource created</response>
        /// <response code="400">This is a bad, bad request</response>
        /// <response code="404">This resource does not exist</response>
        /// <response code="500">Error while getting user</response>
        [HttpGet("{userId}", Name = "User")]
        [ProducesResponseType(typeof(ExampleUser), 200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetUserAsync(string userId)
        {
            var logProperties = new Dictionary<string, string> { ["user_id"] = userId };
            try
            {
                // Validate request
                if (string.IsNullOrWhiteSpace(userId))
                {
                    // Return BMW API developer guidelines-compliant error response
                    return _errorResponseHelper.BadRequest(new Dictionary<string, string> { ["userId"] = "userId must not be null or empty" });
                }

                // Retrieve data
                var user = await _exampleUserService.GetUserByIdAsync(userId);

                // Validate response
                if (user == null)
                {
                    return NotFound();
                }

                // Map result to response object
                var response = new ExampleUserDTO
                {
                    Name = user.Name,
                    Id = user.Id
                };

                // Return response
                return Ok(response);
            }
            catch (Exception e)
            {
                // Log exception with contextually relevant properties
                _logger.LogError(e, "get-user-error", logProperties);

                // Return BMW API developer guidelines-compliant error response
                return _errorResponseHelper.InternalServerError("get-user-error", "Error while retrieving user");
            }
        }
    }
}
