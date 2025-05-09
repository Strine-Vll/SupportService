namespace Domain.Abstractions;

public interface IBaseRepository<T> where T : class
{
    Task<List<T>> GetAllAsync();

    Task<T> GetByIdAsync(int id);

    Task CreateAsync(T entity);

    Task UpdateAsync(T entity);

    Task DeleteByIdAsync(int id);

    Task DeleteAsync(T entity);

    Task SaveChangesAsync();
}