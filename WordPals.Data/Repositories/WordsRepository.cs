using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;
using WordPals.Data.DataContext;
using WordPals.Data.Repositories.Abstractions;
using WordPals.Models.Models;

namespace WordPals.Data.Repositories;

public class WordsRepository : IWordsRepository
{
    private readonly AppDbContext _dbContext;
    private readonly DbSet<Vocabulary> _dbSet;

    public WordsRepository(AppDbContext appDbContext) 
    {
        _dbContext = appDbContext;
        _dbSet = _dbContext.Set<Vocabulary>();
    }

    public void Add(Vocabulary item)
    {
        _dbContext.Vocabulary.Add(item);
    }

    public async Task AddAsync(Vocabulary item)
    {
        await _dbContext.Vocabulary.AddAsync(item);
    }

    public Vocabulary Get(string id)
    {
        return _dbContext.Vocabulary.SingleOrDefault(w => w.Id == id);
    }

    public Vocabulary Get(Func<Vocabulary , bool> filter = null, 
        Func<IQueryable<Vocabulary>, IIncludableQueryable<Vocabulary, object>> includes = null)
    {
        IQueryable<Vocabulary> query = _dbSet;

        if (filter != null)
        {
            query = query.Where(filter).AsQueryable();
        }

        if (includes != null)
        {
            query = includes(query);
        }

        var words = query.FirstOrDefault();

        return words;
    }
    
    public async Task<Vocabulary> GetAsync(Expression<Func<Vocabulary, bool>> filter = null,  
        Func<IQueryable<Vocabulary>, IIncludableQueryable<Vocabulary, object>> includes = null)
    {
        IQueryable<Vocabulary> query = _dbSet;

        if (filter != null)
        {
            query = query.Where(filter);
        }

        if (includes != null)
        {
            query = includes(query);
        }

        var words = await query.FirstOrDefaultAsync();

        return words;
    }

    public IEnumerable<Vocabulary > GetAll(
        Expression<Func<Vocabulary , bool>> filter = null, Func<IQueryable<Vocabulary >, 
        IIncludableQueryable<Vocabulary , object>> includes = null)
    {
        IQueryable<Vocabulary > query = _dbSet;

        if (filter != null)
        {
            query = query.Where(filter);
        }

        if (includes != null)
        {
            query = includes(query);
        }

        var words = query.ToList();

        return words;
    }

    public async Task<IEnumerable<Vocabulary >> GetAllAsync(
        Expression<Func<Vocabulary, bool>> filter = null, Func<IQueryable<Vocabulary >,
        IIncludableQueryable<Vocabulary, object>> includes = null)
    {
        IQueryable<Vocabulary> query = _dbSet;

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

        var words = await query.ToListAsync();

        return words;
    }

    public void Remove(Vocabulary item)
    {        
        _dbContext.Vocabulary.Remove(item);
    }

    public void Save()
    {
        _dbContext.SaveChanges();
    }

    public async Task SaveAsync()
    {
        await _dbContext.SaveChangesAsync();
    }

    public void Update(Vocabulary item)
    {
        _dbContext.Vocabulary.Update(item);
    }

    public async Task UpdateAsync(Vocabulary item)
    {
        await Task.Run(() => _dbContext.Vocabulary.Update(item));
    }
}
