using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace MembershipCardSystem.LogOut
{
    [ApiController]
    public class LogOutController : ControllerBase
    {
        [HttpGet]
        [Route("membershipcard/logout")]
        [Produces("application/json")]
        public OkObjectResult LogOutMessage()
        {
            return Ok("Goodbye");
        }
    }
}