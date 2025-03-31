using System.IdentityModel.Tokens.Jwt;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Threading.Tasks;
using _4erp.api.entities;
using _4erp.domain.Ports;
using _4erp.domain.repositories;
using _4erp.domain.Services.Tenant;
using _4erp.domain.Services.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("v1/users")]
public class UserControlador : ControllerBase
{
    private readonly IUserService _userService;
    private readonly ITenantService _tenantService;
    private readonly IUserRepository _userRepository;
    private readonly IGenericRepository<User> _repository;

    public UserControlador(
        IUserService userService,
        ITenantService tenantService,
        IUserRepository userRepository,
        IGenericRepository<User> repository)
    {
        _userService = userService;
        _tenantService = tenantService;
        _userRepository = userRepository;
        _repository = repository;
    }

    [Authorize]
    [HttpGet("current")]
    public async Task<ActionResult<User>> GetCurrentAsync()
    {
        try
        {
            var currentTenant = await _tenantService.GetCurrentAsync();
            if (currentTenant is not null)
                return Ok(currentTenant);
            else
                return Unauthorized("Tenant não encontrado!");
        }
        catch (Exception exception)
        {
            return Unauthorized(exception.Message);
        }
    }


    [HttpGet("")]
    public async Task<List<User>> GetAllAsync(
        [FromQuery] int skip,
        [FromQuery] int take,
        [FromQuery] string? email = null,
        [FromQuery] string? name = null,
        [FromQuery] int? type = 1


    )
    {

        Expression<Func<User, bool>> predicate = u => u.Person != null && u.Person.Type.Equals(type);

        if (email is not null)
            predicate = u => u.Person != null && u.Person.Type.Equals(type)
                               && u.Email != null && u.Email.Equals(email)
                               && u.Person != null && u.Person.Type.Equals(type);

        if (name is not null)
            predicate = u => u.Person != null && u.Person.FirstName.Contains(name);


        return await _repository.GetAllAsync(
            skip, take,
                predicate,
                n => n.Role,
                n => n.Person,
                n => n.Person.Phone
            );
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<User?>> GetByIdAsync(string id)
    {
        return await _userService.FindFirstAsync(id);
    }

    [HttpPost]
    public async Task<ActionResult> Create([FromBody] User user)
    {
        await _userService.AddAsync(user);
        return Created();
    }

    [HttpPut]
    public async Task<ActionResult> Update([FromBody] User user)
    {
        try
        {
            await _userService.UpdateAsync(user);
            return Ok(user);
        }
        catch (Exception exception)
        {
            return BadRequest(exception.Message);
        }
    }

    [HttpDelete("{id}")]
    public ActionResult Remove(string id)
    {
        _userService.Remove(Guid.Parse(id));
        return Ok();
    }
}
