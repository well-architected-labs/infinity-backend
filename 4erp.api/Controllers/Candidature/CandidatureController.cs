using System.Linq.Expressions;
using System.Threading.Tasks;
using _4erp.api.DT.Vacancies;
using _4erp.api.entities;
using _4erp.api.entities.candidature;
using _4erp.application.Inbound.Authorization;
using _4erp.domain.repositories;
using _4erp.domain.Services;
using _4erp.domain.Services.Candidatures;
using _4erp.domain.Services.Tenant;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("v1/candidatures")]
public class CandidatureControlador : ControllerBase
{
    private readonly IGenericService<Candidature> _service;
    private readonly IGenericRepository<Candidature> _repository;
    private readonly ICandidatureService _candidatureService;
    private readonly IGenericRepository<Role> _roleRepository;
    private readonly ITenantService _tenantService;
    private readonly IMapper _mapper;

    public CandidatureControlador(
        IGenericService<Candidature> service,
        ICandidatureService candidatureService,
         IGenericRepository<Candidature> repository,
         IGenericRepository<Role> roleRepository,
         ITenantService tenantService,
        IMapper mapper)
    {
        _service = service;
        _repository = repository;
        _tenantService = tenantService;
        _candidatureService = candidatureService;
        _roleRepository = roleRepository;
        _mapper = mapper;
    }

    [Authorize]
    [HttpGet("subscribed")]
    public async Task<List<Candidature>> GetAllByCurrentTenantByParams(
        [FromQuery] string id,
        [FromQuery] int skip = 0,
        [FromQuery] int take = 20
        )
    {
        return await _candidatureService.GetAllSubscribed(
             id, skip, take
        );
    }


    [Authorize]
    [HttpGet("")]
    public async Task<List<Candidature>> GetAllByCurrentTenantByParams(
        [FromQuery] int skip = 0,
        [FromQuery] int take = 20,
        [FromQuery] string? candidature_status = null,
        [FromQuery] string? vacancy_status = null
        )
    {
        return await _candidatureService.GetAllByCurrentTenantByParams(
             skip, take, candidature_status, vacancy_status
        );
    }

    [HttpGet("{id}")]
    public async Task<ActionResult> GetByIdAsync(string id)
    {
        try
        {
            return Ok(
                await _candidatureService.GetByIdAsync(
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
    public async Task<ActionResult> Create([FromBody] CandidatureDT candidatureDT)
    {
        try
        {
            var candidature = _mapper.Map<Candidature>(candidatureDT);
            await _candidatureService.AddAsync(candidature);
            return Ok(candidature);
        }
        catch (Exception exception)
        {
            return BadRequest(new
            {
                message = exception.Message
            });
        }
    }

    [Authorize]
    [HttpPut]
    public async Task<ActionResult> Update([FromBody] Candidature candidature)
    {
        try
        {
            await _candidatureService.UpdateAttachAsync(candidature);
            return Ok(candidature);
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
