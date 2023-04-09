using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;
using WordPals.Data.DataContext;
using WordPals.Data.Repositories.Abstractions;
using WordPals.Models.Models;

namespace WordPals.Data.Repositories;

public class UserRepository : IUserRepository
{
    private readonly AppDbContext _dbContext;
    private readonly DbSet<AppIdentityUser> _dbSet;

    public UserRepository(AppDbContext appDbContext)
    {
        _dbContext = appDbContext;
        _dbSet = appDbContext.Set<AppIdentityUser>();
    }

    public void Add(AppIdentityUser item)
    {
        _dbSet.Add(item);
    }

    public async Task AddAsync(AppIdentityUser item)
    {
        await _dbSet.AddAsync(item);
    }

    public AppIdentityUser Get(Func<AppIdentityUser, bool> filter = null, 
        Func<IQueryable<AppIdentityUser>, IIncludableQueryable<AppIdentityUser, object>> includes = null)
    {
        IQueryable<AppIdentityUser> query = _dbSet;

        if (includes != null)
        {
            query = includes(query);
        }

        if (filter != null)
        {
            query = query.Where(filter).AsQueryable();
        }

        var user = query.FirstOrDefault();

        return user;
    }

    public async Task<AppIdentityUser> GetAsync(Expression<Func<AppIdentityUser, bool>> filter = null, 
        Func<IQueryable<AppIdentityUser>, IIncludableQueryable<AppIdentityUser, object>> includes = null)
    {
        IQueryable<AppIdentityUser> query = _dbSet;

        if (includes != null)
        {
            query = includes(query);
        }

        if (filter != null)
        {
            query = query.Where(filter);
        }

        var user = await query.FirstOrDefaultAsync();

        return user;
    }

    public IEnumerable<AppIdentityUser> GetAll(
        Expression<Func<AppIdentityUser, bool>> filter = null, 
        Func<IQueryable<AppIdentityUser>, IIncludableQueryable<AppIdentityUser, object>> includes = null)
    {
        IQueryable<AppIdentityUser> query = _dbSet;

        if (includes != null)
        {
            query = includes(query);
        }

        if (filter != null)
        {
            query = query.Where(filter);
        }

        var users = query.ToList();

        return users;
    }

    public async Task<IEnumerable<AppIdentityUser>> GetAllAsync(
        Expression<Func<AppIdentityUser, bool>> filter = null, 
        Func<IQueryable<AppIdentityUser>, IIncludableQueryable<AppIdentityUser, object>> includes = null)
    {
        IQueryable<AppIdentityUser> query = _dbSet;

        await Task.Run(() =>
        {
            if (includes != null)
            {
                query = includes(query);
            }

            if (filter != null)
            {
                query = query.Where(filter);
            }
        });
        
        var users = await query.ToListAsync();

        return users;
    }

    public void Remove(AppIdentityUser item)
    {
        _dbSet.Remove(item);
    }

    public void Save()
    {
        _dbContext.SaveChanges();
    }

    public async Task SaveAsync()
    {
        await _dbContext.SaveChangesAsync();
    }

    public void Update(AppIdentityUser item)
    {
        _dbSet.Update(item);
    }
}
