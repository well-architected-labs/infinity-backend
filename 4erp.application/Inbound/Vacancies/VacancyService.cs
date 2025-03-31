using System.Linq.Expressions;
using System.Threading.Tasks;
using _4erp.api.entities;
using _4erp.api.entities.ocupation;
using _4erp.api.entities.skill;
using _4erp.api.entities.vacancy;
using _4erp.domain.Ports;
using _4erp.domain.repositories;
using _4erp.domain.Services.Tenant;
using _4erp.domain.Services.Vacancies;

namespace _4erp.application.Inbound.Users;
public class VacancyService : IVacancyService
{
    private readonly IUserRepository _userRepository;
    private readonly ITenantService _tenantService;

    private readonly IVacancyRepository _vacancyRepository;

    private readonly IGenericRepository<Vacancy> _repository;
    private readonly IGenericRepository<Ocupation> _ocupationRepository;
    private readonly IGenericRepository<Skill> _skillRepository;


    public VacancyService(
        IUserRepository userRepository,
        IGenericRepository<Vacancy> repository,
        ITenantService tenantService,
        IVacancyRepository vacancyRepository,
        IGenericRepository<Ocupation> ocupationRepository,
        IGenericRepository<Skill> skillRepository
        )
    {
        _userRepository = userRepository;
        _repository = repository;
        _tenantService = tenantService;
        _vacancyRepository = vacancyRepository;
        _ocupationRepository = ocupationRepository;
        _skillRepository = skillRepository;
    }

    public async void Remove(Guid id)
    {
        var userFound = await _repository.FindFirstAsync(
            c => c.Id.ToString() != null && c.Id.Equals(id))
            ?? throw new Exception("Não foi possível remover vaga! O ID não corresponde a nenhuma vaga!");

        _repository.Remove(userFound);
    }

    public void Update(Vacancy entity)
    {
        _vacancyRepository.UpdateAttachAsync(entity);
    }

    public async Task<int> CountAsync()
    {
        return await _repository.CountAsync();
    }


    public async Task<User?> FindByEmailAsync(string email)
    {
        return await _userRepository.FindByEmailAsync(email);
    }

    public async Task<List<Vacancy>> GetAllAsync(int skip, int take)
    {
        return await _repository.GetAllAsync(skip, take);
    }

    public async Task<List<Vacancy>> GetAllAsync(
        int skip, int take, Expression<Func<Vacancy, bool>> predicate)
    {
        return await _repository.GetAllAsync(skip, take, predicate);
    }

    public async Task<Vacancy?> GetByIdAsync(Guid id)
    {
        return await _repository.GetByIdAsync(id);
    }

    public async Task AddAsync(Vacancy entity)
    {
        var currentTenantFound = await _tenantService.GetCurrentAsync();

        if (currentTenantFound is null)
            throw new Exception("Tenant não encontrado!");

        if (currentTenantFound.Person is null)
            throw new Exception("Tenant not found!");


        entity.Person = currentTenantFound.Person;

        await _vacancyRepository.AddAttachAsync(entity);
    }

}