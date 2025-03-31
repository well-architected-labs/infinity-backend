using _4erp.domain.repositories;
using _4erp.infrastructure.data.context;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace _4erp.infrastructure.Repositories;
public class GenericRepository<T> : IGenericRepository<T> where T : class
{
    private readonly AppDBContext _context;
    private readonly DbSet<T> _dbSet;

    public GenericRepository(AppDBContext context)
    {
        _context = context;
        _dbSet = _context.Set<T>();
    }

    public async Task<T?> GetByIdAsync(Guid id)
    {
        return await _dbSet.FindAsync(id);
    }

    public async Task<List<T>> GetAllAsync(int skip, int take)
    {
        return await _dbSet.Skip(skip).Take(take).ToListAsync();
    }

    public async Task AddAsync(T entity)
    {
        _dbSet.Attach(entity).State = EntityState.Modified;
        await _dbSet.AddAsync(entity);
        await _context.SaveChangesAsync();
    }

    public void Update(T entity)
    {
        _context.Update(entity);
        _context.SaveChanges();
    }

    public async void Remove(T entity)
    {
        _context.Entry(entity).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }

    public async Task<int> CountAsync()
    {
        return await _dbSet.CountAsync();
    }

    public async Task<List<T>> FindAsync(Expression<Func<T, bool>> predicate)
    {
        return await _dbSet.Where(predicate).ToListAsync();
    }

    public async Task<T?> FindFirstAsync(Expression<Func<T, bool>> predicate)
    {
        return await _dbSet.Where(predicate).FirstOrDefaultAsync();
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }

    public async Task RemoveByConditionAsync(Expression<Func<T, bool>> predicate)
    {
        await _dbSet.Where(predicate).ExecuteDeleteAsync();
        await _context.SaveChangesAsync();
    }

    public async Task<List<T>> GetAllAsync(int skip, int take, Expression<Func<T, bool>> predicate)
    {
        return await _dbSet.Where(predicate).Skip(skip).Take(take).ToListAsync();
    }

    public async Task<T?> FindByFieldAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes)
    {
        IQueryable<T> query = _context.Set<T>();

        foreach (var include in includes)
            query = query.Include(include);

        return await query.FirstOrDefaultAsync(predicate);
    }

    public async Task<List<T>> GetAllAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes)
    {
        IQueryable<T> query = _context.Set<T>();

        foreach (var include in includes)
            query = query.Include(include);

        return await query.Where(predicate).ToListAsync();
    }

    public async Task<int> GetCountAsync(Expression<Func<T, bool>> predicate)
    {
        return await _dbSet.Where(predicate).CountAsync();
    }

    public async Task<List<T>> GetAllAsync(int skip, int take, Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes)
    {
        IQueryable<T> query = _context.Set<T>();

        foreach (var include in includes)
            query = query.Include(include);

        return await query.Where(predicate).Skip(skip)
            .Take(take).ToListAsync();
    }
}
