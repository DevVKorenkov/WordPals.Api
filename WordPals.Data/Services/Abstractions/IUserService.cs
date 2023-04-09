using System.Linq.Expressions;
using WordPals.Models.Models;

namespace WordPals.Data.Services.Abstractions;

public interface IUserService : IService<AppIdentityUser>
{
    void Update(AppIdentityUser item);
    Task<AppIdentityUser> GetWithFavorites(string id);
    Task AddToFavorite(string id, string favoriteUserId);
    Task RemoveFromFavorite(string id, string removedUserId);
}
