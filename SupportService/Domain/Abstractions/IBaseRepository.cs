namespace Domain.Abstractions;

public interface IBaseRepository<T> where T : class
{
    Task<IEnumerable<T>> GetAllAsync();

    Task<T> GetByIdAsync(int id);

    Task CreateAsync(T entity);

    Task UpdateAsync(T entity);

    Task DeleteByIdAsync(int id);

    Task DeleteAsync(T entity);

    Task SaveChangesAsync();
}