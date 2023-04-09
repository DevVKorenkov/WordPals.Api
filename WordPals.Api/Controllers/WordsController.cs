using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WordPals.Api.Extensions;
using WordPals.Data.Helpers;
using WordPals.Data.Services.Abstractions;
using WordPals.Models.DTO;

namespace WordPals.Api.Controllers;

[ApiController]
[Authorize]
[ProducesResponseType(StatusCodes.Status200OK)]
[ProducesResponseType(StatusCodes.Status400BadRequest)]
[Route("api/[controller]")]
public class WordsController : Controller
{
    private readonly IWordsService _wordsService;
    private readonly IUserService _userService;

    public WordsController(IWordsService wordsService, IUserService userService)
    {
        _wordsService = wordsService;
        _userService = userService;
    }

    [HttpGet, Route("getWords")]
    public async Task<IActionResult> GetWords()
    {
        var userId = User.GetUserId();
        var result = await _wordsService.GetAllByIdAsync(userId);
        var words = WordHelper.WordsHandler(result);
        return Json(words);
    }

    [HttpGet, Route("getWord")]
    public async Task<IActionResult> GetWord(string id)
    {
        var userId = User.GetUserId();
        var result = await _wordsService.GetAsync(v => v.Id == id);
        var word = WordHelper.WordHandler(result);
        return Json(word);
    }

    [HttpGet, Route("getWordsForCheck")]
    public async Task<IActionResult> GetWordsForCheck()
    {
        var userId = User.GetUserId();
        var result = await _wordsService.GetAsync(v => v.FromWhomId == userId && !v.IsCheked);
        var word = WordHelper.WordHandler(result);
        return Json(word);
    }

    [HttpGet, Route("getGivenWords")]
    public async Task<IActionResult> GetGivenWords()
    {
        var userId = User.GetUserId();
        var result = await _wordsService.GetAsync(v => v.FromWhomId == userId);
        var word = WordHelper.WordHandler(result);
        return Json(word);
    }

    [HttpPost, Route("addWord")]
    public async Task AddWord(WordDTO wordDto)
    {
        var word = WordHelper.VocabularyHandler(wordDto);
        await _wordsService.AddAsync(word);
        await _wordsService.SaveAsync();
    }

    [HttpPost, Route("updateWord")]
    public async Task UpdateWord(WordDTO wordDto)
    {
        var word = WordHelper.VocabularyHandler(wordDto);
        await _wordsService.UpdateAsync(word);
        await _wordsService.SaveAsync();
    }

    [HttpPost, Route("removeWord")]
    public async Task RemoveWord(WordDTO wordDto)
    {
        var word = WordHelper.VocabularyHandler(wordDto);
        _wordsService.Remove(word);
        await _wordsService.SaveAsync();
    }

    [HttpPost, Route("addWordToFavorite")]
    public async Task<IActionResult> AddWordToFavorite(string wordId)
    {
        var user = await UserHelper.GetUserDTOWithFavorites(
            User.GetUserId(), 
            wordId, 
            _wordsService.AddToFavorite, 
            _userService);

        return Ok(new
        {
            Message = "User has been added to favorites succesfully",
            User = user,
        });
    }

    [HttpPost, Route("removeWordToFavorite")]
    public async Task<IActionResult> RemoveWordFromFavorite(string wordId)
    {
        var user = await UserHelper.GetUserDTOWithFavorites(
            User.GetUserId(), 
            wordId, 
            _wordsService.RemoveFromFavorite, 
            _userService);

        return Ok(new
        {
            Message = "User has been added to favorites succesfully",
            User = user,
        });
    }
}
