using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace WordPals.Data.Repositories.Abstractions;

public interface IRepository<T> where T : class
{
    IEnumerable<T> GetAll(
        Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, 
        IIncludableQueryable<T, object>> includes = null);
    Task<IEnumerable<T>> GetAllAsync(
        Expression<Func<T, bool>> filter = null, 
        Func<IQueryable<T>, IIncludableQueryable<T, object>> includes = null);
    T Get(Func<T, bool> filter = null, 
        Func<IQueryable<T>, IIncludableQueryable<T, object>> includes = null);

    Task<T> GetAsync(Expression<Func<T, bool>> filter = null, 
        Func<IQueryable<T>, IIncludableQueryable<T, object>> includes = null);
    void Add(T item);
    Task AddAsync(T item);
    void Remove(T item);
    void Save();
    Task SaveAsync();
}
