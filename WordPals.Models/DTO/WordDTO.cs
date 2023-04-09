using WordPals.Models.Models;

namespace WordPals.Models.DTO;

public class WordDTO
{
    public string Id { get; set; }

    public WordModel Word { get; set; }

    public bool IsNew { get; set; }

    public Definition Definition { get; set; }

    public User Owner { get; set; }

    public User FromWhom { get; set; }

    public bool IsRight { get; set; }

    public bool IsCheked {get; set; }
}
