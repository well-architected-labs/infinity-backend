
using System.Linq.Expressions;

namespace _4erp.domain.VO;

public interface IGeneric<T>
{
    Task<T?> GetByIdAsync(Guid id);
    Task<List<T>> GetAllAsync(int skip, int take);
    Task<List<T>> GetAllAsync(int skip, int take, Expression<Func<T, bool>> predicate);
    Task AddAsync(T entity);
    void Update(T entity);
    void Remove(Guid id);
    Task<int> CountAsync();
}