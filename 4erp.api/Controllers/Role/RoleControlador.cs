using _4erp.api.entities;
using _4erp.domain.repositories;
using _4erp.domain.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("v1/roles")]
public class RoleControlador : ControllerBase
{
    private readonly IGenericService<Role> _service;
    private readonly IGenericRepository<Role> _repository;

    public RoleControlador(
        IGenericService<Role> service,
        IGenericRepository<Role> repository
        )
    {
        _service = service;
        _repository = repository;
    }

    [HttpGet("")]
    public async Task<IEnumerable<Role>> GetBySlug([FromQuery] string alias)
    {
        return await _repository.GetAllAsync(
                    u => u.Alias.Contains(alias),
                    c => c.Scopes
        );
    }

    [HttpGet("{id}")]
    public async Task<ActionResult> GetById(string id)
    {
        try
        {
            return Ok(
                await _repository.FindByFieldAsync(
                    u => u.Id.Equals(Guid.Parse(id)),
                    c => c.Scopes
            ));
        }
        catch (Exception exception)
        {
            return BadRequest(exception.Message);
        }

    }

    [Authorize]
    [HttpPost]
    public async Task<ActionResult> Create([FromBody] Role Role)
    {
        try
        {
            await _service.AddAsync(Role);
            return Created();
        }
        catch (Exception exception)
        {
            return BadRequest(exception.Message);
        }
    }

    [Authorize]
    [HttpPut]
    public ActionResult Update([FromBody] Role Role)
    {
        try
        {
            _service.Update(Role);
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
