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
    private readonly IGenericRepository<Role> _roleRepository;


    public VacancyService(
        IUserRepository userRepository,
        IGenericRepository<Vacancy> repository,
        ITenantService tenantService,
        IVacancyRepository vacancyRepository,
        IGenericRepository<Ocupation> ocupationRepository,
        IGenericRepository<Skill> skillRepository,
        IGenericRepository<Role> roleRepository
        )
    {
        _userRepository = userRepository;
        _repository = repository;
        _tenantService = tenantService;
        _vacancyRepository = vacancyRepository;
        _ocupationRepository = ocupationRepository;
        _skillRepository = skillRepository;
        _roleRepository = roleRepository;
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

    public async Task<List<Vacancy>> GetAllAsync(
        int skip,
        int take,
        string? title = null,
        DateTime? dateInit = null,
        DateTime? dateEnd = null,
        List<string>? skills = null,
        string? ocupation = null,
        string? status = null
    )
    {

        Expression<Func<Vacancy, bool>> predicate = u => u.CreatedAt != null;

        var user = await _tenantService.GetCurrentAsync();

        if (user is null)
            throw new Exception("Tenant não encontrado!");

        if (user.Person is null)
            throw new Exception("Tenant não encontrado!");

        var role = await _roleRepository.FindFirstAsync(
            u => u.Alias.Equals(user.Role.Alias)
        );


        if (role is not null && role.Alias.Equals("administrator:company:system:*"))
        {

            if (dateInit is not null && dateEnd is not null && ocupation is not null && status is not null)
                predicate = u => u.Person != null && u.Person.Id.Equals(user.Person.Id) && u.DateInit != null && u.DateInit != null ? u.DateInit >= dateEnd && u.DateEnd <= dateInit : u.CreatedAt != null && ocupation != null && u.Ocupation != null && u.Ocupation.Id != Guid.Empty ? u.Ocupation.Id.Equals(Guid.Parse(ocupation)) : u.CreatedAt != null && u.Status != null && u.Status.Name != null && u.Status.Name.Contains(status);

            if (ocupation is not null)
                predicate = u => u.Person != null && u.Person.Id.Equals(user.Person.Id) && ocupation != null && u.Ocupation != null && u.Ocupation.Id != Guid.Empty ? u.Ocupation.Id.Equals(Guid.Parse(ocupation)) : u.CreatedAt != null;

            if (title is not null)
                predicate = u => u.Person != null && u.Person.Id.Equals(user.Person.Id) && u.Title != null && u.Title.Contains(title);

            if (status is not null)
                predicate = u => u.Person != null && u.Person.Id.Equals(user.Person.Id) && u.Status != null && u.Status.Name != null && u.Status.Name.Contains(status);

            if (dateInit is not null && dateEnd is not null && ocupation is not null && status is not null)
                predicate = u => u.Person != null && u.Person.Id.Equals(user.Person.Id) && u.DateInit != null && u.DateInit != null ? u.DateInit >= dateEnd && u.DateEnd <= dateInit : u.CreatedAt != null;
            
            if (user.Person != null)
                predicate = u => u.Person != null && u.Person.Id.Equals(user.Person.Id);

                return await _repository.GetAllAsync(
                    skip,
                    take,
                    predicate,
                    c => c.Ocupation,
                    c => c.Status,
                    c => c.Skills,
                    c => c.Person
                    );
        }

        return await _repository.GetAllAsync(
                skip,
                take,
                u => u.CreatedAt != null,
                c => c.Ocupation,
                c => c.Status,
                c => c.Skills,
                c => c.Person
            );
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