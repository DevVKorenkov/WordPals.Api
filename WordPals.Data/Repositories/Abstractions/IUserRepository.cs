using System.Linq.Expressions;
using WordPals.Models.Models;

namespace WordPals.Data.Repositories.Abstractions;

public interface IUserRepository : IRepository<AppIdentityUser>
{
    void Update(AppIdentityUser item);
}
