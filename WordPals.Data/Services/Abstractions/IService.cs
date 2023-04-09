using System.Linq.Expressions;

namespace WordPals.Data.Services.Abstractions;

public interface IService<T> where T : class
{
    IEnumerable<T> GetAllById(string id);
    Task<IEnumerable<T>> GetAllByIdAsync(string id);
    Task<IEnumerable<T>> GetAllAsync();
    T Get(Func<T, bool> filter);
    Task<T> GetAsync(Expression<Func<T, bool>> filter);
    void Add(T item);
    Task AddAsync(T item);
    void Remove(T item);
    Task SaveAsync();
}
