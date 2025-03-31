using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using _4erp.api.entities.vacancy;
using _4erp.infrastructure.data.context;
using _4erp.domain.repositories;
using _4erp.api.entities.candidature;
using _4erp.domain.repositories.Candidates;

namespace _4erp.infrastructure.Repositories.Vacancies
{
    public class CandidateRepository : GenericRepository<Candidature>, ICandidateRepository
    {
        private readonly AppDBContext _context;

        public CandidateRepository(AppDBContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Candidature?> FindFirstWithRelationsAsync(Expression<Func<Candidature, bool>> predicate)
        {
            return await _context.Candidatures
                .Include(v => v.Person)
                .FirstOrDefaultAsync(predicate);
        }

        public async Task AddAttachAsync(Candidature entity)
        {
            _context.Persons.Attach(entity.Person).State = EntityState.Unchanged;
            _context.Vacancies.Attach(entity.Vacancy).State = EntityState.Unchanged;
            _context.Status.Attach(entity.Status).State = EntityState.Unchanged;
            _context.Candidatures.Attach(entity).State = EntityState.Added;
            await _context.Candidatures.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAttachAsync(Candidature entity)
        {
            if (entity.Person is not null && entity.Person.Skills is not null)
                foreach (var include in entity.Person.Skills)
                    _context.Skills.Attach(include).State = EntityState.Unchanged;

            _context.Persons.Attach(entity.Person).State = EntityState.Unchanged;
            _context.Vacancies.Attach(entity.Vacancy).State = EntityState.Unchanged;
            _context.Candidatures.Update(entity);
            await _context.SaveChangesAsync();
        }
    }
}
