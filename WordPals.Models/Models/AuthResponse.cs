using WordPals.Models.DTO;

namespace WordPals.Models.Models;

public class AuthResponse
{
    public string Message { get; set; }
    public JwtResponseModel Token { get; set; }
    public UserDTO User { get; set; }
}