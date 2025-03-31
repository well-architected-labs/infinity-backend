using _4erp.api.entities.skill;
using _4erp.domain.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("v1/skills")]
public class SkillControlador : ControllerBase
{
    private readonly IGenericService<Skill> _service;

    public SkillControlador(IGenericService<Skill> service)
    {
        _service = service;
    }


    [HttpGet("")]
    public async Task<IEnumerable<Skill>> GetBySlug([FromQuery] string? name = null)
    {
        return await _service.FindAsync(c => c.Name != null && (name == null || c.Name.Contains(name)));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult> GetByIdAsync(string id)
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
    public async Task<ActionResult> Create([FromBody] Skill Skill)
    {
        try
        {
            await _service.AddAsync(Skill);
            return Created();
        }
        catch (Exception exception)
        {
            return BadRequest(exception.Message);
        }
    }

    [Authorize]
    [HttpPut]
    public ActionResult Update([FromBody] Skill Skill)
    {
        try
        {
            _service.Update(Skill);
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
            _service.RemoveByConditionAsync(c => c.Id.Equals(Guid.Parse(id)));
            return Ok();
        }
        catch (Exception exception)
        {
            return BadRequest(exception.Message);
        }

    }
}
