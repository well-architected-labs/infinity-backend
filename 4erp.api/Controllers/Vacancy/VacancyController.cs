using System.Linq.Expressions;
using _4erp.api.DT.Vacancies;
using _4erp.api.entities.candidature;
using _4erp.api.entities.vacancy;
using _4erp.application.Inbound.Users;
using _4erp.domain.repositories;
using _4erp.domain.Services;
using _4erp.domain.Services.Vacancies;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("v1/vacancies")]
public class VacancyControlador : ControllerBase
{
    private readonly IGenericService<Vacancy> _service;
    private readonly IGenericRepository<Candidature> _candidatureRepository;
    private readonly IGenericRepository<Vacancy> _repository;
    private readonly IVacancyService _vacancyService;
    private readonly IVacancyRepository _vacancyRepository;
    private readonly IMapper _mapper;

    public VacancyControlador(
        IGenericService<Vacancy> service,
        IVacancyRepository vacancyRepository,
        IVacancyService vacancyService,
         IGenericRepository<Candidature> candidatureRepository,
         IGenericRepository<Vacancy> repository,
        IMapper mapper)
    {
        _service = service;
        _vacancyRepository = vacancyRepository;
        _vacancyService = vacancyService;
        _repository = repository;
        _candidatureRepository = candidatureRepository;
        _mapper = mapper;
    }

    [HttpGet("")]
    public async Task<IEnumerable<Vacancy>> GetAllAsync(
        [FromQuery] int skip,
        [FromQuery] int take,
        [FromQuery] string? title = null,
        [FromQuery] DateTime? dateInit = null,
        [FromQuery] DateTime? dateEnd = null,
        [FromQuery] List<string>? skills = null,
        [FromQuery] string? ocupation = null,
        [FromQuery] string? status = null
        )
    {
        return await _vacancyService.GetAllAsync(
            skip,
            take,
            title,
            dateInit,
            dateEnd,
            skills,
            ocupation,
            status
        );
    }


    [HttpGet("tiles")]
    public async Task<List<Vacancy>> GetTilesVacancies(
        [FromQuery] int skip, [FromQuery] int take)
    {

        var vacancies = await _repository.GetAllAsync(
            skip,
            take,
            u => u.CreatedAt != null,
            c => c.Ocupation,
            c => c.Person
        );

        return vacancies;
    }



    [HttpGet("{id}")]
    public async Task<ActionResult> FindFirstAsync(string id)
    {
        try
        {
            return Ok(
                await _vacancyRepository.FindFirstWithRelationsAsync(
                c => c.Id.Equals(Guid.Parse(id)))
            );
        }
        catch (Exception exception)
        {
            return BadRequest(exception.Message);
        }

    }

    [Authorize]
    [HttpPost]
    public async Task<ActionResult> Create([FromBody] VacancyDT vacancyDT)
    {
        try
        {
            var vacancy = _mapper.Map<Vacancy>(vacancyDT);
            await _vacancyService.AddAsync(vacancy);
            return Ok(vacancy);
        }
        catch (Exception exception)
        {
            return BadRequest(exception.Message);
        }
    }

    [HttpPut]
    public ActionResult Update([FromBody] VacancyDT vacancyDT)
    {
        try
        {
            var vacancy = _mapper.Map<Vacancy>(vacancyDT);
            _vacancyService.Update(vacancy);
            return Ok(vacancy);
        }
        catch (Exception exception)
        {
            return BadRequest(exception.Message);
        }
    }

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
