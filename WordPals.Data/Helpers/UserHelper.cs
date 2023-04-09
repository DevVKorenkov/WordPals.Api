using WordPals.Data.Services.Abstractions;
using WordPals.Models.DTO;
using WordPals.Models.Models;

namespace WordPals.Data.Helpers;

public class UserHelper
{
    private const string emailIsHidden = "User's email is hidden";

    public static UserDTO GetUserDto(AppIdentityUser appUser)
    {
        var userDto = new UserDTO
        {
            Id = appUser.Id,
            Name = appUser.UserName,
            Email = appUser.IsAllowToShowEmail ? emailIsHidden : appUser.Email,
            FavoriteUsers = MakeFavorites(appUser.FavoriteUsers).ToList(),
            FavoriteWords = WordHelper.WordsHandler(appUser.FavoriteWords).ToList(),
        };

        return userDto;
    }

    public static IEnumerable<User> GetUserDTOs(IEnumerable<AppIdentityUser> appUsers)
    {
        var user = appUsers.Select(appUser => new User
        {
            Id = appUser.Id,
            Name = appUser.UserName,
            Email = appUser.IsAllowToShowEmail ? emailIsHidden : appUser.Email,
        });

        return user;
    }
    

    public static IEnumerable<User> MakeFavorites(ICollection<AppIdentityUser> appUserFavorites)
    {
        var favorites = appUserFavorites.Select(appUser => new User
        {
            Id = appUser.Id,
            Name = appUser.UserName,
            Email = appUser.IsAllowToShowEmail ? emailIsHidden : appUser.Email,
        });

        return favorites ?? new List<User>();
    }

    public static async Task<UserDTO> GetUserDTOWithFavorites(
        string userId, 
        string favoriteUserId, 
        Func<string, string, Task> action, 
        IUserService userService)
    {
        await action(userId, favoriteUserId);
        var appUser = await userService.GetWithFavorites(userId);
        var user = GetUserDto(appUser);

        return user;
    }
}
