using Microsoft.AspNetCore.Identity;

namespace WordPals.Models.Models;

public class AppIdentityUser : IdentityUser
{
    public bool IsAllowToShowEmail { get; set; }
    public ICollection<AppIdentityUser> FavoriteUsers { get; set; }
    public ICollection<Vocabulary> FavoriteWords { get; set; }
}
