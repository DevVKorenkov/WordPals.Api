using WordPals.Models.Models;

namespace WordPals.Data.Repositories.Abstractions;

public interface IWordsRepository : IRepository<Vocabulary>
{
    void Update(Vocabulary item);
    Task UpdateAsync(Vocabulary item);
}
