using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WordPals.Api.Extensions;
using WordPals.Data.Helpers;
using WordPals.Data.Services.Abstractions;
using WordPals.Models.DTO;
using WordPals.Models.Models;

namespace WordPals.Api.Controllers;

[ApiController]
[Authorize]
[Route("api/[controller]")]
public class UserController : Controller
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet, Route("getUser")]
    public async Task<IActionResult> GetUser(string id)
    {
        var user = await _userService.GetAsync(u => u.Id == id);

        return user != null ? Ok(new UserResponse
        {
            Message = $"User {user.UserName} has been found",
            User = UserHelper.GetUserDto(user),
        }) 
            : NotFound(new UserResponse
            {
            Message = "User not found"
            });
    }

    [HttpGet,Route("getAllUsers")]
    public async Task<IActionResult> GetAllUsers()
    {
        var appUsers = await _userService.GetAllAsync();
        var users = UserHelper.GetUserDTOs(appUsers);

        return users.Any() ? Ok(new 
        { 
            Message = "Users have been gotten", 
            Users = users
        }) 
            : BadRequest(new UserResponse
            {
                Message = "Users not found or we have trobles with getting users."
            });
    }

    [HttpGet, Route("fetchuser")]
    public IActionResult FetchUser()
    {
        return Ok(new UserDTO
        {
            Id = User.GetUserId(),
            Name = User.GetUserName(),
            Email = User.GetUserEmail()
        });
    }

    [HttpGet, Route("getFavorites")]
    public async Task<IActionResult> GetWithFavorites()
    {
        var appUser = await _userService.GetWithFavorites(User.GetUserId());
        var user = UserHelper.GetUserDto(appUser);

        return user != null ? Ok(new
        {
            Message = "User with favorites has been found",
            User = user
        }) : NotFound(new
        {
            Message = "User not found"
        });
    }

    [HttpPost, Route("addFavoriteUser")]
    public async Task<IActionResult> AddToFavorite(string favoriteUserId)
    {
        var user = await UserHelper.GetUserDTOWithFavorites(
            User.GetUserId(), 
            favoriteUserId, 
            _userService.AddToFavorite, 
            _userService);

        return Ok(new
        {
            Message = "User has been added to favorites succesfully",
            User = user,
        });
    }

    [HttpPost, Route("removeFromFavorites")]
    public async Task<IActionResult> RemoveFromFavorites(string removedUserId)
    {
        var user = await UserHelper.GetUserDTOWithFavorites(
            User.GetUserId(), 
            removedUserId, 
            _userService.AddToFavorite, 
            _userService);

        return Ok(new
        {
            Message = "User has been added to favorites succesfully",
            User = user,
        });
    }
}
