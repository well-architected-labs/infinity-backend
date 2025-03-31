using _4erp.api.entities.ocupation;
using _4erp.domain.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("v1/ocupations")]
public class OcupationControlador : ControllerBase
{
    private readonly IGenericService<Ocupation> _service;

    public OcupationControlador(IGenericService<Ocupation> service)
    {
        _service = service;
    }

    [HttpGet("")]
    public async Task<List<Ocupation>> GetBySlug([FromQuery] string? name = null)
    {
        return await _service.FindAsync(c => c.Name != null && (name == null || c.Name.Contains(name)));
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
    public async Task<ActionResult> Create([FromBody] Ocupation ocupation)
    {
        try
        {
            await _service.AddAsync(ocupation);
            return Created();
        }
        catch (Exception exception)
        {
            return BadRequest(exception.Message);
        }
    }

    [Authorize]
    [HttpPut]
    public ActionResult Update([FromBody] Ocupation ocupation)
    {
        try
        {
            _service.Update(ocupation);
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
