using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using WordPals.Data.Repositories.Abstractions;
using WordPals.Data.Services.Abstractions;
using WordPals.Models.Models;

namespace WordPals.Data.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IWordsService _wordService;

    public UserService(IUserRepository userRepository, IWordsService wordsService)
    { 
        _userRepository = userRepository;
        _wordService = wordsService;
    }

    public void Add(AppIdentityUser item)
    {
        _userRepository.Add(item);
    }

    public async Task AddAsync(AppIdentityUser item)
    {
        await _userRepository.AddAsync(item);
    }

    public AppIdentityUser Get(Func<AppIdentityUser, bool> filter)
    {
        var user = _userRepository.Get(filter);

        return user;
    }

    public async Task<AppIdentityUser> GetAsync(Expression<Func<AppIdentityUser, bool>> filter)
    {
        var user = await _userRepository.GetAsync(filter);

        return user;
    }

    public AppIdentityUser Get(string id)
    {
        return _userRepository.Get(u => u.Id == id);
    }

    public IEnumerable<AppIdentityUser> GetAllById(string id)
    {
        var users = _userRepository.GetAll(u => u.Id == id);

        return users;
    }

    public async Task<IEnumerable<AppIdentityUser>> GetAllByIdAsync(string id)
    {
        var users = await _userRepository.GetAllAsync(u => u.Id == id);

        return users;
    }

    public async Task<IEnumerable<AppIdentityUser>> GetAllAsync() => await _userRepository.GetAllAsync();

    public void Remove(AppIdentityUser item)
    {
        _userRepository.Remove(item);
    }

    public async Task SaveAsync()
    {
        await _userRepository.SaveAsync();
    }

    public void Update(AppIdentityUser item)
    {
        _userRepository.Update(item);
    }

    public async Task<AppIdentityUser> GetWithFavorites(string id)
    { 
        var user = await _userRepository.GetAsync(u => u.Id == id,
            includes: i => i
            .Include(u => u.FavoriteUsers)
            .Include(u => u.FavoriteWords)
            .ThenInclude(u => u.Owner)
            .Include(u => u.FavoriteWords)
            .ThenInclude(u => u.FromWhom));

        return user;
    }

    public async Task AddToFavorite(string id, string favoriteUserId)
    {
        var user = await _userRepository.GetAsync(u => u.Id == id,
            includes: i => i.Include(u => u.FavoriteUsers));

        var favorite = await _userRepository.GetAsync(u => u.Id == favoriteUserId);

        user.FavoriteUsers.Add(favorite);

        await SaveAsync();
    }

    public async Task RemoveFromFavorite(string id, string removedUserId)
    {
        var user = await _userRepository.GetAsync(u => u.Id == id, 
            includes: i => i.Include(u => u.FavoriteUsers));

        var removedUser = await _userRepository.GetAsync(u => u.Id == removedUserId);

        user.FavoriteUsers.Remove(removedUser);

        await SaveAsync();
    }
}
