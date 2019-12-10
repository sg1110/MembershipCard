using MembershipCardSystem.Status.Model;
using Microsoft.AspNetCore.Mvc;

namespace MembershipCardSystem.Status
{
    [ApiController]
    public class StatusController : ControllerBase
    {
    private readonly StatusSettings _settings;

    public StatusController(StatusSettings settings)
    {
        _settings = settings;
    }
        
    [HttpGet]
    [Route("membershipcard/v1/_status")]
    public ActionResult GetStatus()
    {
        var health = new ApplicationHealth(_settings.Version, _settings.Environment, "OK");
        return Ok(health);
    }
    }
}