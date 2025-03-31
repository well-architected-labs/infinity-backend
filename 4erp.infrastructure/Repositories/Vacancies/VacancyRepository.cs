using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using _4erp.api.entities.vacancy;
using _4erp.infrastructure.data.context;
using _4erp.domain.repositories;

namespace _4erp.infrastructure.Repositories.Vacancies
{
    public class VacancyRepository : GenericRepository<Vacancy>, IVacancyRepository
    {
        private readonly AppDBContext _context;


        public async Task AddAttachAsync(Vacancy entity)
        {

            if (entity.Skills is not null)
                foreach (var include in entity.Skills)
                    _context.Skills.Attach(include).State = EntityState.Unchanged;

            _context.Status.Attach(entity.Status).State = EntityState.Unchanged;
            _context.Persons.Attach(entity.Person).State = EntityState.Unchanged;
            _context.Ocupations.Attach(entity.Ocupation).State = EntityState.Unchanged;
            _context.Vacancies.Attach(entity).State = EntityState.Added;

            await _context.Vacancies.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public VacancyRepository(AppDBContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Vacancy?> FindFirstWithRelationsAsync(Expression<Func<Vacancy, bool>> predicate)
        {
            return await _context.Vacancies
                .Include(v => v.Ocupation)
                .Include(v => v.Person)
                .Include(v => v.Status)
                .Include(v => v.Skills)
                .FirstOrDefaultAsync(predicate);
        }

        public async Task UpdateAttachAsync(Vacancy entity)
        {
            if (entity.Skills is not null)
                foreach (var include in entity.Skills)
                    _context.Skills.Attach(include).State = EntityState.Modified;

            _context.Status.Attach(entity.Status).State = EntityState.Unchanged;
            _context.Persons.Attach(entity.Person).State = EntityState.Unchanged;
            _context.Ocupations.Attach(entity.Ocupation).State = EntityState.Unchanged;
            _context.Vacancies.Attach(entity).State = EntityState.Modified;
            _context.Vacancies.Update(entity);
            await _context.SaveChangesAsync();
        }
    }
}
