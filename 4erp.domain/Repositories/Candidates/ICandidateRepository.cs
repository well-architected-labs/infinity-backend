using System.Linq.Expressions;
using _4erp.api.entities.candidature;
using _4erp.api.entities.vacancy;

namespace _4erp.domain.repositories.Candidates;
public interface ICandidateRepository : IGenericRepository<Candidature>
{
    Task<Candidature?> FindFirstWithRelationsAsync(Expression<Func<Candidature, bool>> predicate);
    Task AddAttachAsync(Candidature entity);
    Task UpdateAttachAsync(Candidature entity);
}