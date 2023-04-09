using System.ComponentModel.DataAnnotations;

namespace WordPals.Models.Models;

public class Definition
{
    [Key]
    public int Id { get; set; }

    public string Explanation { get; set; }
}
