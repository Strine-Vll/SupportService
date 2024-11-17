using Domain.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class BaseRepository<T> : IBaseRepository<T> where T : class
{
    private readonly ApplicationDbContext _dbContext;
    protected DbSet<T> _dbSet;

    public BaseRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
        _dbSet = _dbContext.Set<T>();
    }

    public async virtual Task<T> GetByIdAsync(int id)
    {
        var result = await _dbSet.FindAsync(id) ?? default(T);

        return result;
    }

    public async virtual Task<IEnumerable<T>> GetAllAsync()
    {
        var result = await _dbSet.AsNoTracking().ToListAsync();

        return result;
    }

    public async virtual Task CreateAsync(T entity)
    {
        await _dbSet.AddAsync(entity);

        await SaveChangesAsync();
    }

    public async virtual Task UpdateAsync(T entity)
    {
        _dbSet.Update(entity);

        await SaveChangesAsync();
    }

    public async virtual Task DeleteAsync(T entity)
    {
        _dbSet.Remove(entity);

        await SaveChangesAsync();
    }

    public async virtual Task DeleteByIdAsync(int id)
    {
        var entity = await GetByIdAsync(id);

        await DeleteAsync(entity);
    }

    public async virtual Task SaveChangesAsync()
    {
        await _dbContext.SaveChangesAsync();
    }
}
