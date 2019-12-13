using MembershipCardSystem.LogOut.Model;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Examples;

namespace MembershipCardSystem.LogOut
{
    [ApiController]
    public class LogOutController : ControllerBase
    {
        [HttpGet]
        [Route("card/logout")]
        [Produces("application/json")]
        [SwaggerRequestExample(typeof(LogOutResponse), typeof(LogOutResponseModelExample))]
        public OkObjectResult LogOutMessage()
        {
            return Ok(new LogOutResponse("Goodbye"));

        }
    }
}
