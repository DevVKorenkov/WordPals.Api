using WordPals.Models.DTO;

namespace WordPals.Models.Models;

public class UserResponse
{
    public string Message { get; set; }

    public UserDTO User { get; set; }
}
