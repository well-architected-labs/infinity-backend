
using System.Threading.Tasks;
using _4erp.application.Inbound.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("v1/authorization")]
public class AuthorizationController : ControllerBase
{
    private readonly IAuthorizationService _authorizationService;

    public AuthorizationController(IAuthorizationService authorizationService)
    {
        _authorizationService = authorizationService;
    }


    [HttpPost("sign-in")]
    public async Task<IActionResult> Authorization([FromBody] Authorization authorization)
    {
        try
        {
            return Ok(await _authorizationService.Authorization(authorization));
        }
        catch (Exception exception)
        {
            return Unauthorized(exception.Message);
        }
    }

    [HttpPost("sign-up")]
    public async Task<IActionResult> Register([FromBody] Authorization authorization)
    {
        try
        {
            var authorized = await _authorizationService.Register(authorization);
            return Ok(authorized);
        }
        catch (Exception exception)
        {
            return Unauthorized(exception.Message);
        }
    }
}
