using System.Linq.Expressions;
using _4erp.api.entities.vacancy;

namespace _4erp.domain.repositories
{
    public interface IVacancyRepository : IGenericRepository<Vacancy>
    {
        Task<Vacancy?> FindFirstWithRelationsAsync(Expression<Func<Vacancy, bool>> predicate);
        Task AddAttachAsync(Vacancy entity);
        Task UpdateAttachAsync(Vacancy entity);
    }
}