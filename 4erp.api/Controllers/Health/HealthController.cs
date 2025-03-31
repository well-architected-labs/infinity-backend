
using System.Threading.Tasks;
using _4erp.application.Inbound.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("v1/health")]
public class HealthController : ControllerBase
{

    [HttpGet("")]
    public IActionResult Health()
    {
        return Ok(new
        {
            Health = true
        });
    }
}
