using WordPals.Models.DTO;
using WordPals.Models.Models;

namespace WordPals.Data.Helpers;

public class WordHelper
{
    private const string emailIsHidden = "User's email is hidden";

    public static IEnumerable<WordDTO> WordsHandler(IEnumerable<Vocabulary> vocabulary)
    {
        IEnumerable<WordDTO> words = null;

        if (vocabulary != null)
        {
            words = vocabulary.Select(x => new WordDTO
            {
                Id = x.Id,
                Word = x.Word,
                IsNew = x.IsNew,
                Definition = x.Definition,
                Owner = new User
                {
                    Id = x.Owner.Id,
                    Name = x.Owner.UserName,
                    Email = x.Owner.IsAllowToShowEmail ? x.Owner.Email : emailIsHidden,
                },
                FromWhom = new User
                {
                    Id = x.FromWhom.Id,
                    Name = x.FromWhom.UserName,
                    Email = x.Owner.IsAllowToShowEmail ? x.Owner.Email : emailIsHidden,
                },
                IsRight = x.IsRight,
            });
        }

        return words ?? new List<WordDTO>();
    }

    public static WordDTO WordHandler(Vocabulary vocabulary)
    {
        var word = new WordDTO
        {
            Id = vocabulary.Id,
            Word = vocabulary.Word,
            IsNew = vocabulary.IsNew,
            Definition = vocabulary.Definition,
            Owner = new User
            {
                Id = vocabulary.Owner.Id,
                Name = vocabulary.Owner.UserName,
                Email = vocabulary.Owner.IsAllowToShowEmail ? vocabulary.Owner.Email : emailIsHidden,
            },
            FromWhom = new User
            {
                Id = vocabulary.FromWhom.Id,
                Name = vocabulary.FromWhom.UserName,
                Email = vocabulary.Owner.IsAllowToShowEmail ? vocabulary.Owner.Email : emailIsHidden,
            },
            IsRight = vocabulary.IsRight,
        };

        return word;
    }

    public static Vocabulary VocabularyHandler(WordDTO wordDTO)
    {
        var vocabulary = new Vocabulary
        {
            Id = wordDTO.Id,
            WordId = wordDTO.Word.Id,
            Word = wordDTO.Word,
            DefinitionId = wordDTO.Definition == null ? default : wordDTO.Definition.Id,
            Definition = wordDTO.Definition ?? new Definition(),
            IsNew = wordDTO.IsNew,
            OwnerId = wordDTO.Owner.Id,
            FromWhomId = wordDTO.FromWhom.Id,
            IsRight = wordDTO.IsRight,
            IsCheked = wordDTO.IsCheked,
        };

        return vocabulary;
    }
}
