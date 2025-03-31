using _4erp.domain.repositories;
using _4erp.domain.Services;
using System.Linq.Expressions;

namespace _4erp.application.inbound;
public class GenericService<T> : IGenericService<T> where T : class
{
    private readonly IGenericRepository<T> _repository;

    public GenericService(IGenericRepository<T> repository)
    {
        _repository = repository;
    }

    public async Task<T?> GetByIdAsync(Guid id)
    {
        return await _repository.GetByIdAsync(id);
    }

    public async Task<List<T>> GetAllAsync(int skip, int take)
    {
        return await _repository.GetAllAsync(skip, take);
    }

    public async Task AddAsync(T entity)
    {
        await _repository.AddAsync(entity);
        await _repository.SaveChangesAsync();
    }

    public void Update(T entity)
    {
        _repository.Update(entity);
    }

    public async void Remove(Guid id)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity is null)
            throw new KeyNotFoundException($"Entidade com ID {id} não encontrada.");

        _repository.Remove(entity);
    }

    public async Task<int> CountAsync()
    {
        return await _repository.CountAsync();
    }

    public async Task<List<T>> FindAsync(Expression<Func<T, bool>> predicate)
    {
        return await _repository.FindAsync(predicate);
    }

    public async Task RemoveByConditionAsync(Expression<Func<T, bool>> predicate)
    {
        await _repository.RemoveByConditionAsync(predicate);
    }

    public Task<T?> FindFirstAsync(Expression<Func<T, bool>> predicate)
    {
        return _repository.FindFirstAsync(predicate);
    }

}
