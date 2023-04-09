using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WordPals.Models.Models;

public class Vocabulary
{
    [Key]
    public string Id { get; set; }

    public int WordId { get; set; }

    [ForeignKey("WordId")]
    public WordModel Word { get; set; }

    public int DefinitionId { get; set; }

    [ForeignKey("DefinitionId")]
    public Definition Definition { get; set; }

    public bool IsNew { get; set; }

    public string OwnerId { get; set; }

    [ForeignKey("OwnerId")]
    public AppIdentityUser Owner { get; set; }

    public string FromWhomId { get; set; }

    [ForeignKey("FromWhomId")]
    public AppIdentityUser FromWhom { get; set; }

    public bool IsRight { get; set; }

    public bool IsCheked { get; set; }

    public ICollection<AppIdentityUser> UsersWhoHaveWord { get; set; }
}
