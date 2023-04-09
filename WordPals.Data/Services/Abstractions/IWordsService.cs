using WordPals.Models.DTO;
using WordPals.Models.Models;

namespace WordPals.Data.Services.Abstractions;

public interface IWordsService : IService<Vocabulary>
{
    void Update(Vocabulary item);
    Task UpdateAsync(Vocabulary item);
    Task AddToFavorite(string userId, string favoriteItemId);
    Task RemoveFromFavorite(string userId, string removedItemId);
}
