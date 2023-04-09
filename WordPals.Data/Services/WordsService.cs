using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using WordPals.Data.Repositories.Abstractions;
using WordPals.Data.Services.Abstractions;
using WordPals.Models.Models;

namespace WordPals.Data.Services;

public class WordsService : IWordsService
{
    private readonly IWordsRepository _wordsRepository;
    private readonly IUserRepository _userRepository;

    public WordsService(
        IWordsRepository wordsRepository,
        IUserRepository userRepository)
    {
        _wordsRepository = wordsRepository;
        _userRepository = userRepository;
    }

    public void Add(Vocabulary item)
    {
        item.Id = Guid.NewGuid().ToString();
        item = SetUsersData(item);

        _wordsRepository.Add(item);
    }

    public async Task AddAsync(Vocabulary item)
    {
        item.Id = Guid.NewGuid().ToString();
        await SetUsersDataAsync(item);

        await _wordsRepository.AddAsync(item);
    }

    public IEnumerable<Vocabulary> GetAllById(string id)
    {
        var queryWords = _wordsRepository.GetAll(
            u => u.Id == id,
            includes: x => 
            x.Include(w => w.Word)
            .Include(w => w.Definition)
            .Include(w => w.Owner)
            .Include(w => w.FromWhom));

        return queryWords;
    }

    public async Task<IEnumerable<Vocabulary>> GetAllByIdAsync(string id)
    {
        var queryWords = await _wordsRepository.GetAllAsync(
           u => u.OwnerId == id,
           includes: x =>
           x.Include(w => w.Word)
           .Include(w => w.Definition)
           .Include(w => w.Owner)
           .Include(w => w.FromWhom));

        return queryWords;
    }

    public Vocabulary Get(Func<Vocabulary, bool> filter)
    {
        var queryWord = _wordsRepository.Get(filter,
            includes: x => x.Include(v => v.Word)
            .Include(v => v.Definition)
            .Include(v => v.Owner)
            .Include(v => v.FromWhom));

        return queryWord;
    }

    public async Task<Vocabulary> GetAsync(Expression<Func<Vocabulary, bool>> filter)
    {
        var queryWord = await _wordsRepository.GetAsync(filter, 
            includes: x => x.Include(v => v.Word)
            .Include(v => v.Definition)
            .Include(v => v.Owner)
            .Include(v => v.FromWhom));

        return queryWord;
    }

    public void Remove(Vocabulary item)
    {
        item = SetUsersData(item);

        _wordsRepository.Remove(item);
    }

    public void Update(Vocabulary item)
    {
        item = SetUsersData(item);

        _wordsRepository.Update(item);
    }

    public async Task UpdateAsync(Vocabulary item)
    {
        item = await SetUsersDataAsync(item);

        await _wordsRepository.UpdateAsync(item);
    }

    public async Task SaveAsync()
    {
        await _wordsRepository.SaveAsync();
    }

    public async Task<IEnumerable<Vocabulary>> GetAllAsync() => await _wordsRepository.GetAllAsync();

    public async Task AddToFavorite(string userId, string favoriteItemId)
    {
        var user = await _userRepository.GetAsync(u => u.Id == userId,
            includes: i => i.Include(u => u.FavoriteWords));

        var favorite = await _wordsRepository.GetAsync(u => u.Id == favoriteItemId, 
            includes: x => x.Include(v => v.Word)
            .Include(v => v.Definition)
            .Include(v => v.Owner)
            .Include(v => v.FromWhom));

        user.FavoriteWords.Add(favorite);

        await SaveAsync();
    }

    public async Task RemoveFromFavorite(string userId, string favoriteItemId)
    {
        var user = await _userRepository.GetAsync(u => u.Id == userId,
            includes: i => i.Include(u => u.FavoriteWords));

        var favorite = await _wordsRepository.GetAsync(u => u.Id == favoriteItemId);

        user.FavoriteWords.Remove(favorite);

        await SaveAsync();
    }

    private Vocabulary SetUsersData(Vocabulary item)
    {
        item.Owner = _userRepository.Get(u => u.Id == item.OwnerId);
        item.FromWhom = _userRepository.Get(u => u.Id == item.FromWhomId);

        return item;
    }

    private async Task<Vocabulary> SetUsersDataAsync(Vocabulary item)
    {
        item.Owner = await _userRepository.GetAsync(u => u.Id == item.OwnerId);
        item.FromWhom = await _userRepository.GetAsync(u => u.Id == item.FromWhomId);

        return item;
    }
}
