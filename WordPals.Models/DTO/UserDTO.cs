using WordPals.Models.Models;

namespace WordPals.Models.DTO;

public class UserDTO
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public ICollection<User> FavoriteUsers { get; set; }
    public ICollection<WordDTO> FavoriteWords { get; set; }
}
