using System.Linq.Expressions;

namespace _4erp.domain.repositories;
public interface IGenericRepository<T> where T : class
{
    Task<T?> GetByIdAsync(Guid id);
    Task AddAsync(T entity);
    void Update(T entity);
    void Remove(T entity);
    Task<int> CountAsync();
    Task<List<T>> GetAllAsync(int skip, int take);
    Task<List<T>> GetAllAsync(int skip, int take, Expression<Func<T, bool>> predicate);
    Task<int> GetCountAsync(Expression<Func<T, bool>> predicate);
    Task<List<T>> GetAllAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes);
    Task<List<T>> GetAllAsync(int skip, int take, Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes);
    Task<List<T>> FindAsync(Expression<Func<T, bool>> predicate);
    Task SaveChangesAsync();
    Task RemoveByConditionAsync(Expression<Func<T, bool>> predicate);
    Task<T?> FindFirstAsync(Expression<Func<T, bool>> predicate);
    Task<T?> FindByFieldAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes);

}