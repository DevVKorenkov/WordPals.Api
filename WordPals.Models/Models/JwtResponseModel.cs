namespace WordPals.Models.Models;

public class JwtResponseModel
{
    public string Token { get; private set; }

	public JwtResponseModel(string token)
	{
		Token = token;
	}
}
