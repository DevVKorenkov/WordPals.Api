using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace WordPals.Models.Models;

[DisplayName("Word")]
public class WordModel
{
    [Key]
    public int Id { get; set; }
    public string Word { get; set; }
}
