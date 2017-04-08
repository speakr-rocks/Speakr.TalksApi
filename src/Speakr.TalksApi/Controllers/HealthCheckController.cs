using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace Speakr.TalksApi.Controllers
{
    [Route("[controller]")]
    public class HealthCheckController : Controller
    {
        [HttpGet]
        [Route("healthcheck")]
        [ApiExplorerSettings(IgnoreApi = true)]
        public IActionResult HealthCheck()
        {
            return Ok();
        }

        [HttpGet]
        [Authorize]
        [Route("authorizedhealthcheck")]
        [ApiExplorerSettings(IgnoreApi = true)]
        public IActionResult AuthorizedHealthCheck()
        {
            return Ok("You are successfully authorized using auth0");
        }
    }
}
