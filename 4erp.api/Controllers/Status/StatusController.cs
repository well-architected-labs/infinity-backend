using _4erp.api.entities.status;
using _4erp.domain.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("v1/status")]
public class StatusControlador : ControllerBase
{
    private readonly IGenericService<Status> _service;

    public StatusControlador(IGenericService<Status> service)
    {
        _service = service;
    }

    [HttpGet("")]
    public async Task<List<Status>> GetBySlug([FromQuery] string slug)
    {
        return await _service.FindAsync(c => c.Slug != null && c.Slug.Equals(slug));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult> Get(string id)
    {
        try
        {
            return Ok(
                await _service.GetByIdAsync(
                    Guid.Parse(id))
            );
        }
        catch (Exception exception)
        {
            return BadRequest(exception.Message);
        }

    }

    [Authorize]
    [HttpPost]
    public async Task<ActionResult> Create([FromBody] Status status)
    {
        try
        {
            await _service.AddAsync(status);
            return Created();
        }
        catch (Exception exception)
        {
            return BadRequest(exception.Message);
        }
    }


    [Authorize]
    [HttpPut]
    public ActionResult Update([FromBody] Status status)
    {
        try
        {
            _service.Update(status);
            return Created();
        }
        catch (Exception exception)
        {
            return BadRequest(exception.Message);
        }
    }

    [Authorize]
    [HttpDelete("{id}")]
    public ActionResult Remove(string id)
    {
        try
        {
            _service.Remove(Guid.Parse(id));
            return Ok();
        }
        catch (Exception exception)
        {
            return BadRequest(exception.Message);
        }

    }
}
