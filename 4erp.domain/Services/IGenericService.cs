using System.Linq.Expressions;

namespace _4erp.domain.Services
{
    public interface IGenericService<T> where T : class
    {
        Task<T?> GetByIdAsync(Guid id);
        Task<List<T>> GetAllAsync(int skip, int take);
        Task AddAsync(T entity);
        void Update(T entity);
        void Remove(Guid id);
        Task<int> CountAsync();
        Task<List<T>> FindAsync(Expression<Func<T, bool>> predicate);
        Task RemoveByConditionAsync(Expression<Func<T, bool>> predicate);
        Task<T?> FindFirstAsync(Expression<Func<T, bool>> predicate);

    }
}