namespace WordPals.Models.Models;

public class FavoriteWord
{
    public int Id { get; set; }
    public string UserId { get; set; }
    public Vocabulary Favorite { get; set; }
}
