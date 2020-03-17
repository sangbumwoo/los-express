using System.Net;
using Microsoft.AspNetCore.Mvc;

namespace LosExpress.Controllers
{
    [ApiVersionNeutral]
    public class InternalController : ControllerBase
    {
        [HttpDelete(".internal/v1/state/user/{usid}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        [ProducesResponseType(501)]
        [ApiExplorerSettings(IgnoreApi = true)]
        public IActionResult DeleteUserData(string usid)
        {
            return StatusCode((int)HttpStatusCode.NotImplemented);
        }
    }
}