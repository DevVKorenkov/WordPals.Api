using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WordPals.Api.Config;
using WordPals.Data.Services.Abstractions;
using WordPals.Models;
using WordPals.Models.DTO;
using WordPals.Models.Models;

namespace WordPals.Api.Controllers;

[ApiController]
[ProducesResponseType(StatusCodes.Status200OK)]
[ProducesResponseType(StatusCodes.Status400BadRequest)]
[Route("api/[controller]")]
public class LoginController : Controller
{
    private readonly UserManager<AppIdentityUser> _userManager;
    private readonly SignInManager<AppIdentityUser> _signInManager;
    private readonly IUserService _userService;
    private readonly RoleManager<IdentityRole> _roleManager;

    public LoginController(
        UserManager<AppIdentityUser> userManager,
        SignInManager<AppIdentityUser> signInManager,
        IUserService userService,
        RoleManager<IdentityRole> roleManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _userService = userService;
        _roleManager = roleManager;
    }

    [HttpPost, Route("register")]
    public async Task<IActionResult> Register(RegisterModel register)
    {
        var checkName = await _userManager.FindByNameAsync(register.Name);

        if (checkName != null)
        {
            return BadRequest(new AuthResponse
            {
                Message = $"User {register.Name} or email already exists."
            });
        }

        var checkEmail = await _userManager.FindByEmailAsync(register.Email);

        if(!string.IsNullOrWhiteSpace(register.Email) && checkEmail != null)
        {
            return BadRequest(new AuthResponse
            {
                Message = $"This email is already registered."
            });
        }

        var user = new AppIdentityUser()
        {
            UserName = register.Name,
            Email = register.Email,
        };

        var isFirstUser = _userManager.Users.Any();

        if (!isFirstUser)
        {
            register.Role = Roles.Admin;
        }

        if (!User.IsInRole(Roles.Admin) && register.Role == Roles.Admin)
        {
            return Unauthorized(new AuthResponse
            {
                Message = "You are not allowed to create admin user."
            });
        }

        var result = await _userManager.CreateAsync(user, register.Password);

        if (result.Succeeded)
        {
            if(!await _roleManager.RoleExistsAsync(register.Role))
            {
                await _roleManager.CreateAsync(new IdentityRole(register.Role));
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, register.Role),
            };

            await _userManager.AddClaimsAsync(user, claims);

            await _userManager.AddToRoleAsync(user, register.Role);

            await _signInManager.SignInAsync(user, false);
            
            await _userService.SaveAsync();

            var jwtToken = await CreateJwtToken(user);

            return Ok(new AuthResponse
            {
                Message = "User has been created successfully",
                Token = jwtToken,
                User = new UserDTO
                {
                    Id = user.Id,
                    Name = user.UserName,
                    Email = user.Email
                }
            });
        }
        else
        {
            foreach (var identity in result.Errors)
            {
                ModelState.AddModelError("", identity.Description);
            }

            return BadRequest(new AuthResponse
            {
                Message = result.Errors?.FirstOrDefault()?.Description
            });
        }
    }

    [HttpPost, Route("signin")]
    public async Task<IActionResult> Login(LoginModel loginModel)
    {
        var user = await _userManager.FindByNameAsync(loginModel.Name) ?? await _userManager.FindByEmailAsync(loginModel.Name);

        if (user == null)
        {
            return NotFound(new AuthResponse
            {
                Message = $"User {loginModel.Name} not found"
            });
        }

        var result = await _signInManager.PasswordSignInAsync(loginModel.Name, loginModel.Password, false, false);

        if (result.Succeeded)
        {
            var jwtToken = await CreateJwtToken(user);
            var response = new AuthResponse
            {
                Message = "You have successfully logged in",
                Token = jwtToken,
                User = new UserDTO
                {
                    Id = user.Id,
                    Name = user.UserName,
                    Email = user.Email
                }
            };
            return Ok(response);
        }
        else
        {
            ModelState.AddModelError("", "Login or password is wrong");
            return Unauthorized(new AuthResponse
            {
                Message = "Login or password is incorrect"
            });
        }
    }

    [HttpGet, Route("logout")]
    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();

        return Ok(new AuthResponse
        {
            Message = "You have successfully logged out"
        });
    }

    private async Task<JwtResponseModel> CreateJwtToken(AppIdentityUser user)
    {
        var roles = await _userManager.GetRolesAsync(user);
        var rolesString = string.Join(',', roles);
        var claims = new List<Claim>()
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id),
            new Claim(ClaimTypes.Name, user.UserName),
            new Claim(ClaimTypes.Role, rolesString),
        };
        var secret = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SettingsManager.AppSettings["JWT:Secret"]));
        var credentials = new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
        var tokenOptions = new JwtSecurityToken(
            issuer: SettingsManager.AppSettings["Jwt:ValidIssuer"],
            audience: SettingsManager.AppSettings["Jwt:ValidAudience"],
            claims: claims,
            expires: DateTime.Now.AddDays(1),
            signingCredentials: credentials);
        var tokenString = new JwtSecurityTokenHandler().WriteToken(tokenOptions);

        return new JwtResponseModel(tokenString);
    }
}
