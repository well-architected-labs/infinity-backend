using _4erp.domain.Services.Candidatures;
using _4erp.domain.repositories;
using _4erp.api.entities.candidature;
using System.Linq.Expressions;
using _4erp.domain.Services.Tenant;
using _4erp.domain.repositories.Candidates;
using _4erp.api.entities.status;
using _4erp.api.entities;

namespace _4erp.application.services.Candidatures
{
    public class CandidatureService : ICandidatureService
    {
        private readonly IGenericRepository<Candidature> _repository;
        private readonly IGenericRepository<Status> _statusRepository;
        private readonly ICandidateRepository _candidateRepository;
        private readonly ITenantService _tenantService;
        private readonly IGenericRepository<Role> _roleRepository;

        public CandidatureService(
            IGenericRepository<Candidature> repository,
            ITenantService tenantService,
            IGenericRepository<Role> roleRepository,
            ICandidateRepository candidateRepository,
            IGenericRepository<Status> statusRepository)
        {
            _repository = repository;
            _tenantService = tenantService;
            _candidateRepository = candidateRepository;
            _statusRepository = statusRepository;
            _roleRepository = roleRepository;
        }

        public async Task AddAsync(Candidature entity)
        {
            var currentTenant = await _tenantService.GetCurrentAsync();
            if (currentTenant is null)
                throw new Exception("Tenant não encontrado!");

            var existCandidature = await _repository.FindFirstAsync(c => c.Person != null && currentTenant.Person != null && c.Person.Id.Equals(currentTenant.Person.Id) && c.Vacancy.Id.Equals(entity.Vacancy.Id));
            if (existCandidature is not null)
                throw new Exception("Você já se candidatou a esta vaga!");

            var status = await _statusRepository.FindFirstAsync(c => c.Name != null && c.Name.Equals("candidate_evaluation"));
            if (status is not null)
                entity.Status = status;



            if (currentTenant is null)
                throw new Exception("Tenant não encontrado!");

            entity.Person = currentTenant.Person;

            await _candidateRepository.AddAttachAsync(entity);
        }

        public async Task<int> CountAsync()
        {
            return await _repository.CountAsync();
        }

        public async Task<List<Candidature>> GetAllAsync(int skip, int take)
        {
            return await _repository.GetAllAsync(skip, take);
        }

        public async Task<List<Candidature>> GetAllAsync(int skip, int take, Expression<Func<Candidature, bool>> predicate)
        {
            return await _repository.GetAllAsync(skip, take, predicate);
        }

        public async Task<List<Candidature>> GetAllByCurrentTenantByParams(
            int skip = 0, int take = 20, string?
            statusCandidature = null, string?
            vacancyStatus = null)
        {

            var user = await _tenantService.GetCurrentAsync();

            if (user is null)
                throw new Exception("Tenant não encontrado!");

            if (user.Person is null)
                throw new Exception("Tenant não encontrado!");

            var role = await _roleRepository.FindFirstAsync(
                u => u.Alias.Equals(user.Role.Alias)
            );

            if (role is not null && role.Alias.Equals("administrator:person:system:*"))
                return await _repository.GetAllAsync(
                    skip,
                    take,
                        u => u.Person != null && u.Person.Id.Equals(user.Person.Id),
                        c => c.Vacancy,
                        c => c.Vacancy.Status,
                        c => c.Vacancy.Skills,
                        c => c.Vacancy.Ocupation,
                        c => c.Vacancy.Person,
                        c => c.Status,
                        c => c.Person
                    );

            if (role is not null && role.Alias.Equals("administrator:company:system:*"))
                return await _repository.GetAllAsync(
                    skip,
                    take,
                        u => u.Vacancy.Person.Id.Equals(user.Person.Id),
                        c => c.Vacancy,
                        c => c.Vacancy.Status,
                        c => c.Vacancy.Skills,
                        c => c.Vacancy.Ocupation,
                        c => c.Vacancy.Person,
                        c => c.Status,
                        c => c.Person
                    );
            else
                return await _repository.GetAllAsync(
                    skip,
                    take,
                        u => u.CreatedAt != null,
                        c => c.Vacancy,
                        c => c.Vacancy.Status,
                        c => c.Vacancy.Skills,
                        c => c.Vacancy.Ocupation,
                        c => c.Vacancy.Person,
                        c => c.Status,
                        c => c.Person
            );
        }

        public async Task<List<Candidature>> GetAllSubscribed(string id, int skip = 0, int take = 20)
        {
            var user = await _tenantService.GetCurrentAsync();

            if (user is null)
                throw new Exception("Tenant não encontrado!");

            if (user.Person is null)
                throw new Exception("Tenant não encontrado!");

            var role = await _roleRepository.FindFirstAsync(
                u => u.Alias.Equals(user.Role.Alias)
            );

            if (role is not null && role.Alias.Equals("administrator:person:system:*"))
                return await _repository.GetAllAsync(
                    skip,
                    take,
                        u => u.Person != null && u.Person.Id.Equals(user.Person.Id) && u.Vacancy.Id.Equals(Guid.Parse(id)),
                        c => c.Vacancy,
                        c => c.Status,
                        c => c.Person,
                        c => c.Person.Bio,
                        c => c.Person.Phone
                    );

            if (role is not null && role.Alias.Equals("administrator:company:system:*"))
                return await _repository.GetAllAsync(
                    skip,
                    take,
                        u => u.Vacancy.Person.Id.Equals(user.Person.Id) && u.Vacancy.Id.Equals(Guid.Parse(id)),
                        c => c.Vacancy,
                        c => c.Status,
                        c => c.Person,
                        c => c.Person.Bio,
                        c => c.Person.Phone
                    );
            else
                return await _repository.GetAllAsync(
                    skip,
                    take,
                        u => u.CreatedAt != null,
                        c => c.Vacancy,
                        c => c.Status,
                        c => c.Person
            );
        }

        public async Task<Candidature?> GetByIdAsync(Guid id)
        {
            return await _repository.FindByFieldAsync(
                c => c.Id.Equals(id),
                u => u.Status,
                u => u.Vacancy,
                u => u.Vacancy.Status,
                u => u.Vacancy.Skills,
                u => u.Vacancy.Ocupation,
                u => u.Person,
                u => u.Vacancy.Person,
                u => u.Person.Bio,
                u => u.Person.Bio.Educations,
                u => u.Person.Bio.Experiences,
                u => u.Person.Phone,
                u => u.Person.Skills
            );
        }

        public async void Remove(Guid id)
        {
            var candidature = await _repository.GetByIdAsync(id);
            if (candidature is null)
                throw new Exception("Candidatura não existe!");

            _repository.Remove(candidature);
        }

        public async void Update(Candidature entity)
        {
            var candidature = await _repository.GetByIdAsync(entity.Id);
            if (candidature is null)
                throw new Exception("Candidatura não existe!");

            _repository.Update(candidature);
        }

        public async Task UpdateAttachAsync(Candidature entity)
        {
            var candidature = await _repository.FindByFieldAsync(
                c => c.Id.Equals(entity.Id),
                u => u.Person,
                u => u.Status,
                u => u.Vacancy,
                u => u.Vacancy.Status,
                u => u.Vacancy.Skills,
                u => u.Vacancy.Ocupation,
                u => u.Vacancy.Person,
                u => u.Person.Bio,
                u => u.Person.Phone,
                u => u.Person.Skills
            );
            if (candidature is null)
                throw new Exception("Candidatura não existe!");

            candidature.Status = entity.Status;

            await _candidateRepository.UpdateAttachAsync(candidature);
        }
    }
}
